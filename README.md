# AreaCalculator library

## Основные возможности

AreaCalculator - библиотека, предназначенная для поставки внешним клиентам, которая умеет вычислять площадь различных геометрических фигур, включая круг и треугольник. При создании ипользовалась FluentValidation - библиотека для создания правил валидации входных данных

#### Ключевые функции:

-  **Вычисление площади круга по заданному радиусу**
-  **Вычисление площади треугольника по заданным трем сторонам**
-  **Проверка созданного треугольника на то, является ли он прямоугольным**

#### Общий пример использования библиотеки:
~~~
using AreaCalculator;

public void Foo()
    {
        IShape figure;

        if (condition)
        {
            figure = new Circle(10);
        }
        else
        {
            figure = new Triangle(10, 10, 10);
        }
        
        var area = figure.CalculateArea();
    }
    
public void Boo()
    {
        var triangle = new Triangle(3, 4, 5);
        
        // true
        var isRight = triangle.IsRight;
    }
~~~
