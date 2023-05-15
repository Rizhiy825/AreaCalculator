using FluentValidation;

namespace AreaCalculator
{
    public class Circle : IShape
    {
        public readonly double Radius;

        public Circle(double radius)
        {
            Radius = radius;

            var validator = new CircleValidator();
            validator.ValidateAndThrow(this);
        }

        public double CalculateArea()
        {
            var area = Math.PI * Radius * Radius;

            return area;
        }
    }
}