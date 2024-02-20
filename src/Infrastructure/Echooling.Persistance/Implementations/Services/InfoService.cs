using System;
using System.Linq;
using System.Threading.Tasks;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Persistance.Contexts;
using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Echooling.Persistance.Implementations.Services
{
    public class InfoService : IInfoService
    {
        private readonly UserManager<AppUser> _userManager;
        public readonly AppDbContext _context;

        public InfoService(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Info> getInfo()
        {
            var totalUsersCount = await _userManager.Users.CountAsync();
            var totalCoursesCount = await _context.Courses.CountAsync();
            var totalTeachersCount = await _context.TeacherDetails.CountAsync();
            var totalEventCount = await _context.Events.CountAsync();
            var info = new Info
            {
                TotalStudentCount = totalUsersCount,
                TotalCoursesCount = totalCoursesCount,
                TotalTeachersCount = totalTeachersCount,
                TotalEventsCount = totalEventCount
            };

            return info;
        }
    }
}
