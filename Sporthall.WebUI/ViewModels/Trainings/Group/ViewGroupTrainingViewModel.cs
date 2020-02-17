using Sporthall.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sporthall.WebUI.ViewModels.Trainings.Group
{
    public class ViewGroupTrainingViewModel : GroupTrainingViewModel
    {
        public int Id { get; set; }
        public Hall Hall { get; set; }
        public List<User> CoachUsers { get; set; }
        public List<User> ClientUsers { get; set; }
        public ViewGroupTrainingViewModel()
        {
        }
        public ViewGroupTrainingViewModel(GroupTraining groupTraining, Hall hall, List<User> coachUsers, List<User> clientUsers)
            : base(groupTraining)
        {
            Id = groupTraining.Id;
            Hall = hall;
            CoachUsers = coachUsers;
            ClientUsers = clientUsers;
        }
    }
}
