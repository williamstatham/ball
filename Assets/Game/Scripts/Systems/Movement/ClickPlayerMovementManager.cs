using Game.Scripts.Core.UniRX;
using Game.Scripts.Systems.Player;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Movement
{
    public class ClickPlayerMovementManager : ControllerBase, IPlayerMovementManager, IStartable
    {
        private InputPanel _inputPanel;
        private UnityEngine.Camera _inputCamera;
        private MovementSettings _movementSettings;
        private PlayerView _playerView;

        private int _direction = 1;

        [Inject]
        public void Construct(UnityEngine.Camera inputCamera, InputPanel inputPanel, PlayerView playerView, MovementSettings movementSettings)
        {
            _inputPanel = inputPanel;
            _inputCamera = inputCamera;
            _playerView = playerView;
            _movementSettings = movementSettings;
        }
        
        void IStartable.Start()
        {
            _inputPanel.OnClickAsObservable
                .Subscribe(OnPanelClick)
                .AddTo(this);
        }

        private void OnPanelClick(PointerEventData eventData)
        {
            _direction *= -1;
        }
        
        public void MovePlayer(float gameSpeed, float deltaTime)
        {
            Vector3 updatedPosition = _playerView.transform.localPosition + new Vector3(_direction * _movementSettings.Speed.x, 0);
            
            float cameraMinX = _inputCamera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            float cameraMaxX = _inputCamera.ViewportToWorldPoint(new Vector3(1, 0)).x;
            
            float playerWidth = _playerView.Bounds.max.x - _playerView.Bounds.min.x;
            
            float newPlayerPositionX = Mathf.Clamp(updatedPosition.x, cameraMinX + playerWidth / 2,
                cameraMaxX - playerWidth / 2);
            
            Vector3 newPlayerPosition = _playerView.transform.localPosition;
            newPlayerPosition.Set(newPlayerPositionX, newPlayerPosition.y + (_movementSettings.Speed.y * gameSpeed * deltaTime), newPlayerPosition.z);
            _playerView.transform.localPosition = newPlayerPosition;
        }
        
        public void Reset()
        {
            _playerView.transform.position = Vector2.zero;
        }
    }
}