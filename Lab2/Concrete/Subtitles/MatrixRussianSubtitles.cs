public class MatrixRussianSubtitles : ISubtitles
{
    public string Language => "Russian";

    public void Display()
    {
        Console.WriteLine("СУБТИТРЫ: Не пытайся согнуть ложку. Это невозможно.");
    }
}