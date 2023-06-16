using Core.Systems.DataPersistenceSystem;
using UnityEngine;

namespace Core.Systems
{
    [RequireComponent(typeof(AudioListener))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystem : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private AudioClip mainMenuMelody;
        private AudioSource _musicAudioSource;
        public float MusicVolume { get; private set; }
        public float SoundsVolume { get; private set; }

        private void Awake() => _musicAudioSource = GetComponent<AudioSource>();

        public void PlayMainMenuMelody()
        {
            _musicAudioSource.clip = mainMenuMelody;
            _musicAudioSource.Play();
        }

        public void StopMainMenuMelody()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = null;
        }

        public void SetMusicVolume(float volume)
        {
            if (volume > 0)
            {
                MusicVolume = volume;
                _musicAudioSource.volume = MusicVolume;
            }
            else
            {
                MusicVolume = 0;
                _musicAudioSource.volume = MusicVolume;
            }
        }
        
        public void SetSoundsVolume(float volume) => SoundsVolume = volume > 0 ? volume : 0;

        public void SaveData(ref GameData gameData)
        {
            gameData.MusicVolume = MusicVolume;
            gameData.SoundsVolume = SoundsVolume;
        }

        public void LoadData(GameData gameData)
        {
            MusicVolume = gameData.MusicVolume;
            SoundsVolume = gameData.SoundsVolume;
            _musicAudioSource.volume = MusicVolume;
            _musicAudioSource.Play();
        }
    }
}