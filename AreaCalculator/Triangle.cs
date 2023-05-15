using FluentValidation;
using System.Globalization;

namespace AreaCalculator;

public class Triangle : IShape
{
    public readonly double SideA;
    public readonly double SideB;
    public readonly double SideC;
    public Triangle(double sideA, double sideB, double sideC)
    {
        this.SideA = sideA;
        this.SideB = sideB;
        this.SideC = sideC;

        /* Валидируем основные метрики треугольника сразу при его создании.
        Это поможет пользователю сразу узнать, где ошибка. */
        var validator = new TriangleValidator();
        validator.ValidateAndThrow(this);
    }

    /* Внутри метода расчета площади делаем проверку:
    1) Если треугольник прямоугольный, считаем по формуле "половина произведения катетов"
    2) Если треугольник не прямоугольный, считаем по формуле Герона.
     */
    public double CalculateArea()
    {
        double maxSide = GetMaxSide();

        /* Определяем, является ли треугольник прямоугольным. Если да - вычисляем
        его площадь по упрощеной формуле. */
        if (maxSide == SideA && IsRightAngle(SideB, SideC, SideA))
        {
            return CalculateRightTriangleArea(SideB, SideC);
        }
        else if (maxSide == SideB && IsRightAngle(SideA, SideC, SideB))
        {
            return CalculateRightTriangleArea(SideA, SideC);
        }
        else if (IsRightAngle(SideA, SideB, SideC))
        {
            return CalculateRightTriangleArea(SideA, SideB); ;
        }

        // Если треугольник не прямоугольный, решаем с помощью формулы Герона
        return CalculateAreaByHeronFormula();
    }

    private double GetMaxSide()
    {
        return Math.Max(Math.Max(SideA, SideB), SideC);
    }

    private bool IsRightAngle(double leg1, double leg2, double hypotenuse)
    {
        // Проверка на прямоугольность без использования функции Pow
        return Math.Abs(leg1 * leg1 + leg2 * leg2 - hypotenuse * hypotenuse) < 1E-8;
    }

    private double CalculateRightTriangleArea(double leg1, double leg2)
    {
        return leg1 * leg2 / 2;
    }

    private double CalculateAreaByHeronFormula()
    {
        var p = (SideA + SideB + SideC) / 2;

        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
    }
}