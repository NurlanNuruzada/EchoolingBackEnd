using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.DTOs.CourseReviewDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Persistance.MapperProfile
{
    public class CourseReviewProfile:Profile
    {
        public CourseReviewProfile()
        {
            CreateMap<CourseReview , CreateCourseReviewDto>().ReverseMap();
            CreateMap<CourseReview , GetCourseReviewDto>().ReverseMap();
            CreateMap<CourseReview , UpdateCourseReviewDto>().ReverseMap();
        }
    }
}
