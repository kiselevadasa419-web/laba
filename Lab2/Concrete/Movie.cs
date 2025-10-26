public class Movie : IMovie
{
    public string Title { get; }
    public IAudioTrack AudioTrack { get; }
    public ISubtitles Subtitles { get; }

    public Movie(string title, IAudioTrack audioTrack, ISubtitles subtitles)
    {
        Title = title;
        AudioTrack = audioTrack;
        Subtitles = subtitles;
    }

    public void PlayMovie()
    {
        Console.WriteLine($"\n--- Воспроизведение фильма: {Title} ---");
        Console.WriteLine($"Язык звука: {AudioTrack.Language}");
        Console.WriteLine($"Язык субтитров: {Subtitles.Language}");
        Console.WriteLine();
        AudioTrack.Play();
        Subtitles.Display();
        Console.WriteLine("\n--- Фильм завершен ---\n");
    }
}