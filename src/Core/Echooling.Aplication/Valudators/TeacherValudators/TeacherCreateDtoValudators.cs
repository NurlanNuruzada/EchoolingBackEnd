using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs.TeacherDetailsDTOs;
using FluentValidation;

namespace Echooling.Aplication.Valudators.TeacherValudators
{
    public class TeacherCreateDtoValudators:AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValudators()
        {
            RuleFor(x=>x.faculty).MaximumLength(100).NotEmpty().NotNull();
            RuleFor(x => x.linkedin).MaximumLength(100);
            RuleFor(x => x.faculty).MaximumLength(100);
            RuleFor(x => x.twitter).MaximumLength(100);
            RuleFor(x => x.instagram).MaximumLength(100);
            RuleFor(x => x.profession).MaximumLength(100);
        }
    }
}
