using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Systems.Field.Entities;
using UnityEngine;

namespace Game.Scripts.Systems.Field
{
    public sealed class FieldWallsView : MonoBehaviour
    {
        [SerializeField] private WallEntity leftWall;
        [SerializeField] private WallEntity rightWall;

        private List<WallEntity> _entities;

        public IReadOnlyCollection<WallEntity> Entities => GetEntities();

        private Tween _rescaleTween;

        private float _currentXSize;
        private bool _active;
        public float CurrentXSize => _currentXSize;
        public bool Active => _active;

        public event Action WallActivationComplete = () => { };
        public event Action<Collider> AnyWallTriggeredCollider = (trigger) => { };

        private void Awake()
        {
            GetEntities();
            Deactivate(0);
        }

        private void Start()
        {
            foreach (WallEntity entity in _entities)
            {
                entity.WallEnteredTrigger += OnWallEnteredTrigger;
            }
        }

        private void OnDestroy()
        {
            foreach (WallEntity entity in _entities)
            {
                entity.WallEnteredTrigger -= OnWallEnteredTrigger;
            }
        }
        

        private void OnWallEnteredTrigger(Collider other)
        {
            AnyWallTriggeredCollider.Invoke(other);
        }

        private List<WallEntity> GetEntities()
        {
            return _entities ??= new List<WallEntity>
            {
                leftWall,
                rightWall
            };
        }

        public void UpdateWalls(Rect cameraRect)
        {
            leftWall.transform.position = new Vector3(cameraRect.xMin, cameraRect.center.y);
            Vector3 leftWallScale = leftWall.transform.localScale;
            leftWallScale.Set(_currentXSize, cameraRect.height, leftWallScale.z);
            leftWall.transform.localScale = leftWallScale;
            
            rightWall.transform.position = new Vector3(cameraRect.xMax, cameraRect.center.y);
            Vector3 rightWallScale = rightWall.transform.localScale;
            rightWallScale.Set(_currentXSize, cameraRect.height, rightWallScale.z);
            rightWall.transform.localScale = rightWallScale;
        }

        public void Deactivate(float duration)
        {
            _rescaleTween.Kill(true);
            _rescaleTween = DOTween.To(() => _currentXSize, value => _currentXSize = value, 0, duration)
                .OnComplete(() =>
                {
                    SetActive(false);
                });
        }

        public void Activate(float width, float duration)
        {
            SetActive(true);
            _rescaleTween.Kill(true);
            _rescaleTween = DOTween.To(() => _currentXSize, value => _currentXSize = value, width * 2, duration)
                .OnComplete(() => WallActivationComplete.Invoke());
        }

        private void SetActive(bool state)
        {
            _active = state;
            foreach (WallEntity wallEntity in _entities)
            {
                wallEntity.gameObject.SetActive(_active);
            }
        }
    }
}