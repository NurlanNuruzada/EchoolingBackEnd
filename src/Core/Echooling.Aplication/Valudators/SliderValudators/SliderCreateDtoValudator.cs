using Echooling.Aplication.DTOs.SliderDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.Valudators.SliderValudators
{
    public class SliderCreateDtoValudator:AbstractValidator<SliderCreateDto>
    {
        public SliderCreateDtoValudator()
        {
            RuleFor(x => x.Title).MaximumLength(20).NotEmpty().NotNull();
            RuleFor(x=>x.SeccondTile).MaximumLength(50).NotEmpty().NotNull();
            RuleFor(x=>x.Description).MaximumLength(250).NotEmpty().NotNull();   
        }
    }
}
