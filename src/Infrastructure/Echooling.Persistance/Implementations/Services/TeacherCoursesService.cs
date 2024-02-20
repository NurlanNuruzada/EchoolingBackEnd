using AutoMapper;
using Echooling.Aplication.Abstraction.Repository.Couse;
using Echooling.Aplication.Abstraction.Repository.EventRepositories;
using Echooling.Aplication.Abstraction.Repository.StaffRepositories;
using Echooling.Aplication.Abstraction.Repository.TeacherCourses;
using Echooling.Aplication.Abstraction.Repository.TeacherRepositories;
using Echooling.Aplication.Abstraction.Services;
using Echooling.Persistance.Exceptions;
using Echooling.Persistance.Implementations.Repositories.EventRepositories;
using Echooling.Persistance.Implementations.Repositories.StaffRepository;
using Echooling.Persistance.Resources;
using Ecooling.Domain.Entites;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Echooling.Persistance.Implementations.Services
{
    public class TeacherCoursesService : ITeacherCourses
    {
        private readonly ITeacherCoursesWriteRepository _writeRepository;
        private readonly ITeacherCoursesWriteRepository _readRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly ICourseReadRepository _CourseReadRepsotory;
        private readonly ICourseWriteRepository _CourseWriteRepository;
        private readonly ITeacherReadRepository _ITeaherReadRepository;

        public TeacherCoursesService(ITeacherCoursesWriteRepository writeRepository,
                                     ITeacherCoursesWriteRepository readRepository,
                                     IMapper mapper,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     ICourseReadRepository courseReadRepsotory,
                                     ICourseWriteRepository courseWriteRepository,
                                     ITeacherReadRepository iTeaherReadRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
            _localizer = localizer;
            _CourseReadRepsotory = courseReadRepsotory;
            _CourseWriteRepository = courseWriteRepository;
            _ITeaherReadRepository = iTeaherReadRepository;
        }

        public async Task AddCourseToTeacherAsync(Guid CourseId, Guid TeacherId)
        {
            var Course = await _CourseReadRepsotory.GetByExpressionAsync(u => u.GuId == CourseId);
            string message = _localizer.GetString("NotFoundExceptionMsg");
            if (Course == null)
            {
                throw new notFoundException("Course " + message);
            }

            var Teacher = await _ITeaherReadRepository.GetByIdAsync(TeacherId);
            if (Teacher == null)
            {
                throw new notFoundException("Teacher " + message);
            }
            if (Course.TeacherDetailsCourses == null)
            {
                Course.TeacherDetailsCourses = new List<TeacherDetails_Courses>();
            }

            var TeaherCourse = new TeacherDetails_Courses
            {
                CourseId = CourseId,
                teacherDetailsId = TeacherId,
                Course = _mapper.Map<Course>(Course),
                teacherDetails = _mapper.Map<teacherDetails>(Teacher)
            };

            Course.TeacherDetailsCourses.Add(TeaherCourse);
            await _writeRepository.SaveChangesAsync();
        }
    }
}
