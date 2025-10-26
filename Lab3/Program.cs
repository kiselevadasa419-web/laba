using System;
using System.Collections.Generic;

// Базовый класс для всех фигур
public abstract class Figure
{
    public string Name { get; set; }
    public int Cells { get; set; }
    
    public abstract Figure Clone();
    
    public void Display()
    {
        Console.WriteLine($"Фигура: {Name}, Клеток: {Cells}");
    }
}

// Обычные фигуры
public class Square : Figure
{
    public Square()
    {
        Name = "Квадрат";
        Cells = 4;
    }
    
    public override Figure Clone()
    {
        return new Square { Name = this.Name, Cells = this.Cells };
    }
}

public class Line : Figure
{
    public Line()
    {
        Name = "Линия";
        Cells = 4;
    }
    
    public override Figure Clone()
    {
        return new Line { Name = this.Name, Cells = this.Cells };
    }
}

public class LShape : Figure
{
    public LShape()
    {
        Name = "L-образная";
        Cells = 4;
    }
    
    public override Figure Clone()
    {
        return new LShape { Name = this.Name, Cells = this.Cells };
    }
}

// Супер-фигуры
public class SuperSquare : Figure
{
    public SuperSquare()
    {
        Name = "Супер-квадрат";
        Cells = 9;
    }
    
    public override Figure Clone()
    {
        return new SuperSquare { Name = this.Name, Cells = this.Cells };
    }
}

public class Cross : Figure
{
    public Cross()
    {
        Name = "Крест";
        Cells = 5;
    }
    
    public override Figure Clone()
    {
        return new Cross { Name = this.Name, Cells = this.Cells };
    }
}

// Фабрика для создания фигур
public abstract class FigureFactory
{
    public abstract Figure CreateFigure();
}

// Фабрики для каждой фигуры
public class SquareFactory : FigureFactory
{
    public override Figure CreateFigure()
    {
        return new Square();
    }
}

public class LineFactory : FigureFactory
{
    public override Figure CreateFigure()
    {
        return new Line();
    }
}

public class LShapeFactory : FigureFactory
{
    public override Figure CreateFigure()
    {
        return new LShape();
    }
}

public class SuperSquareFactory : FigureFactory
{
    public override Figure CreateFigure()
    {
        return new SuperSquare();
    }
}

public class CrossFactory : FigureFactory
{
    public override Figure CreateFigure()
    {
        return new Cross();
    }
}

// Главный класс для работы с фигурами
public class FigureManager
{
    private List<FigureFactory> factories;
    private Random random;
    
    public FigureManager()
    {
        random = new Random();
        factories = new List<FigureFactory>
        {
            new SquareFactory(),
            new LineFactory(),
            new LShapeFactory(),
            new SuperSquareFactory(),
            new CrossFactory()
        };
    }
    
    // Получить 3 случайные фигуры
    public List<Figure> GetThreeRandomFigures()
    {
        var figures = new List<Figure>();
        for (int i = 0; i < 3; i++)
        {
            int index = random.Next(factories.Count);
            figures.Add(factories[index].CreateFigure());
        }
        return figures;
    }
    
    // Создание копии фигуры
    public Figure CopyFigure(Figure original)
    {
        return original.Clone();
    }
}

// Тестирование
class Program
{
    static void Main(string[] args)
    {
        FigureManager manager = new FigureManager();
        
        Console.WriteLine("3 случайные фигуры:");
        var threeFigures = manager.GetThreeRandomFigures();
        foreach (var figure in threeFigures)
        {
            figure.Display();
        }
        
        Console.WriteLine("\nКопирование фигур:");
        Figure original = manager.GetThreeRandomFigures()[0];
        Console.Write("Оригинал: ");
        original.Display();
        
        Figure copy = manager.CopyFigure(original);
        Console.Write("Копия: ");
        copy.Display();
        
        Console.WriteLine($"Это один и тот же объект? {original == copy}");
    }
}