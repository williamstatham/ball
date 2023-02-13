using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Systems.Field;
using Game.Scripts.Systems.Field.Entities;
using Game.Scripts.Systems.Lives;
using Game.Scripts.Systems.Movement;
using Game.Scripts.Systems.Player;
using Game.Scripts.Systems.Popups;
using Game.Scripts.Systems.Score;
using Game.Scripts.Systems.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems
{
    public sealed class GamePresenter : ITickable, IStartable, IDisposable
    {
        // management layer
        private IPlayerMovementManager _playerMovementManager;
        private LivesManager _livesManager;
        private ScoreManager _scoreManager;
        private FieldManager _fieldManager;
        private GamePopupManager _gamePopupManager;
        
        // view layer
        private PlayerView _playerView;

        // settings layer
        private GameSettings _gameSettings;
        
        private GameUIViewModel _uiViewModel;
        private GamePopupViewModel _popupViewModel;

        private bool _gameRunning;
        private float _currentGameSpeed;

        [Inject]
        public void Construct(
            IPlayerMovementManager playerMovementManager,
            LivesManager livesManager,
            ScoreManager scoreManager,
            FieldManager fieldManager,
            GamePopupManager gamePopupManager,
            PlayerView playerView,
            GameSettings gameSettings,
            GameUIViewModel gameUIViewModel,
            GamePopupViewModel popupViewModel)
        {
            _playerMovementManager = playerMovementManager;
            _livesManager = livesManager;
            _scoreManager = scoreManager;
            _fieldManager = fieldManager;
            _gamePopupManager = gamePopupManager;
            
            _playerView = playerView;

            _gameSettings = gameSettings;

            _uiViewModel = gameUIViewModel;
            _popupViewModel = popupViewModel;
        }

        void ITickable.Tick()
        {
            if (!_gameRunning)
            {
                return;
            }
            _playerMovementManager.MovePlayer(_currentGameSpeed, Time.deltaTime);
            _fieldManager.UpdateField(_currentGameSpeed, Time.deltaTime);
        }

        void IStartable.Start()
        {
            Reset();
            
            _playerView.OnPlayerEnterTrigger += OnPlayerEnterTrigger;
            _popupViewModel.StartGameButtonClicked += OnStartGameButtonClicked;
            _gamePopupManager.ShowStartGamePopup();
        }

        void IDisposable.Dispose()
        {
            _playerView.OnPlayerEnterTrigger -= OnPlayerEnterTrigger;
            _popupViewModel.StartGameButtonClicked -= OnStartGameButtonClicked;
        }
        
        private void OnStartGameButtonClicked()
        {
            ResetAndStart();
        }

        private void OnPlayerEnterTrigger(Collider collider)
        {
            FieldEntityBase fieldEntity = _fieldManager.GetFieldEntity(i => i.Collider == collider);
            if (fieldEntity == null)
            {
                return;
            }

            if (fieldEntity is ICanDieFieldEntity canDieFieldEntity)
            {
                canDieFieldEntity.ShowDeathVFX();
            }

            switch (fieldEntity)
            {
                case ScoreEntity scoreFieldEntity:
                    UpdateScoreAndValidateGameSpeed(scoreFieldEntity.ScoreIncrement);
                    _fieldManager.ReleaseEntity(scoreFieldEntity);
                    break;
                case LifeWithdrawEntity lifeWithdrawFieldEntity:
                    if (!_livesManager.WithdrawLives(lifeWithdrawFieldEntity.WithdrawLives))
                    {
                        Die();
                    }
                    _fieldManager.ReleaseEntity(lifeWithdrawFieldEntity);
                    break;
                case WallEntity:
                    Die();
                    break;
            }
        }

        private void ResetAndStart()
        {
            Reset();
            _gameRunning = true;
        }

        private void UpdateScoreAndValidateGameSpeed(int scoreIncrement)
        {
            _scoreManager.IncrementScore(scoreIncrement);

            if (_gameSettings.TryGetNewGameSpeed(_scoreManager.CurrentScore, out float targetGameSpeed))
            {
                IncreaseGameSpeed(targetGameSpeed).Forget();
            }
        }

        private void Die()
        {
            _playerView.showDieVFX();
            _gameRunning = false;
            _gamePopupManager.ShowRestartGamePopup();
        }

        private async UniTaskVoid IncreaseGameSpeed(float targetGameSpeed)
        {
            float startGameSpeed = _currentGameSpeed;
            for (float time = 0f; time <= _gameSettings.GameSpeedChangeTimeSeconds; time += Time.deltaTime)
            {
                _currentGameSpeed = Mathf.Lerp(startGameSpeed, targetGameSpeed,
                    _gameSettings.GameSpeedChangeFunction.Evaluate(time / _gameSettings.GameSpeedChangeTimeSeconds));
                await UniTask.Yield();
            }

            _currentGameSpeed = targetGameSpeed;
        }

        private void Reset()
        {
            _currentGameSpeed = _gameSettings.DefaultGameSpeed;
            _playerMovementManager.Reset();
            _playerView.Reset();
            _livesManager.Reset();
            _scoreManager.Reset();
            _fieldManager.Reset();
        }
    }
}