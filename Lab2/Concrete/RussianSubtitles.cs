public class RussianSubtitles : ISubtitles
{
    public string Language => "Russian";
    
    public void Display()
    {
        Console.WriteLine($"Отображение русских субтитров");
    }
}