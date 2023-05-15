using FluentValidation;
using System.Globalization;

namespace AreaCalculator;

public class Triangle : IShape
{
    /* В предыдущем коммите в методе CalculateArea() я добавил проверку на то, 
     * является ли треугольник прямоугольным (поскольку требование включало такую проверку). 
     * Однако, я пришел к выводу, что выигрыш в производительности при вычислении площади 
     * прямоугольных треугольников по сравнению с формулой Герона является незначительным. 
     * 
     * Поэтому я предлагаю другое решение.
     * Теперь все треугольники вычисляются по формуле Герона, что повышает читаемость кода. 
     * Проверку на то, является ли треугольник прямоугольным, я реализовал с помощью свойства IsRight. 
     * Теперь у клиента появилась возможность проверить свой треугольник и определить, 
     * является ли он прямоугольным.
    */
    public bool IsRight
    {
        get
        {
            double maxSide = GetMaxSide();

            if (maxSide == SideA && IsRightAngle(SideB, SideC, SideA))
            {
                return true;
            }
            else if (maxSide == SideB && IsRightAngle(SideA, SideC, SideB))
            {
                return true;
            }
            else if (IsRightAngle(SideA, SideB, SideC))
            {
                return true;
            }

            return false;
        }
    }

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

    // Площадь любого треугольника считаем по формуле Герона
    public double CalculateArea()
    {
        var p = (SideA + SideB + SideC) / 2;

        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
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
}