using Game.Scripts.Core.UniRX;
using Game.Scripts.Systems.Player;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Camera
{
    public sealed class CameraMovementManager : ControllerBase, IStartable, IInitializable
    {
        private UnityEngine.Camera _camera;
        private PlayerView _playerView;

        private float _yOffset;

        [Inject]
        public void Construct(UnityEngine.Camera camera, PlayerView playerView)
        {
            _camera = camera;
            _playerView = playerView;
        }
        
        void IInitializable.Initialize()
        {
            _yOffset = _camera.transform.position.y;
        }
        
        void IStartable.Start()
        {
            _playerView.transform
                .ObserveEveryValueChanged(i => i.position)
                .Subscribe(OnPlayerPositionChange)
                .AddTo(this);
        }

        private void OnPlayerPositionChange(Vector3 newPosition)
        {
            Vector3 previousPosition = _camera.transform.position;
            previousPosition.Set(previousPosition.x, newPosition.y + _yOffset, previousPosition.z);
            _camera.transform.position = previousPosition;
        }
    }
}