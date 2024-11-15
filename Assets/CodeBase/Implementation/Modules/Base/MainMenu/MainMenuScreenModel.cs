using CodeBase.Core.MVVM;
using CodeBase.Core.Systems;

namespace CodeBase.Implementation.Modules.Base.MainMenu
{
    public class MainMenuScreenModel : IScreenModel
    {
        private readonly AudioSystem _audioSystem;
        
        public MainMenuScreenModel(AudioSystem audioSystem)
        {
            _audioSystem = audioSystem;
        }

        public bool GetSoundValue() => _audioSystem.isVolumeOn;
        
        public void SetSoundVolume(bool isVolumeOn) => _audioSystem.SetSoundsVolume(isVolumeOn);

        public void Dispose()
        {
            
        }
    }
}