using System.Text;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.SliderRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Echooling.Persistance.Implementations.Services
{
    public class SliderServices : ISliderService
    {
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IMapper _mapper;
        private readonly ISliderReadRepository _readRepository;
        private readonly ISliderWriteRepository _writeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SliderServices(ISliderWriteRepository writeRepository, ISliderReadRepository readRepository, IMapper mapper, IStringLocalizer<ErrorMessages> localizer, IWebHostEnvironment hostingEnvironment)
        {

            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _localizer = localizer;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task CreateAsync(SliderCreateDto categoryCreateDto)
        {
            if (categoryCreateDto.image == null)
            {
                throw new Exception("No image file provided.");
            }

            Slider newSlider = _mapper.Map<Slider>(categoryCreateDto);
            // Dynamically determine the root directory
            string currentDirectory = Directory.GetCurrentDirectory();
            int index = currentDirectory.IndexOf("FinalApp\\");
            if (index >= 0)
            {
                currentDirectory = currentDirectory.Substring(0, index + 8); // +8 to include "FinalApp\"
            }
            string uploadsRootDirectory = Path.Combine(currentDirectory, "FrontEnd", "echooling", "public", "Uploads", "Sliders");
            Directory.CreateDirectory(uploadsRootDirectory);
            //end
            if (categoryCreateDto.image is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryCreateDto.image.FileName);
                string filePath = Path.Combine(uploadsRootDirectory, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await categoryCreateDto.image.CopyToAsync(fileStream);
                }

                newSlider.ImageRoutue = fileName;
            }

            await _writeRepository.addAsync(newSlider);
            await _writeRepository.SaveChangesAsync();
        }

        public async Task<List<SliderGetDto>> GetAllAsync()
        {
            var Sliders = await _readRepository.GetAll().Where(s=>s.IsDeleted == false).ToListAsync();
            List<SliderGetDto> List = _mapper.Map<List<SliderGetDto>>(Sliders);
            foreach (SliderGetDto sliderDto in List)
            {
                sliderDto.ImageRoutue = $"{sliderDto.ImageRoutue}";
            }
            return List;
        }
        public async Task<SliderGetDto> getById(Guid id)
        {
            string message = _localizer.GetString("NotFoundExceptionMsg");
            Slider slider = await _readRepository.GetByIdAsync(id);
            if (slider is null)
            {
                throw new notFoundException(message);
            }
            if (slider.IsDeleted == true)
            {
                throw new notFoundException(message);
            }
            else
            {
                SliderGetDto sliderGetDto = _mapper.Map<SliderGetDto>(slider);
                sliderGetDto.ImageRoutue = slider.ImageRoutue;
                return sliderGetDto;
            }
        }
        public async Task Remove(Guid id)
        {
            Slider slider = await _readRepository.GetByIdAsync(id);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (slider is null)
            {
                throw new notFoundException(message);
            }
            slider.IsDeleted = true;
            await _writeRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(SliderUpdateDto sldierUpdateDto, Guid id)
        {
            var Slider = await _readRepository.GetByIdAsync(id);
            var ImageRoutue = Slider.ImageRoutue;
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Slider is null)
            {
                throw new notFoundException(message);
            }
            // Dynamically determine the root directory
            string currentDirectory = Directory.GetCurrentDirectory();
            int index = currentDirectory.IndexOf("FinalApp\\");
            if (index >= 0)
            {
                currentDirectory = currentDirectory.Substring(0, index + 8); // +8 to include "FinalApp\"
            }
            string uploadsRootDirectory = Path.Combine(currentDirectory, "FrontEnd", "echooling", "public", "Uploads", "Sliders");
            Directory.CreateDirectory(uploadsRootDirectory);
            //end
            _mapper.Map(sldierUpdateDto, Slider);
            if (sldierUpdateDto.image is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sldierUpdateDto.image.FileName);
                string filePath = Path.Combine(uploadsRootDirectory, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await sldierUpdateDto.image.CopyToAsync(fileStream);
                }

                Slider.ImageRoutue = fileName;
            }
            await _writeRepository.SaveChangesAsync();
        }
    }
}
