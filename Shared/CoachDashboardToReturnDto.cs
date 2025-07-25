﻿using Shared.TraineeGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record CoachDashboardToReturnDto
    {
        public ICollection<GymCoachDashboardToReturnDto> Gyms { get; set; } = new HashSet<GymCoachDashboardToReturnDto>();
        public ICollection<ClassCoachDashboardToReturnDto> Classes { get; set; } = new HashSet<ClassCoachDashboardToReturnDto>();
        public ICollection<TraineeCoachDashboardToReturnDto> Trainees { get; set; } = new HashSet<TraineeCoachDashboardToReturnDto>();
    }
    public record TraineeCoachDashboardToReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }
    public record TraineeCoachDashboardDetailDto
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string ImageUrl { get; init; }
        public DateOnly? DateOfBirth { get; init; }
        public string ReasonForJoining { get; init; }
        public double? Weight { get; init; }
        public string GymName { get; init; }
    }
    public record GymCoachDashboardToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }

    public record ClassCoachDashboardToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int CurrentCapacity { get; set; }
    }
}
