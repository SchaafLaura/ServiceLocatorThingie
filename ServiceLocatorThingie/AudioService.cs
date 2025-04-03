namespace ServiceLocatorThingie;

public class AudioService(int soundId)
{
    public void PlaySong()
    {
        Console.WriteLine("playing song " + soundId);
    }
}