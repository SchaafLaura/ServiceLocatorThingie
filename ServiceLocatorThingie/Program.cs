using ServiceLocatorThingie;

//////////////////////////// Adding new Services Example //////////////////////////// 
Services.Open();
Services.TryRegister<AudioService>(new(soundId: 0));
Services.TryRegister<AudioService>(new(soundId: 1));    // won't work, as there is already a AudioService
Services.Close();

var hasAudio = Services.TryLocate<AudioService>(out var audioService);
if(hasAudio)
    audioService!.PlaySong(); // plays song 0

//////////////////////////// Overriding Services Example ////////////////////////////
Services.Open();
Services.ForceAdd<AudioService>(new(soundId: 1));       // will override the existing AudioServicee
Services.Close();

hasAudio = Services.TryLocate<AudioService>(out audioService);
if(hasAudio)
    audioService!.PlaySong();  // plays song 1

//////////////////////////// Exceptions Example 1 ////////////////////////////
Services.Open();
try
{
    hasAudio = Services.TryLocate<AudioService>(out audioService); // throws ServiceLocatorNotClosedException
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}
Services.Close();

//////////////////////////// Exceptions Example 2 ////////////////////////////
try
{
    Services.TryRegister<AudioService>(new(soundId: 0));    // throws ServiceLocatorNotOpenException
    Services.ForceAdd<AudioService>(new(soundId: 1));       // throws ServiceLocatorNotOpenException
} 
catch (Exception e) {
    Console.WriteLine(e.Message);
}