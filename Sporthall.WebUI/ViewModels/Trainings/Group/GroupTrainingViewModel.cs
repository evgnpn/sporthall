using Sporthall.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sporthall.WebUI.ViewModels.Trainings.Group
{
    public class GroupTrainingViewModel
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Capacity { get; set; }
        public virtual DateTime Date { get; set; } = DateTime.Now;
        public virtual TimeSpan BeginTime { get; set; }
        public virtual TimeSpan EndTime { get; set; }
        public virtual bool Active { get; set; }
        public GroupTrainingViewModel()
        {
        }
        public GroupTrainingViewModel(GroupTraining groupTraining)
        {
            if (groupTraining is null)
            {
                throw new ArgumentNullException(nameof(groupTraining));
            }
            Name = groupTraining.Name;
            Description = groupTraining.Description;
            Capacity = groupTraining.Capacity;
            Date = groupTraining.Date;
            BeginTime = groupTraining.BeginTime;
            EndTime = groupTraining.EndTime;
            Active = groupTraining.Active;
        }
    }
}
