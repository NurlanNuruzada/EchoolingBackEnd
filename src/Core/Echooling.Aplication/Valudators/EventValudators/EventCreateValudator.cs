using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Echooling.Aplication.DTOs.SliderDTOs;
using Ecooling.Domain.Entites;
using FluentValidation;

namespace Echooling.Aplication.Valudators.EventValudators
{
    public class CourseCreateValudator : AbstractValidator<events>
    {
        public CourseCreateValudator()
        {
            RuleFor(x => x.EventTitle).MaximumLength(20).NotEmpty().NotNull();
            RuleFor(x => x.AboutEvent).MaximumLength(400).NotEmpty().NotNull();
            RuleFor(x => x.Location).MaximumLength(100).NotEmpty().NotNull();
            RuleFor(x => x.AboutEvent).MaximumLength(400).NotEmpty().NotNull();
        }
    }
}
