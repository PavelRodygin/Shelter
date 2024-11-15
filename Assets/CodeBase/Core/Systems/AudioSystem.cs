using System;
using CodeBase.Core.Systems.DataPersistenceSystem;
using UnityEngine;

namespace CodeBase.Core.Systems
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSystem : MonoBehaviour, IDataPersistence
    {
        public event Action<float> OnSoundsVolumeChanged;
        public bool isVolumeOn { get; private set; }
        
        public float SoundsVolume { get; private set; }

        public void SetSoundsVolume(bool isVolumeEnabled)
        {
            isVolumeOn = isVolumeEnabled;
            SoundsVolume = isVolumeOn ? 1f : 0f;
            OnSoundsVolumeChanged?.Invoke(SoundsVolume);
        }
        
        public void SaveData(ref GameData gameData)
        {
            gameData.isVolumeOn = isVolumeOn;
        }

        public void LoadData(GameData gameData)
        {
            isVolumeOn = gameData.isVolumeOn;
            SetSoundsVolume(isVolumeOn);
        }
    }
}