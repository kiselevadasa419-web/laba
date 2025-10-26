public abstract class MovieFactory
{
    public abstract IAudioTrack CreateAudioTrack();
    public abstract ISubtitles CreateSubtitles();
    public abstract IMovie CreateMovie(string title);
}