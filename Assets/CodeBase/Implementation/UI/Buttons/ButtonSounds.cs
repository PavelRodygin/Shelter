using CodeBase.Core.Systems;
using CodeBase.Core.UI.Buttons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Implementation.UI.Buttons
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Selectable))]
    //TODO OnEnable OdDisable needs for Pool. Otherwise works bad
    public class ButtonSounds : MonoBehaviour, ISoundsVolumeObserver, IPointerEnterHandler
    {
        [Inject] private AudioSystem _audioSystem;
        [SerializeField] private AudioClip pressedSound;
        [SerializeField] private AudioClip rolloverSound;

        private AudioSource _audioSource;
        private Selectable _selectable;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _selectable = GetComponent<Selectable>();

            _audioSource.volume = _audioSystem.SoundsVolume;
        }

        private void OnEnable()
        {
            _audioSystem.OnSoundsVolumeChanged += SetSoundVolume;
            _audioSource.volume = _audioSystem.SoundsVolume;
            
            switch (_selectable)
            {
                case Button button:
                    button.onClick.AddListener(PlayPressedSound);
                    break;
                case Toggle toggle:
                    //May be different sound variants for On and Of
                    toggle.onValueChanged.AddListener(isOn => PlayPressedSound());
                    break;
            }
        }

        private void OnDisable()
        {
            _audioSystem.OnSoundsVolumeChanged -= SetSoundVolume;

            switch (_selectable)
            {
                case Button button:
                    button.onClick.RemoveListener(PlayPressedSound);
                    break;
                case Toggle toggle:
                    toggle.onValueChanged.RemoveListener(isOn => PlayPressedSound());
                    break;
            }
        }

        public void SetSoundVolume(float volume) => _audioSource.volume = volume;
        
        public void OnPointerEnter(PointerEventData eventData) => PlayRolloverSound();
        
        private void PlayPressedSound()
        {
            if (!_audioSource.isActiveAndEnabled || !(_audioSystem.SoundsVolume > 0) || pressedSound == null) return;
            _audioSource.clip = pressedSound;
            _audioSource.Play();
        }

        private void PlayRolloverSound()
        {
            if (!_audioSource.isActiveAndEnabled || !(_audioSystem.SoundsVolume > 0) || rolloverSound == null) return;
            _audioSource.clip = rolloverSound;
            _audioSource.Play();
        }
    }
}