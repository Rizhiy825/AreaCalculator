using FluentValidation;

namespace AreaCalculator;

/* Используя FluentValidation, можно очень удобно и читаемо определять правила
валидации. */
internal class TriangleValidator : AbstractValidator<Triangle>
{
    public TriangleValidator()
    {
        RuleFor(triangle => triangle.SideA).GreaterThan(0);
        RuleFor(triangle => triangle.SideB).GreaterThan(0);
        RuleFor(triangle => triangle.SideC).GreaterThan(0);

        RuleFor(triangle => triangle)
            .Must(BeValidTriangle)
            .WithMessage("Invalid triangle sides!");
    }

    private bool BeValidTriangle(Triangle triangle)
    {
        return triangle.SideA + triangle.SideB > triangle.SideC &&
               triangle.SideB + triangle.SideC > triangle.SideA &&
               triangle.SideA + triangle.SideC > triangle.SideB;
    }
}