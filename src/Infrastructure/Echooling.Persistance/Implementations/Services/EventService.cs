using System.Collections.Generic;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs;
using Echooling.Aplication.DTOs.CategoryDTOs;
using Echooling.Aplication.DTOs.CourseDTOs;
using Echooling.Aplication.DTOs.EventDTOs;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Helper;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Ecooling.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Echooling.Persistance.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEventWriteRepository _writeRepository;
        private readonly IEventReadRepository _readRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        public readonly IStaffEventsService _staffEventsService;
        private readonly IAppUserEventService _AppuserEventService;
        public readonly IAppUserEventService _appUserEventService;
        public IEventsCategoryService _eventsCategoryService;
        private readonly AppDbContext _context;
        private readonly IStaffReadRepository _staffReadRepository;

        public EventService(IEventWriteRepository writeRepository,
                            IEventReadRepository readRepository,
                            IMapper mapper,
                            IStringLocalizer<ErrorMessages> localizer,
                            IStaffEventsService staffEventsService,
                            IAppUserEventService appuserEventService,
                            IAppUserEventService appUserEventService,
                            IWebHostEnvironment env,
                            IEventsCategoryService eventsCategoryService,
                            AppDbContext context,
                            IStaffReadRepository staffReadRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _localizer = localizer;
            _staffEventsService = staffEventsService;
            _AppuserEventService = appuserEventService;
            _appUserEventService = appUserEventService;
            _env = env;
            _eventsCategoryService = eventsCategoryService;
            _context = context;
            _staffReadRepository = staffReadRepository;
        }

        public async Task CreateAsync(EventCreateDto CreateEventDto, Guid UserId)
        {
            if (CreateEventDto.image == null)
            {
                throw new Exception("No image file provided.");
            }
            events EntityEvent = _mapper.Map<events>(CreateEventDto);
            // Dynamically determine the root directory
            string currentDirectory = Directory.GetCurrentDirectory();
            int index = currentDirectory.IndexOf("FinalApp\\");
            if (index >= 0)
            {
                currentDirectory = currentDirectory.Substring(0, index + 8); // +8 to include "FinalApp\"
            }
            string uploadsRootDirectory = Path.Combine(currentDirectory, "FrontEnd", "echooling", "public", "Uploads", "Event");
            Directory.CreateDirectory(uploadsRootDirectory);
            //end
            if (CreateEventDto.image is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(CreateEventDto.image.FileName);
                string filePath = Path.Combine(uploadsRootDirectory, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await CreateEventDto.image.CopyToAsync(fileStream);
                }

                EntityEvent.ImageRoutue = fileName;
            }
            EventCategoryDto eventCategoryDto = new EventCategoryDto();
            await _writeRepository.addAsync(EntityEvent);
            await _writeRepository.SaveChangesAsync();
            await _staffEventsService.AddStaffToEventAsync(EntityEvent.GuId, UserId);
            await _writeRepository.SaveChangesAsync();
        }
        public async Task<List<EventGetDto>> GetAllAsync()
        {
            var Events = await _readRepository.GetAll().Where(e => e.IsDeleted == false).ToListAsync();
            List<EventGetDto> List = _mapper.Map<List<EventGetDto>>(Events);
            foreach (EventGetDto sliderDto in List)
            {
                sliderDto.ImageRoutue = $"{sliderDto.ImageRoutue}";
            }
            return List;
        }
        public async Task<List<EventGetDto>> GetAllAsyncTake(int take)
        {
            var Events = await _readRepository.GetAll().Take(take).Where(e => e.IsDeleted == false).ToListAsync();
            List<EventGetDto> List = _mapper.Map<List<EventGetDto>>(Events);
            foreach (EventGetDto sliderDto in List)
            {
                sliderDto.ImageRoutue = $"{sliderDto.ImageRoutue}";
            }
            return List;
        }
        public async Task<List<EventGetDto>> GetAllSearchAsync(
      string? EventName,
      string? category,
      DateTime? StartDate,
      DateTime? EndDate,
      string? location)
        {
            var Event = _readRepository.GetAll().Where(e => e.IsDeleted == false);

            if (!string.IsNullOrEmpty(EventName))
            {
                Event = Event.Where(x => x.EventTitle.ToLower().Contains(EventName.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                Event = Event.Where(x => x.Categoryname.ToLower() == category.ToLower());
            }

            if (!string.IsNullOrEmpty(location))
            {
                Event = Event.Where(x => x.Location.ToLower().Contains(location.ToLower()));
            }

            if (StartDate.HasValue)
            {
                Event = Event.Where(x => x.EventStartDate >= StartDate );
            }  
            if (EndDate.HasValue)
            {
                Event = Event.Where(x => x.EventFinishDate <= EndDate);
            }

            var queryList = await Event.ToListAsync();

            List<EventGetDto> List = _mapper.Map<List<EventGetDto>>(queryList);

            foreach (EventGetDto courseGetDto in List)
            {
                courseGetDto.ImageRoutue = $"{courseGetDto.ImageRoutue}";
            }

            return List;
        }

        public async Task<EventGetDto> getById(Guid id)
        {
            var Event = await _readRepository.GetByIdAsync(id);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Event is null)
            {
                throw new notFoundException("user" + " " + message);
            }
            if (Event.IsDeleted == true)
            {
                throw new notFoundException(message);
            }
            else
            {
                EventGetDto FoundEvent = _mapper.Map<EventGetDto>(Event);
                FoundEvent.ImageRoutue = Event.ImageRoutue;
                return FoundEvent;
            }
        }
        public async Task Remove(Guid id)
        {
            var Event = await _readRepository.GetByIdAsync(id);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Event is null)
            {
                throw new notFoundException(message);
            }
            Event.IsDeleted = true;
            await _writeRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(EventCreateDto StaffUpdateDto, Guid id)
        {
            var Event = await _readRepository.GetByIdAsync(id);
            var ImageRoutue = Event.ImageRoutue;
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Event is null)
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
            string uploadsRootDirectory = Path.Combine(currentDirectory, "FrontEnd", "echooling", "public", "Uploads", "Event");
            Directory.CreateDirectory(uploadsRootDirectory);
            //end
            _mapper.Map(StaffUpdateDto, Event);
            if (StaffUpdateDto.image is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(StaffUpdateDto.image.FileName);
                string filePath = Path.Combine(uploadsRootDirectory, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await StaffUpdateDto.image.CopyToAsync(fileStream);
                }

                Event.ImageRoutue = fileName;
            }
            await _writeRepository.SaveChangesAsync();
        }
        public async Task BuyEvent(Guid courseId, Guid appUserId)
        {
            var FoundEvent= await _readRepository.GetByIdAsync(courseId);
            if (FoundEvent.TotalSlot <= FoundEvent.Enrolled) 
            {
                throw new NoAcsessException("no Space left!");
            }
            var existingAssociation = await _context.AppUserEvents
                .FirstOrDefaultAsync(cau => cau.eventsId == courseId && cau.AppUserId == appUserId);

            if (existingAssociation != null)
            {
                throw new DublicatedException("you already bouth this!");
            }
            var AppuserEnvet = new AppUser_Events
            {
                eventsId = courseId,
                AppUserId = appUserId
            };
            FoundEvent.Enrolled += 1;
            await _writeRepository.SaveChangesAsync();
            _context.AppUserEvents.Add(AppuserEnvet);
            await _context.SaveChangesAsync();

        }
        //[HttpGet("[Action]")]
        //public async Task<IActionResult> QrCode(Guid appUserId)
        //{
            //List<GetBouthEventDto> events = await _eventService.GetBouthEvent(appUserId);
            //string qrCodeContent = "Your QR Code Content Here";

            //var barcodeWriter = new BarcodeWriterPixelData
            //{
            //    Format = BarcodeFormat.QR_CODE,
            //    Options = new QrCodeEncodingOptions
            //    {
            //        Width = 200,  
            //        Height = 200,
            //    }
            //};

            //var pixelData = barcodeWriter.Write(qrCodeContent);
            //var imageStream = new MemoryStream(pixelData.Pixels);
            //string base64Image = Convert.ToBase64String(imageStream.ToArray());
            //var response = new
            //{
            //    Events = events,
            //    QRCodeImageBase64 = base64Image
            //};

        //    return 
        //}
        public async Task<List<EventGetDto>> getEventsbyStaffId(Guid StaffId)
        {
            var userEvents = await _context.EventStaff
                 .Where(cau => cau.StaffId == StaffId)
                 .Select(cau => cau.events)
                .Where(e => e.IsDeleted == false)
                 .ToListAsync();
            List<EventGetDto> EventList = _mapper.Map<List<EventGetDto>>(userEvents);
            return EventList;
        }
        public async Task<List<EventGetDto>> GetBouthEvents(Guid appUserId)
        {
            var EventList = await _context.EventStaff
                .Where(cau => cau.StaffId == appUserId)
                .Select(cau => cau.events)
                .Where(e=>e.IsDeleted == false)
                .ToListAsync();
            var list = _mapper.Map<List<EventGetDto>>(EventList);
            return list;
        }

        public async Task<List<GetBouthEventDto>> GetBouthEvent(Guid appUserId)
        {
            var EventList = await _context.EventStaff
             .Where(cau => cau.StaffId == appUserId)
             .Select(cau => cau.events)
             .Where(e => e.IsDeleted == false)
             .ToListAsync();
            var list = _mapper.Map<List<GetBouthEventDto>>(EventList);
            return list;
        }
    }

}
