using System;
using System.Collections.Generic;

public class MovieRentalService
{
    private Dictionary<string, Func<string, MovieFactory>> _movieFactories;

    public MovieRentalService()
    {
        _movieFactories = new Dictionary<string, Func<string, MovieFactory>>
        {
            { "1", (language) => new MenInBlackMovieFactory(language) },
            { "2", (language) => new MatrixMovieFactory(language) },
            { "3", (language) => new IronManMovieFactory(language) }
        };
    }

    public IMovie RentMovie(string movieChoice, string language)
    {
        if (string.IsNullOrEmpty(movieChoice))
        {
            throw new ArgumentException("Выбор фильма не может быть пустым");
        }

        if (string.IsNullOrEmpty(language))
        {
            throw new ArgumentException("Язык не может быть пустым");
        }

        if (_movieFactories.TryGetValue(movieChoice, out var factoryCreator))
        {
            var factory = factoryCreator(language);
            var movieTitle = GetMovieTitle(movieChoice, language);
            return factory.CreateMovie(movieTitle);
        }
        
        throw new ArgumentException($"Неверный выбор фильма: {movieChoice}");
    }

    private string GetMovieTitle(string movieChoice, string language)
    {
        return movieChoice switch
        {
            "1" => language == "russian" ? "Люди в черном" : "Men in Black",
            "2" => language == "russian" ? "Матрица" : "The Matrix",
            "3" => language == "russian" ? "Железный человек" : "Iron Man",
            _ => "Неизвестный фильм"
        };
    }

    public void DisplayAvailableLanguages()
    {
        Console.WriteLine("Доступные языки: russian, english");
    }
}