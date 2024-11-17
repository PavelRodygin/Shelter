using Core.Gameplay.AbstractClasses;
using Core.Gameplay.PlayerScripts;
using Core.Systems.DataPersistenceSystem;
using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;
using Zenject;

namespace Modules.GameScreen.Scripts
{
    public class LevelManager : MonoBehaviour, IDataPersistence
    {
        [Inject] private Camera _camera;
        [Inject] private IInputService _inputService;
        //[Inject] private AudioSystem _audioSystem;
        [SerializeField] private GameObject level;
        [SerializeField] private OpenClosable blastDoor;
        [SerializeField] private OpenClosable blastHatch;
        [SerializeField] private Interactable fvm;  // Filter ventilation machine
        //[SerializeField] private AudioSource siren;
        //[SerializeField] private float breakingTime = 20000f;
        //[SerializeField] private float breakTimeMultiplier = 1.2f;
        //private List<Breakable> _breakables = new();  
        [SerializeField] private float messageFadeTime = 0.5f;
        [SerializeField] private Player player;
        private GameScreenView _gameScreenView;
        private Breakable _currentBroken;
        private bool _surviveStarted;
        //private bool _allIsFine = true;

        public float CameraSensitivity { get; set; }

        public void Initialize(GameScreenView gameScreenView)
        {
            _gameScreenView = gameScreenView;
            player.Initialize(CameraSensitivity);
        }
        
        private void GenerateLevel()
        {
            level.SetActive(true);
        }

        //private async void BreakSomething()
        //{
            // while (player.FirstPersonController.IsAlive)
            // {
            //     int index = Random.Range(0, breakable.Count); 
            //     //breakables[index]Break();
            //     await UniTask.Delay((int)(breakingTime * breakTimeMultiplier));
            // }
        //}
        
        public async UniTask Show()
        {
            await player.Show();
        }

        public async UniTask Hide()
        {
            await player.Hide();
            gameObject.SetActive(false);
        }
        
        public void LoadData(GameData gameData)
        {
            CameraSensitivity = gameData.CameraSensitivity;
        }

        public void SaveData(ref GameData gameData)
        {
            gameData.CameraSensitivity = CameraSensitivity;
        }
    }
}
