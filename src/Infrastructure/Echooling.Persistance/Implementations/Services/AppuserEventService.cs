using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository;
using Echooling.Aplication.Abstraction.Repository.AppUserEventRepository;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs;
using Echooling.Aplication.DTOs.SliderDTOs;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Echooling.Persistance.Implementations.Services
{
    public class AppuserEventService : IAppUserEventService
    {
        private readonly IAppuserEventReadRopository _readRepository;
        private readonly IAppuserEventWriteRepository _writeRepository;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IMapper _mapper;

        public AppuserEventService(IAppuserEventReadRopository readRepository,
                                   IAppuserEventWriteRepository writeRepository,
                                   IStringLocalizer<ErrorMessages> localizer,
                                   IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task CreateAsync(AppUserEventDto categoryCreateDto)
        {
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (categoryCreateDto is null)
            {
                throw new notFoundException(message);
            }
            categoryCreateDto.eventsId = new Guid(categoryCreateDto.eventsId.ToString());
            string guid = categoryCreateDto.AppUserId.ToString();
            AppUser_Events userEvents = _mapper.Map<AppUser_Events>(categoryCreateDto);
            await _writeRepository.addAsync(userEvents);
            await _writeRepository.SaveChangesAsync();
        }
    }
}
