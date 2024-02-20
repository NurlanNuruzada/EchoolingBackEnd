using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.CourseCategory;
using Echooling.Aplication.Abstraction.Repository.CourseReviewRepositories;
using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.CourseDTOs;
using Echooling.Aplication.DTOs.CourseReviewDTOs;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Echooling.Persistance.Implementations.Services
{
    public class CourseReviewService : ICourseReviewServices
    {
        private readonly ICourseReviewWriteRepository _WriteRepository;
        private readonly ICourseReviewReadRepository _ReadRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly ICourseReadRepository _CourseReadReadRepo;
        private readonly ICourseWriteRepository _CourseWriteRepository;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CourseReviewService(ICourseReviewWriteRepository writeRepository,
                                   ICourseReviewReadRepository readRepository,
                                   IMapper mapper,
                                   IStringLocalizer<ErrorMessages> localizer,
                                   ICourseReadRepository courseReadReadRepo,
                                   ICourseWriteRepository courseWriteRepository,
                                   AppDbContext context = null,
                                   UserManager<AppUser> userManager = null)
        {
            _WriteRepository = writeRepository;
            _ReadRepository = readRepository;
            _mapper = mapper;
            _localizer = localizer;
            _CourseReadReadRepo = courseReadReadRepo;
            _CourseWriteRepository = courseWriteRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task AddReview(CreateCourseReviewDto review)
        {
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (review is null)
            {
                throw new notFoundException("review" + " " + message);
            }
            var CreateReview = _mapper.Map<CourseReview>(review);
            CreateReview.IsDeleted = false;
            await _WriteRepository.addAsync(CreateReview);
            await _WriteRepository.SaveChangesAsync();
            //CreateCourse rate
            decimal TotalRate = 0;
            int totalRateCount = 0;

            Course Course = await _CourseReadReadRepo.GetByIdAsync(review.CourseId);
            var AllReview = await _ReadRepository.GetAll()

            .Where(r => r.IsDeleted == false && r.CourseId == review.CourseId)
            .ToListAsync();
            AllReview.ForEach(OneRate =>
            {
                if (OneRate.IsDeleted == false)
                {
                    TotalRate += OneRate.rate;
                    totalRateCount += 1;
                }

            });

            Course.Rate = TotalRate / totalRateCount;
            await _CourseWriteRepository.SaveChangesAsync();
        }

        public async Task Delete(Guid ReviewId, Guid userId)
        {
            var review = await _ReadRepository.GetByIdAsync(ReviewId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (review is null)
            {
                throw new notFoundException("review" + " " + message);
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (review.UserId != userId && !await _userManager.IsInRoleAsync(user, "admin") && !await _userManager.IsInRoleAsync(user, "superadmin"))
            {
                throw new NoAcsessException("This review can't be deleted by this user!");
            }
            review.IsDeleted = true;
            await _WriteRepository.SaveChangesAsync();
        }

        public async Task<GetCourseReviewDto> getbyId(Guid CourseId)
        {
            var review = await _ReadRepository.GetByIdAsync(CourseId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (review is null && review.isEdited == true)
            {
                throw new notFoundException("review" + " " + message);
            }
            var fountReview = _mapper.Map<GetCourseReviewDto>(review);
            return fountReview;
        }

        public async Task<List<GetCourseReviewDto>> getReviewsOfCourseById(Guid CourseId)
        {
            var review = await _ReadRepository.GetAll()
                .Where(r => r.IsDeleted == false && r.CourseId == CourseId)
                .ToListAsync();
            var List = _mapper.Map<List<GetCourseReviewDto>>(review);
            return List;
        }

        public async Task UpdateAsync(UpdateCourseReviewDto reviewDto, Guid ReviewId, Guid userId)
        {
            var Review = await _ReadRepository.GetByIdAsync(ReviewId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Review is null && Review?.UserId != userId)
            {
                throw new notFoundException("review" + " " + message);
            }
            Review.rate = reviewDto.rate;
            Review.Comment =reviewDto.Comment;
            Review.isEdited = true;
            await _WriteRepository.SaveChangesAsync();

            decimal TotalRate = 0;
            int totalRateCount = 0;

            Course Course = await _CourseReadReadRepo.GetByIdAsync(reviewDto.CourseId);
            var AllReview = await _ReadRepository.GetAll()

            .Where(r => r.IsDeleted == false && r.CourseId == reviewDto.CourseId)
            .ToListAsync();
            AllReview.ForEach(OneRate =>
            {
                if (OneRate.IsDeleted == false)
                {
                    TotalRate += OneRate.rate;
                    totalRateCount += 1;
                }

            });
            TotalRate -= Review.rate;
            TotalRate += reviewDto.rate;
            Course.Rate = TotalRate / totalRateCount;
            await _CourseWriteRepository.SaveChangesAsync();
        }
    }
}
