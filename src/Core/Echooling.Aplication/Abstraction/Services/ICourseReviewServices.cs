using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs.CourseReviewDTOs;
using Ecooling.Domain.Entites;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface ICourseReviewServices
    {
        Task AddReview(CreateCourseReviewDto review);
        Task <List<GetCourseReviewDto>> getReviewsOfCourseById(Guid CourseId);
        Task <GetCourseReviewDto> getbyId(Guid CourseId);
        Task  Delete(Guid ReviewId, Guid userId);
        Task UpdateAsync(UpdateCourseReviewDto review, Guid ReviewId,Guid userId);    
    }
}
