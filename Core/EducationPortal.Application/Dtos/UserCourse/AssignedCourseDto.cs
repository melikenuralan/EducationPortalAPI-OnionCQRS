﻿using EducationPortal.Domain.Entities.Enums;

namespace EducationPortal.Application.Dtos.Profile
{
    public class AssignedCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CourseLevel CourseLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFavorite { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryTitle { get; set; }
    }
}