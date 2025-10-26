public class MatrixMovieFactory : MovieFactory
{
    private string _language;

    public MatrixMovieFactory(string language)
    {
        _language = language.ToLower();
    }

    public override IAudioTrack CreateAudioTrack()
    {
        return _language == "russian" ? new RussianAudioTrack() : new EnglishAudioTrack();
    }

    public override ISubtitles CreateSubtitles()
    {
        return _language == "russian" ? new MatrixRussianSubtitles() : new MatrixEnglishSubtitles();
    }

    public override IMovie CreateMovie(string title)
    {
        return new Movie(title, CreateAudioTrack(), CreateSubtitles());
    }
}