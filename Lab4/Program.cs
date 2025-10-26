using System;
using System.Collections.Generic;

// Лифт (Singleton)
public class Elevator
{
    // Единственный экземпляр лифта
    private static Elevator? _instance;
    private static readonly object _lock = new object();
    
    public int CurrentFloor { get; private set; }
    public bool IsMoving { get; private set; }
    
    // Приватный конструктор чтобы нельзя было создать новый лифт
    private Elevator()
    {
        CurrentFloor = 1;
        IsMoving = false;
        Console.WriteLine("Лифт создан на 1 этаже");
    }
    
    // Метод чтобы получить лифт
    public static Elevator GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Elevator();
            }
            return _instance;
        }
    }
    
    public void MoveToFloor(int floor)
    {
        if (floor < 1 || floor > 3)
        {
            Console.WriteLine("Ошибка! В здании только этажи 1, 2, 3");
            return;
        }
        
        if (IsMoving)
        {
            Console.WriteLine("Лифт уже движется!");
            return;
        }
        
        IsMoving = true;
        Console.WriteLine($"Лифт едет с {CurrentFloor} этажа на {floor} этаж...");
        
        CurrentFloor = floor;
        IsMoving = false;
        Console.WriteLine($"Лифт прибыл на {CurrentFloor} этаж");
    }
    
    public void CallToFloor(int floor)
    {
        Console.WriteLine($"Вызов лифта на {floor} этаж");
        MoveToFloor(floor);
    }
}

// Помещение на этаже
public class Room
{
    public string Name { get; set; }
    public int Number { get; set; }
    
    public Room(string name, int number)
    {
        Name = name;
        Number = number;
    }
    
    public void CallElevator()
    {
        Console.WriteLine($"Помещение {Name} вызывает лифт");
        Elevator elevator = Elevator.GetInstance();
        elevator.CallToFloor(GetFloorFromRoomNumber(Number));
    }
    
    private int GetFloorFromRoomNumber(int roomNumber)
    {
        return roomNumber / 100;
    }
}

// Этаж
public class Floor
{
    public int Number { get; set; }
    public List<Room> Rooms { get; set; }
    
    public Floor(int number)
    {
        if (number < 1 || number > 3)
        {
            Console.WriteLine("Ошибка! Можно создать только этажи 1, 2, 3");
            Number = 1; // Значение по умолчанию
        }
        else
        {
            Number = number;
        }
        
        Rooms = new List<Room>();
    }
    
    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }
}

// Здание (тоже Singleton)
public class Building
{
    private static Building? _instance;
    private static readonly object _lock = new object();
    
    public string Name { get; set; }
    public List<Floor> Floors { get; set; }
    
    private Building()
    {
        Name = "Офисное здание";
        Floors = new List<Floor>();
        Console.WriteLine("Создано здание");
        
        CreateThreeFloors();
    }
    
    private void CreateThreeFloors()
    {
        Floor floor1 = new Floor(1);
        Floor floor2 = new Floor(2);
        Floor floor3 = new Floor(3);
        
        floor1.AddRoom(new Room("Ресепшен", 101));
        floor1.AddRoom(new Room("Офис", 102));
        
        floor2.AddRoom(new Room("Офис 666", 266));
        
        floor3.AddRoom(new Room("Кухня", 301));
        
        Floors.Add(floor1);
        Floors.Add(floor2);
        Floors.Add(floor3);
        
        Console.WriteLine("Добавлено 3 этажа: 1, 2, 3");
    }
    
    public static Building GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Building();
            }
            return _instance;
        }
    }
    
    public void ShowBuildingInfo()
    {
        Console.WriteLine($"\nИнформация о здании '{Name}':");
        Console.WriteLine($"Этажей: {Floors.Count}");
        foreach (var floor in Floors)
        {
            Console.WriteLine($"Этаж {floor.Number}: {floor.Rooms.Count} помещений");
        }
    }
}

// Тестирование
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== СИСТЕМА ЛИФТА В ЗДАНИИ ===\n");
        
        Building building = Building.GetInstance();
        building.ShowBuildingInfo();
        
        Console.WriteLine("\n=== РАБОТА ЛИФТА ===\n");
        
        Elevator elevator1 = Elevator.GetInstance();
        Elevator elevator2 = Elevator.GetInstance();
        
        Console.WriteLine($"Это один и тот же лифт? {elevator1 == elevator2}");
        
        Console.WriteLine("\n--- Вызовы из помещений ---");
        
        building.Floors[1].Rooms[0].CallElevator();
        building.Floors[2].Rooms[0].CallElevator();
        
        Console.WriteLine("\n--- Попытка вызова на 4 этаж ---");
        elevator1.CallToFloor(4);
        
        Console.WriteLine("\n--- Проверка Singleton ---");
        
        Building anotherBuilding = Building.GetInstance();
        Elevator anotherElevator = Elevator.GetInstance();
        
        Console.WriteLine($"То же здание? {building == anotherBuilding}");
        Console.WriteLine($"Тот же лифт? {elevator1 == anotherElevator}");
        
        Console.WriteLine("\n=== ПРОВЕРКА ЗАВЕРШЕНА ===");
    }
}