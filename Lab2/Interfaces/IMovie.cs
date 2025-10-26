public interface IMovie
{
    string Title { get; }
    IAudioTrack AudioTrack { get; }
    ISubtitles Subtitles { get; }
    void PlayMovie();
}