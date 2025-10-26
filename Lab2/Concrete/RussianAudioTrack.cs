public class RussianAudioTrack : IAudioTrack
{
    public string Language => "Russian";
    
    public void Play()
    {
        Console.WriteLine($"Воспроизведение звуковой дорожки на русском языке");
    }
}