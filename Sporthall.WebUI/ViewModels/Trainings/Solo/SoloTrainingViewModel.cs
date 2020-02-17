using Sporthall.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Trainings.Solo
{
    public class SoloTrainingViewModel
    {
        public virtual string Description { get; set; }
        public virtual DateTime Date { get; set; } = DateTime.Now;
        public virtual TimeSpan BeginTime { get; set; }
        public virtual TimeSpan EndTime { get; set; }
        public virtual bool Active { get; set; }

        public SoloTrainingViewModel()
        {
        }

        public SoloTrainingViewModel(SoloTraining soloTraining)
        {
            if (soloTraining is null)
            {
                throw new ArgumentNullException(nameof(soloTraining));
            }
            Description = soloTraining.Description;
            Date = soloTraining.Date;
            BeginTime = soloTraining.BeginTime;
            EndTime = soloTraining.EndTime;
            Active = Active;
        }
    }
}
