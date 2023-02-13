using Game.Scripts.Core.UniRX;
using Game.Scripts.Systems.Player;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Movement
{
    public class CursorPlayerMovementManager : ControllerBase, IPlayerMovementManager, IStartable
    {
        private InputPanel _inputPanel;
        private UnityEngine.Camera _inputCamera;
        private MovementSettings _movementSettings;
        private PlayerView _playerView;

        private Vector2 _startCursorPosition;
        private Vector3 _currentDragOffset;

        [Inject]
        public void Construct(UnityEngine.Camera inputCamera, InputPanel inputPanel, MovementSettings movementSettings, PlayerView playerView)
        {
            _inputPanel = inputPanel;
            _inputCamera = inputCamera;
            _movementSettings = movementSettings;
            _playerView = playerView;
        }
        
        void IStartable.Start()
        {
            _inputPanel.OnBeginDragAsObservable
                .Subscribe(OnPanelBeginDrag)
                .AddTo(this);

            _inputPanel.OnDragAsObservable
                .Subscribe(OnPanelDrag)
                .AddTo(this);
        }
        
        private void OnPanelBeginDrag(PointerEventData eventData)
        {
            _startCursorPosition = _inputCamera.ScreenToWorldPoint(eventData.position);
        }

        private void OnPanelDrag(PointerEventData eventData)
        {
            Vector2 dragPosition = _inputCamera.ScreenToWorldPoint(eventData.position);
            _currentDragOffset = dragPosition - _startCursorPosition;
            _startCursorPosition = dragPosition;
        }
        
        public void MovePlayer(float gameSpeed, float deltaTime)
        {
            Vector3 updatedPosition = _playerView.transform.position + (_currentDragOffset * _movementSettings.Speed.x);
            
            float cameraMinX = _inputCamera.ViewportToWorldPoint(new Vector3(0, 0)).x;
            float cameraMaxX = _inputCamera.ViewportToWorldPoint(new Vector3(1, 0)).x;
            
            float playerWidth = _playerView.Bounds.max.x - _playerView.Bounds.min.x;
            
            float newPlayerPositionX = Mathf.Clamp(updatedPosition.x, cameraMinX + playerWidth / 2,
                cameraMaxX - playerWidth / 2);
            
            Vector3 newPlayerPosition = _playerView.transform.localPosition;
            newPlayerPosition.Set(newPlayerPositionX, newPlayerPosition.y + (_movementSettings.Speed.y * gameSpeed * deltaTime), newPlayerPosition.z);
            _playerView.transform.localPosition = newPlayerPosition;
            
            _currentDragOffset = default;
        }

        public void Reset()
        {
            _playerView.transform.position = Vector2.zero;
        }
    }
}