using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sporthall.Core.Exceptions;
using Sporthall.Core.Exceptions.ServiceExceptions;
using Sporthall.Core.Services;
using Sporthall.WebUI.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sporthall.WebUI.Filters
{
    public class ModelStateReturnerAttribute : ActionFilterAttribute, IActionFilter, IAsyncActionFilter
    {
        public bool ReturnOnInvalidModelState { get; set; } = true;
        public bool ReturnOnException { get; set; } = true;
        public string ViewName { get; set; } = null;

        private readonly Type _modelType;

        public ModelStateReturnerAttribute(Type modelType = null)
        {
            if (modelType != null)
            {
                if (!IsModel(modelType))
                {
                    throw new ArgumentException(nameof(modelType) + " is not a class or interface");
                }
            }
            _modelType = modelType;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext actionExecutingContext, ActionExecutionDelegate next)
        {
            if (!HttpMethods.IsPost(actionExecutingContext.HttpContext.Request.Method))
            {
                await next();
                return;
            }

            var argValues = actionExecutingContext.ActionArguments.Values;
            var viewModel = _modelType is null ?
                argValues.FirstOrDefault(v => IsModel(v.GetType())) :
                argValues.SingleOrDefault(v => v.GetType() == _modelType);

            if (viewModel is null)
            {
                await next();
                return;
            }

            var controller = (Controller)actionExecutingContext.Controller;

            if (ReturnOnInvalidModelState && !actionExecutingContext.ModelState.IsValid)
            {
                await RebuildIfNeededAsync(controller, viewModel);
                actionExecutingContext.Result = GetView(controller, viewModel);
            }
            else
            {
                var actionExecutedContext = await next();

                if (ReturnOnException && actionExecutedContext.Exception != null)
                {
                    await RebuildIfNeededAsync(controller, viewModel);
                    AddModelStateErrors(actionExecutedContext, controller);
                    actionExecutedContext.ExceptionHandled = true;
                    actionExecutedContext.Result = GetView(controller, viewModel);
                }
            }
        }

        private async Task RebuildIfNeededAsync(Controller controller, object viewModel)
        {
            var services = (AppServices)controller.HttpContext.RequestServices.GetService(typeof(AppServices));

            if (viewModel is IRebuildableAsync rebuildableAsyncModel)
            {
                await rebuildableAsyncModel.RebuildAsync(services);
            }
            else if (viewModel is IRebuildable rebuildableModel)
            {
                rebuildableModel.Rebuild(services);
            }
        }

        private void AddModelStateErrors(ActionExecutedContext context, Controller controller)
        {
            if (context.Exception is ServiceException sEx)
            {
                foreach (var err in sEx.Errors)
                {
                    switch (err.ErrorType)
                    {
                        case ServiceErrorType.Model:
                            controller.ModelState.AddModelError(err.Key, err.Value);
                            break;
                        case ServiceErrorType.Identity:
                            controller.ModelState.AddModelError("", err.Value);
                            break;
                        default:
                            controller.ModelState.AddModelError("", err.Value);
                            break;
                    }
                }
            }
            else if (context.Exception is Exception ex)
            {
                ex.WalkThroughInnerExceptions(e => controller.ModelState.AddModelError("", e.Message));
            }
        }

        private ViewResult GetView(Controller controller, object model)
        {
            return string.IsNullOrEmpty(ViewName) ? controller.View(model) : controller.View(ViewName, model);
        }

        private bool IsModel(Type type)
        {
            return (type.GetType().IsClass || type.GetType().IsInterface) && type.GetType() != typeof(string);
        }
    }
}
