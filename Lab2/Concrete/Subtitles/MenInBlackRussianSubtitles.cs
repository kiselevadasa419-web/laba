public class MenInBlackRussianSubtitles : ISubtitles
{
    public string Language => "Russian";

    public void Display()
    {
        Console.WriteLine("СУБТИТРЫ: Галактика находится на ошейнике этого кота!");
    }
}