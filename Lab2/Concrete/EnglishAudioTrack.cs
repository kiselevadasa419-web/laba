public class EnglishAudioTrack : IAudioTrack
{
    public string Language => "English";
    
    public void Play()
    {
        Console.WriteLine($"Playing audio track in English");
    }
}