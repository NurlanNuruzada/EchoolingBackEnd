using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Aplication.Abstraction.Repository.EventsStaff;
using Echooling.Aplication.Abstraction.Repository.SliderRepositories;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.StaffDTOs;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Echooling.Persistance.Implementations.Services
{
    public class EventStaffServices : IStaffEventsService
    {
        private readonly IEventStaffWriteRepository _writeRepository;
        private readonly IEventStaffReadRepository _readRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IEventWriteRepository _EventwriteRepository;
        private readonly IEventReadRepository _EventreadRepository;
        private readonly IStaffReadRepository _StaffreadRepository;
        private readonly IStaffWriteRepository _StaffwriteRepository;

        public EventStaffServices(IEventStaffWriteRepository writeRepository,
                                  IEventStaffReadRepository readRepository,
                                  IMapper mapper,
                                  IStringLocalizer<ErrorMessages> localizer,
                                  IEventReadRepository eventreadRepository,
                                  IStaffReadRepository staffreadRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _localizer = localizer;
            _EventreadRepository = eventreadRepository;
            _StaffreadRepository = staffreadRepository;
        }

        public async Task AddStaffToEventAsync(Guid eventId, Guid staffId)
        {
            var eventEntity = await _EventreadRepository.GetByExpressionAsync(u => u.GuId == eventId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (eventEntity == null)
            {
                throw new notFoundException("Event " + message);
            }

            var Staff = await _StaffreadRepository.GetByIdAsync(staffId);
            if (Staff == null)
            {
                throw new notFoundException("Staff " + message);
            }
            if (eventEntity.StaffEvents == null)
            {
                eventEntity.StaffEvents = new List<Staff_Events>();
            }

            var staffEvent = new Staff_Events
            {
                StaffId = staffId,
                eventsId = eventId,
                events = _mapper.Map<events>(eventEntity),
                staff = _mapper.Map<Staff>(Staff)
            };

            eventEntity.StaffEvents.Add(staffEvent);

            await _writeRepository.SaveChangesAsync();
        }



        public Task<List<Staff_Events>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GetStaffDto>> GetByEventOrStaffId(Guid eventId)
        {
            var staffEvents = await _readRepository.GetAll()
              .Where(se => se.eventsId == eventId)
              .Include(se => se.staff) // Include the related Staff entity
              .ToListAsync();

            List<GetStaffDto> staffList = staffEvents
                .Select(se => _mapper.Map<GetStaffDto>(se.staff))
                .ToList();

            return staffList;
        }
        public Task UpdateAsync(Staff_Events StaffEvents, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
