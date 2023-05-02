using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Systems.AudioSystem
{
    public class AudioSystem : MonoBehaviour    //, IDataPersistence
    {
        [SerializeField] private AudioClip metronome;
        [SerializeField] private List<AudioClip> songs;
        public List<string> songsNames;
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource metronomeAudioSource;
        public float soundsVolume = .5f;
        public float musicVolume = .5f;
        public float metronomeVolume = .5f;
        private int _songIndex;

        private void Awake()
        {
            musicAudioSource.clip = songs[_songIndex];
            musicAudioSource.Play();
            metronomeAudioSource.Stop();
            songsNames = songs.Select(x => x.name).ToList();
            musicAudioSource.volume = musicVolume;
        }

        public void PlaySong(int songIndex)
        {
            _songIndex = songIndex;
            musicAudioSource.clip = songs[songIndex];
            musicAudioSource.Play();
        }
        
        public int ReturnCurrentSongIndex()
        {
            return songs.FindIndex(i => i == musicAudioSource.clip);
        }
        
        public async UniTask PlayMetronome(int times, int time, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => {metronomeAudioSource.Stop();});
            for (int i = 0; i < times; i++)
            {
                metronomeAudioSource.clip = metronome;
                await UniTask.Delay(1000, cancellationToken: cancellationToken);
                metronomeAudioSource.Play();
                await UniTask.Delay(time/times, cancellationToken: cancellationToken);
                metronomeAudioSource.Stop();
            }
        }
        
        public void ChangeMusicVolume(float volume)
        {
            musicAudioSource.volume = volume;
            musicVolume = volume;
        }

        public void ChangeMetronomeVolume(float volume)
        {
            metronomeAudioSource.volume = volume;
            metronomeVolume = volume;
        }

        public void StopMetronome()
        {
            metronomeAudioSource.Stop();
            metronomeAudioSource.time = 0;
        }

        // public void SaveData(ref GameData data)
        // {
        //     data.MusicVolume = musicVolume;
        //     data.SoundsVolume = soundsVolume;
        //     data.MetronomeVolume = metronomeVolume;
        //     data.SongIndex = _songIndex;
        // }
        //
        // public void LoadData(GameData gameData)
        // {
        //     _songIndex = gameData.SongIndex;
        //     musicVolume = gameData.MusicVolume;
        //     soundsVolume = gameData.SoundsVolume;
        //     metronomeVolume = gameData.MetronomeVolume;
        // }
    }
}