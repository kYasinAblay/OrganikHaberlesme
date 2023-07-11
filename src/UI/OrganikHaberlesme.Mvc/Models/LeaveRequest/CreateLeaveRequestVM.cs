﻿using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrganikHaberlesme.Mvc.Models.LeaveRequest
{
    public class CreateLeaveRequestVm
    {
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        public SelectList? LeaveTypes { get; set; }

        [Display(Name = "Leave Type")]
        [Required]
        [Range(1, int.MaxValue)]
        public int LeaveTypeId { get; set; }

        [Display(Name = "Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
    }
}
