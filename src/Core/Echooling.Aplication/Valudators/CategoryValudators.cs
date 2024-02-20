using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecooling.Domain.Entites;
using FluentValidation;

namespace Echooling.Aplication.Valudators
{
    public class CategoryValudators : AbstractValidator<EventCategoryies>
    {
        public CategoryValudators()
        {
            RuleFor(x => x.Category).MaximumLength(100).NotEmpty().NotNull();
        }
    }
}
