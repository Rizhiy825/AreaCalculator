using FluentValidation;

namespace AreaCalculator;

internal class CircleValidator : AbstractValidator<Circle>
{
    public CircleValidator()
    {
        RuleFor(circle => circle.Radius).GreaterThan(0);
    }
}