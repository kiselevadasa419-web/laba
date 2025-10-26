public class IronManMovieFactory : MovieFactory
{
    private string _language;

    public IronManMovieFactory(string language)
    {
        _language = language.ToLower();
    }

    public override IAudioTrack CreateAudioTrack()
    {
        return _language == "russian" ? new RussianAudioTrack() : new EnglishAudioTrack();
    }

    public override ISubtitles CreateSubtitles()
    {
        return _language == "russian" ? new IronManRussianSubtitles() : new IronManEnglishSubtitles();
    }

    public override IMovie CreateMovie(string title)
    {
        return new Movie(title, CreateAudioTrack(), CreateSubtitles());
    }
}
