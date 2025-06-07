using System;

namespace C__NET5;

public class Shape
{
    public const double PI = Math.PI;
    protected double x, y;
        
    public Shape(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public virtual double Area() => x * y; // не вызовется, все Area наследников вызовутся
}

public class Square : Shape // TODO не семантично, выбивается из иерархии, для примера. доработать
{
    public Square(double r) : base(r, r) { }

    //public override double Area() => Area(); // ~ эксепшн. рекурсивный вызов самого себя (проверил в дебаге). design time ok
    public override double Area() => base.Area();
}

public class Circle : Shape
{
    public Circle(double r) : base(r, 0) { }

    public override double Area() => PI * x * x;
}

class Sphere : Shape
{
    public Sphere(double r) : base(r, 0) { }

    public override double Area() => 4 * PI * x * x;
}

class Cylinder : Shape
{
    public Cylinder(double r, double h) : base(r, h) { }

    public override double Area() => 2 * PI * x * x + 2 * PI * x * y;
}