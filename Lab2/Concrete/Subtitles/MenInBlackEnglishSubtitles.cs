public class MenInBlackEnglishSubtitles : ISubtitles
{
    public string Language => "English";

    public void Display()
    {
        Console.WriteLine("SUBTITLES: The galaxy is on Orion's belt!");
    }
}