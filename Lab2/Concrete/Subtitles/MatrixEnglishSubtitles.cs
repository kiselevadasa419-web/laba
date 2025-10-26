public class MatrixEnglishSubtitles : ISubtitles
{
    public string Language => "English";

    public void Display()
    {
        Console.WriteLine("SUBTITLES: Do not try to bend the spoon. That's impossible.");
    }
}