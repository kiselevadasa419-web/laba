class Program
{
    static void Main(string[] args)
    {
        var rentalService = new MovieRentalService();
        
        Console.WriteLine("=== СИСТЕМА КИНОПРОКАТА ===");
        
        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Арендовать фильм");
            Console.WriteLine("2 - Показать доступные языки");
            Console.WriteLine("3 - Выход");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    RentMovie(rentalService);
                    break;
                case "2":
                    rentalService.DisplayAvailableLanguages();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
    
    static void RentMovie(MovieRentalService rentalService)
    {
        try
        {
            Console.WriteLine("\nДоступные фильмы:");
            Console.WriteLine("1 - Люди в черном / Men in Black");
            Console.WriteLine("2 - Матрица / The Matrix");
            Console.WriteLine("3 - Железный человек / Iron Man");
            
            Console.Write("Введите номер фильма (1-3): ");
            var movieChoice = Console.ReadLine()?.Trim();
            
            Console.Write("Введите язык (russian/english): ");
            var language = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrEmpty(movieChoice) || string.IsNullOrEmpty(language))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым");
                return;
            }
            
            var movie = rentalService.RentMovie(movieChoice, language);
            movie.PlayMovie();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}