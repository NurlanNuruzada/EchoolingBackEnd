using System.Text;
using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.DTOs.EmailDTOs;
using Echooling.Aplication.DTOs;
using Echooling.Aplication.DTOs.TeacherDetailsDTOs;
using Echooling.Persistance.Contexts;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Helper;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Ecooling.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Echooling.Aplication.DTOs.SliderDTOs;

namespace Echooling.Persistance.Implementations.Services
{
    public class TeacherServices : ITeacherService
    {
        public readonly AppDbContext _context;
        public readonly ITeacherReadRepository _readRepo;
        public readonly ITeacherWriteRepository _writeRepo;
        public readonly IStaffReadRepository _staffService;
        public readonly IMapper _mapper;
        public readonly IStringLocalizer<ErrorMessages> _stringLocalizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggerService _loggerService;
        private readonly IEmailService _emailService;

        public TeacherServices(AppDbContext context,
                               ITeacherReadRepository readRepo,
                               ITeacherWriteRepository writeRepo,
                               IStaffReadRepository staffService,
                               IMapper mapper,
                               IStringLocalizer<ErrorMessages> stringLocalizer,
                               UserManager<AppUser> userManager,
                               ILoggerService loggerService,
                               IEmailService emailService)
        {
            _context = context;
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _staffService = staffService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _loggerService = loggerService;
            _emailService = emailService;
        }

        public async Task CreateAsync(TeacherCreateDto teacherCreateDto, Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            string message = _stringLocalizer.GetString("NotFoundExceptionMsg");
            if (user is null)
            {
                throw new notFoundException("user" + " " + message);
            }
            teacherDetails teacher = _mapper.Map<teacherDetails>(teacherCreateDto);
            var result = await _userManager.AddToRoleAsync(user, Roles.Teacher.ToString());
            teacher.AppUserID = UserId;
            teacher.PhoneNumber = user.PhoneNumber;
            teacher.Fullname = user.Fullname;
            teacher.emailAddress = user.Email;
            teacher.UserName = user.UserName;
            teacher.Role = "Teacher";
            teacher.IsApproved = false;
            await _writeRepo.addAsync(teacher);
            await _writeRepo.SaveChangesAsync();
        }
        public async Task<List<TeacherGetDto>> GetAllAsync()
        {
            var teachers = await _readRepo.GetAll().ToListAsync();
            List<TeacherGetDto> List = _mapper.Map<List<TeacherGetDto>>(teachers);
            foreach (TeacherGetDto teacher in List)
            {
                teacher.ImageRoutue = $"{teacher.ImageRoutue}";
            }
            return List;
        }

        public async Task<TeacherGetDto> getById(Guid UserId)
        {
            var teachers = await _readRepo.GetByIdAsync(UserId);
            string message = _stringLocalizer.GetString("NotFoundExceptionMsg");
            if (teachers is null)
            {
                teachers = await _readRepo.GetByExpressionAsync(u => u.AppUserID == UserId);
                if (teachers is null)
                {
                    throw new notFoundException("user" + " " + message);
                }
            }
            TeacherGetDto teacher = _mapper.Map<TeacherGetDto>(teachers);
            teacher.ImageRoutue = teachers.ImageRoutue;
            return teacher;
        }
        public async Task ApproveTeacher(Guid TeacherId, Guid ApprovePersonId)
        {

            var Teacher = await _readRepo.GetByIdAsync(TeacherId);
            string message = _stringLocalizer.GetString("NotFoundExceptionMsg");
            if (Teacher is null)
            {
                throw new notFoundException("teacher" + message);
            }
            Teacher.IsApproved = true;

            await _writeRepo.SaveChangesAsync();
            var Admin = await _userManager.FindByIdAsync(ApprovePersonId.ToString());
            CreateLogDto logDto = new CreateLogDto();
            logDto.ActionTime = DateTime.Now;
            logDto.ActiondEntityName = "Approve";
            logDto.UserId = Admin.UserName;
            logDto.ActiondEntityId = Teacher.UserName;
            _loggerService.CreateLog(logDto);

            var FrontEndBase = "http://localhost:3000";
            var userIp = EmailConfigurations.GetUserIP().ToString();
            var confirmationUrl = $"{FrontEndBase}/";

            SentEmailDto ConfirmLetter = new SentEmailDto
            {
                To = Teacher.emailAddress,
                Subject = "Confirm Email Address",
                body = $"<html><body>" +
                $"<h1>Welcome , <span style='color: #3270fc;'>{Teacher.Fullname}</span></h1>" +
                $"<h2>Confirm Your Email</h2>" +
                $"<p>You Succesfully Approved you can Create Content form this link <a href='{confirmationUrl}'>here</a>. If it's not you, you can ignore this email.</p>" +
                $"<br/>" +
                $"<h3> we received this from {userIp}</h3>" +
                $"</body></html>"
            };
            _emailService.SendEmail(ConfirmLetter);
        }
        public async Task Remove(Guid UserId, Guid AppUserDeletedById)
        {
            var teachers = await _readRepo.GetByIdAsync(UserId);
            string message = _stringLocalizer.GetString("NotFoundExceptionMsg");
            if (teachers is null)
            {
                throw new notFoundException(message);
            }
            teachers.IsDeleted = true;
            await _writeRepo.SaveChangesAsync();

            var Admin = await _userManager.FindByIdAsync(AppUserDeletedById.ToString());
            CreateLogDto logDto = new CreateLogDto();
            logDto.ActionTime = DateTime.Now;
            logDto.ActiondEntityName = "Remove";
            logDto.UserId = Admin.UserName;
            logDto.ActiondEntityId = teachers.UserName;
            _loggerService.CreateLog(logDto);
        }
        public async Task UpdateAsync(TeacherUpdateDto updateDto, Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var teacher = await _readRepo.GetByExpressionAsync(u => u.AppUserID == UserId);
            string message = _stringLocalizer.GetString("NotFoundExceptionMsg");
            if (teacher is null)
            {
                throw new notFoundException("user" + " " + message);
            }
            // Dynamically determine the root directory
            string currentDirectory = Directory.GetCurrentDirectory();
            int index = currentDirectory.IndexOf("FinalApp\\");
            if (index >= 0)
            {
                currentDirectory = currentDirectory.Substring(0, index + 8); // +8 to include "FinalApp\"
            }
            string uploadsRootDirectory = Path.Combine(currentDirectory, "FrontEnd", "echooling", "public", "Uploads", "TeacherImages");
            Directory.CreateDirectory(uploadsRootDirectory);
            //end
            _mapper.Map(updateDto, teacher);
            if (updateDto.image is not null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateDto.image.FileName);
                string filePath = Path.Combine(uploadsRootDirectory, fileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await updateDto.image.CopyToAsync(fileStream);
                }

                teacher.ImageRoutue = fileName;
            }
            _mapper.Map(updateDto, teacher);
            await _writeRepo.SaveChangesAsync();
        }
    }
}
