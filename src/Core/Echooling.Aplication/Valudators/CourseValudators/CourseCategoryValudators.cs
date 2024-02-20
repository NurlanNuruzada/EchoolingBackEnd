using Ecooling.Domain.Entites;
using FluentValidation;

namespace Echooling.Aplication.Valudators.CourseValudators;
public class CourseCategory : AbstractValidator<CourseCategories>
{
    public CourseCategory()
    {
        RuleFor(x => x.Category).MaximumLength(100).NotEmpty().NotNull();
    }
}
