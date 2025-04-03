using ServiceLocatorThingie;



Services.Open();
Services.TryRegister<AudioService>(new(0));
Services.Close();

var hasAudio = Services.TryLocate<AudioService>(out var audioService);
if(hasAudio)
    audioService!.PlaySong();



Services.Open();
Services.ForceAdd<AudioService>(new(1));
Services.Close();

hasAudio = Services.TryLocate<AudioService>(out audioService);
if(hasAudio)
    audioService!.PlaySong();