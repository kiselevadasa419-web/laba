public class IronManRussianSubtitles : ISubtitles
{
    public string Language => "Russian";

    public void Display()
    {
        Console.WriteLine("СУБТИТРЫ: Лучше быть воином в саду, чем садовником на войне.");
    }
}