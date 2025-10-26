public class IronManEnglishSubtitles : ISubtitles
{
    public string Language => "English";

    public void Display()
    {
        Console.WriteLine("SUBTITLES: Better to be a warrior in a garden than a gardener in a war.");
    }
}