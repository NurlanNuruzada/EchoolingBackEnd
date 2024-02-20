using System.Web;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs;
using Echooling.Aplication.DTOs.EmailDTOs;
using Echooling.Aplication.DTOs.StaffDTOs;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Helper;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Ecooling.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Echooling.Persistance.Implementations.Services
{
    public class StaffService : IStaffService
    {
        public readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStaffReadRepository _readRepository;
        private readonly IStaffWriteRepository _writeRepository;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITeacherReadRepository _readRepo;
        private readonly  ILoggerService _loggerService;
        private readonly IEmailService _emailService;
        public StaffService(AppDbContext context,
                            IMapper mapper,
                            UserManager<AppUser> userManager,
                            IStaffReadRepository readRepository,
                            IStaffWriteRepository writeRepository,
                            IStringLocalizer<ErrorMessages> localizer,
                            RoleManager<IdentityRole> roleManager,
                            ITeacherReadRepository readRepo,
                            ILoggerService loggerService,
                            IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _localizer = localizer;
            _roleManager = roleManager;
            _readRepo = readRepo;
            _loggerService = loggerService;
            _emailService = emailService;
        }

        public async Task CreateAsync(CreateStaffDto createStaff, Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var IsAlreadyStaff = await _readRepository.GetByExpressionAsync(S => S.AppUserID == UserId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (user is null)
            {
                throw new notFoundException("user" + " " + message);
            }
            Staff staff = _mapper.Map<Staff>(createStaff);
            var result = await _userManager.AddToRoleAsync(user, Roles.Staff.ToString());
            staff.AppUserID = UserId;
            staff.PhoneNumber = user.PhoneNumber;
            staff.Fullname = user.Fullname;
            staff.emailAddress = user.Email;
            staff.UserName = user.UserName;
            staff.Role = "Staff";
            await _writeRepository.addAsync(staff);
            await _writeRepository.SaveChangesAsync();
        }

        public async Task<List<GetUserListDto>> GetAllAsync()
        {
            var staff = await _readRepository.GetAll().Where(u=>u.IsDeleted==false).ToListAsync();
            var staffDtos = _mapper.Map<List<GetUserListDto>>(staff);

            var teachers = await _readRepo.GetAll().Where(u => u.IsDeleted == false).ToListAsync();
            var teacherDtos = _mapper.Map<List<GetUserListDto>>(teachers);

            var combinedList = staffDtos.Concat(teacherDtos).ToList();

            return combinedList;
        }
        public async Task<GetStaffDto> getById(Guid UserId)
        {
            var Staff = await _readRepository.GetByExpressionAsync(u => u.AppUserID == UserId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Staff is null)
            {
                Staff = await _readRepository.GetByIdAsync(UserId);
                if (Staff is null)
                {
                    throw new notFoundException("user" + " " + message);
                }
            }
            GetStaffDto FoundStaff = _mapper.Map<GetStaffDto>(Staff);
            return FoundStaff;
        }
        public async Task Remove(Guid UserId,Guid AppUserDeletedById)
        {
            var Staff = await _readRepository.GetByIdAsync(UserId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Staff is null)
            {
                throw new notFoundException(message);
            }
            var Admin = await _userManager.FindByIdAsync(AppUserDeletedById.ToString());
            Staff.IsDeleted = true;
            await _writeRepository.SaveChangesAsync();
            CreateLogDto logDto = new CreateLogDto();
            logDto.ActionTime = DateTime.Now;
            logDto.ActiondEntityName = "Remove";
            logDto.UserId = Admin.UserName;
            logDto.ActiondEntityId = Staff.UserName;
            _loggerService.CreateLog(logDto);
        }
        public async Task UpdateAsync(CreateStaffDto updateDto, Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var Staff = await _readRepository.GetByExpressionAsync(u => u.AppUserID == UserId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Staff is null)
            {
                throw new notFoundException("user" + " " + message);
            }
            _mapper.Map(updateDto, Staff);
            await _writeRepository.SaveChangesAsync();
        }

        public async Task ApproveStaff(Guid StaffId,Guid ApprovePersonId)
        {

            var Staff = await _readRepository.GetByIdAsync(StaffId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Staff is null)
            {
                throw new notFoundException("Staff" + message);
            }
            Staff.IsApproved = true;

            await _writeRepository.SaveChangesAsync();
            var Admin = await _userManager.FindByIdAsync(ApprovePersonId.ToString());
            CreateLogDto logDto = new CreateLogDto();
            logDto.ActionTime = DateTime.Now;
            logDto.ActiondEntityName = "Approve";
            logDto.UserId = Admin.UserName;
            logDto.ActiondEntityId = Staff.UserName;
            _loggerService.CreateLog(logDto);

            var FrontEndBase = "http://localhost:3000";
            var userIp = EmailConfigurations.GetUserIP().ToString();
            var confirmationUrl = $"{FrontEndBase}/";

            SentEmailDto ConfirmLetter = new SentEmailDto
            {
                To = Staff.emailAddress,
                Subject = "Confirm Email Address",
                body = $"<html><body>" +
                $"<h1>Welcome , <span style='color: #3270fc;'>{Staff.Fullname}</span></h1>" +
                $"<h2>Confirm Your Email</h2>" +
                $"<p>You Succesfully Approved you can Create Content form this link <a href='{confirmationUrl}'>here</a>. If it's not you, you can ignore this email.</p>" +
                $"<br/>" +
                $"<h3> we received this from {userIp}</h3>" +
                $"</body></html>"
            };
            _emailService.SendEmail(ConfirmLetter);
        }
    }
}
