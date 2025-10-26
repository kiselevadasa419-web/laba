public class EnglishSubtitles : ISubtitles
{
    public string Language => "English";
    
    public void Display()
    {
        Console.WriteLine($"Displaying English subtitles");
    }
}