using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class Article
{
    public string Title { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = new List<string>();
    public string Content { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
}

// Конкретный строитель для TXT формата
public class TxtArticleBuilder
{
    private Article article = new Article();
    private readonly List<string> contentLines = new List<string>();

    public void BuildTitle(string line)
    {
        if (line.StartsWith("Title:"))
        {
            article.Title = line.Substring(6).Trim();
        }
    }

    public void BuildAuthors(string line)
    {
        if (line.StartsWith("Authors:"))
        {
            var authorsText = line.Substring(8).Trim();
            article.Authors = new List<string>(authorsText.Split(','));
            
            for (int i = 0; i < article.Authors.Count; i++)
            {
                article.Authors[i] = article.Authors[i].Trim();
            }
        }
    }

    public void BuildContent(string line)
    {
        if (!string.IsNullOrWhiteSpace(line) && 
            !line.StartsWith("Title:") && 
            !line.StartsWith("Authors:") && 
            !line.StartsWith("Hash:"))
        {
            contentLines.Add(line);
        }
    }

    public void BuildHash(string line)
    {
        if (line.StartsWith("Hash:"))
        {
            article.Hash = line.Substring(5).Trim();
        }
    }

    public void FinalizeContent()
    {
        article.Content = string.Join(" ", contentLines);
    }

    public bool ValidateHash()
    {
        if (string.IsNullOrEmpty(article.Content) || string.IsNullOrEmpty(article.Hash))
            return false;

        string calculatedHash = CalculateMD5Hash(article.Content);
        return string.Equals(calculatedHash, article.Hash, StringComparison.OrdinalIgnoreCase);
    }

    private string CalculateMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    public Article GetArticle() => article;
}

public class ArticleDirector
{
    public Article ConstructFromTxt(string filePath, TxtArticleBuilder builder)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Файл не найден: {filePath}");

        string[] lines = File.ReadAllLines(filePath);
        
        foreach (string line in lines)
        {
            if (line.StartsWith("Title:"))
                builder.BuildTitle(line);
            else if (line.StartsWith("Authors:"))
                builder.BuildAuthors(line);
            else if (line.StartsWith("Hash:"))
                builder.BuildHash(line);
            else if (!string.IsNullOrWhiteSpace(line))
                builder.BuildContent(line);
        }

        builder.FinalizeContent();
        
        if (!builder.ValidateHash())
            throw new InvalidOperationException("Хеш-код статьи не совпадает! Возможно, статья была изменена.");

        return builder.GetArticle();
    }
}

// Конвертер в JSON
public class JsonArticleConverter
{
    public string ConvertToJson(Article article)
{
    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    return JsonSerializer.Serialize(article, options);
}

    public void SaveToFile(Article article, string outputPath)
    {
        string json = ConvertToJson(article);
        File.WriteAllText(outputPath, json, Encoding.UTF8);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string inputFile = "article.txt";
            
            // Создаем пример TXT файла
            CreateSampleTxtFile(inputFile);

            // Строим статью из TXT
            var builder = new TxtArticleBuilder();
            var director = new ArticleDirector();
            
            Article article = director.ConstructFromTxt(inputFile, builder);
            
            Console.WriteLine("Статья успешно загружена!");
            Console.WriteLine($"Заголовок: {article.Title}");
            Console.WriteLine($"Авторы: {string.Join(", ", article.Authors)}");
            Console.WriteLine($"Хеш-код корректен: {builder.ValidateHash()}");

            // Конвертируем в JSON
            var converter = new JsonArticleConverter();
            string json = converter.ConvertToJson(article);
            
            Console.WriteLine("\nJSON представление:");
            Console.WriteLine(json);

            // Сохраняем в файл
            string outputFile = "article.json";
            converter.SaveToFile(article, outputFile);
            Console.WriteLine($"\nJSON сохранен в файл: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CreateSampleTxtFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            // Создаем содержимое как одну строку без переносов
            string exactContent = "В данной статье рассматриваются различные паттерны проектирования, их преимущества и недостатки. Особое внимание уделяется паттерну Builder и его практическому применению в реальных проектах. Статья содержит примеры кода на языке C# и рекомендации по использованию.";
            
            // Вычисляем хеш от точной строки
            string correctHash = CalculateMD5Hash(exactContent);

            string sampleContent = $@"Title: Исследование паттернов проектирования
Authors: Иванов, Петров, Сидорова
{exactContent}
Hash: {correctHash}";

            File.WriteAllText(filePath, sampleContent, Encoding.UTF8);
            Console.WriteLine($"Создан пример файла: {filePath}");
        }
    }

    static string CalculateMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
