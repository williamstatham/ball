using Game.Scripts.Systems.Field;
using Game.Scripts.Systems.UI;
using Game.Scripts.Systems.ViewModel;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Systems.Lives
{
    public sealed class LivesManager
    {
        private GameSettings _gameSettings;
        
        private GameUIViewModel _gameUIViewModel;

        private int _currentLives;

        [Inject]
        public void Construct(GameSettings gameSettings, GameUIViewModel gameUIViewModel)
        {
            _gameSettings = gameSettings;
            _gameUIViewModel = gameUIViewModel;
        }

        public void Reset()
        {
            _currentLives = _gameSettings.MaxLives;
            UpdateViewModel();
        }

        public bool WithdrawLives(int count)
        {
            _currentLives = Mathf.Max(0, _currentLives - count);
            UpdateViewModel();
            return _currentLives != 0;
        }

        private void UpdateViewModel()
        {
            _gameUIViewModel.LivesData.Value = new LivesData(_gameSettings.MaxLives, _currentLives);
        }
    }
}