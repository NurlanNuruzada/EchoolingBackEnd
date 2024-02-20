using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Aplication.Abstraction.Repository.VideoRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Aplication.DTOs.VideoDTOs;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Echooling.Persistance.Implementations.Services
{
    public class VideoService : IVideoContentService
    {
        private readonly IMapper _mapper;
        private readonly IVideoContentReadRepository _readRepository;
        private readonly IVideoContentWriteRepository _writeRepository;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICourseReadRepository _courseReadRepo;

        public VideoService(IMapper mapper,
                            IVideoContentReadRepository readRepository,
                            IVideoContentWriteRepository writeRepository,
                            IStringLocalizer<ErrorMessages> localizer,
                            IWebHostEnvironment hostingEnvironment,
                            ICourseReadRepository courseReadRepo)
        {
            _mapper = mapper;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _localizer = localizer;
            _hostingEnvironment = hostingEnvironment;
            _courseReadRepo = courseReadRepo;
        }

        public async Task CreateAsync(CreateVIdeoContentDto CreateDto, Guid CourseId)
        {
            if (CreateDto.Video == null)
            {
                throw new notFoundException("No image file provided.");
            }
            var course = _courseReadRepo.GetByIdAsync(CourseId);
            if (course == null)
            {
                throw new notFoundException("Course not Found!");
            }
            // Dynamically determine the root directory
            string currentDirectory = Directory.GetCurrentDirectory();
            int index = currentDirectory.IndexOf("FinalApp\\");
            if (index >= 0)
            {
                currentDirectory = currentDirectory.Substring(0, index + 8); // +8 to include "FinalApp\"
            }
            string uploadsRootDirectory = Path.Combine(currentDirectory, "FrontEnd", "echooling", "public", "Uploads", "Course", "Videos");
            //end
            Directory.CreateDirectory(uploadsRootDirectory);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (CreateDto.Video is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(CreateDto.Video.FileName);
                string filePath = Path.Combine(uploadsRootDirectory, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await CreateDto.Video.CopyToAsync(fileStream);
                }

                var Video = _mapper.Map<VideoContent>(CreateDto);
                Video.courseId = CourseId;
                Video.VideoUniqueName = fileName;
                await _writeRepository.addAsync(Video);
                await _writeRepository.SaveChangesAsync();
            }
            if (CreateDto.Video is null)
            {
                throw new notFoundException("video" + message);
            }
        }

        public async Task<List<GetVideoContentDto>> GetAllAsync()
        {
            var Video = await _readRepository.GetAll().ToListAsync();
            List<GetVideoContentDto> List = _mapper.Map<List<GetVideoContentDto>>(Video);

            foreach (GetVideoContentDto VideoDto in List)
            {
                VideoDto.VideoUniqueName = $"{VideoDto.VideoUniqueName}";
            }
            return List;
        }

        public async Task<List<GetVideoContentDto>> GetVideosByCourseId(Guid courseId, int take)
        {
            var Videos = await _readRepository.GetAll().Where(c => c.courseId == courseId).ToListAsync();

            if (take > 0)
            {
                Videos = Videos.Take(take).ToList(); 
            }
            var VideoDto = _mapper.Map<List<GetVideoContentDto>>(Videos);
            return VideoDto;
        }

        public async Task Remove(Guid id)
        {
            VideoContent Video = await _readRepository.GetByIdAsync(id);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Video is null)
            {
                throw new notFoundException(message);
            }
            _writeRepository.remove(Video);
            await _writeRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CreateVIdeoContentDto UpdateDto, Guid id)
        {
            string message = _localizer.GetString("NotFoundExceptionMsg");
            var Video = await _readRepository.GetByIdAsync(id);
            if (Video is null)
            {
                throw new notFoundException(message);
            }
            _mapper.Map(UpdateDto, Video);
            await _writeRepository.SaveChangesAsync();
        }
    }
}
