using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Systems.Field.Entities;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Game.Scripts.Systems.Field
{
    public class FieldManager : IStartable, ITickable, IDisposable
    {
        private UnityEngine.Camera _camera;
        private GameSettings _gameSettings;
        private FieldWallsView _fieldWallsView;

        private FieldEntityPoolManager _entityPoolManager;

        private float _smallEntitiesSpawnTime;
        private float _wallsEventTime;

        private readonly List<FieldEntityBase> _fieldEntities = new();

        [Inject]
        public void Construct(
            UnityEngine.Camera camera,
            GameSettings gameSettings,
            FieldWallsView fieldWallsView,
            FieldEntityPoolManager entityPoolManager
            )
        {
            _camera = camera;
            _gameSettings = gameSettings;
            _fieldWallsView = fieldWallsView;
            _entityPoolManager = entityPoolManager;
        }
        
        void IStartable.Start()
        {
            _fieldEntities.AddRange(_fieldWallsView.Entities);
            _fieldWallsView.AnyWallTriggeredCollider += OnWallsTriggeredEntity;
            _fieldWallsView.WallActivationComplete += OnWallsActivationCompleted;
        }

        void IDisposable.Dispose()
        {
            _fieldWallsView.AnyWallTriggeredCollider -= OnWallsTriggeredEntity;
            _fieldWallsView.WallActivationComplete -= OnWallsActivationCompleted;
        }
        
        void ITickable.Tick()
        {
            _fieldWallsView.UpdateWalls(GetCameraRect());
        }
        
        private void OnWallsTriggeredEntity(Collider other)
        {
            FieldEntityBase fieldEntity = GetFieldEntity(i => i is not WallEntity && i.Collider == other);
            if (fieldEntity != null)
            {
                if (fieldEntity is ICanDieFieldEntity canDieFieldEntity)
                {
                    canDieFieldEntity.ShowDeathVFX();
                }
                
                ReleaseEntity(fieldEntity);
            }
        }

        private Rect GetCameraRect()
        {
            Vector2 min = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = _camera.ViewportToWorldPoint(new Vector2(1, 1));

            return new Rect(min, max - min);
        }

        private Rect GetFieldRect()
        {
            Vector2 min = _camera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = _camera.ViewportToWorldPoint(new Vector2(1, 1));

            return new Rect(min + new Vector2(_fieldWallsView.CurrentXSize, 0), max - min - new Vector2(_fieldWallsView.CurrentXSize, 0));
        }

        public FieldEntityBase GetFieldEntity(Predicate<FieldEntityBase> match)
        {
            return _fieldEntities.Find(match);
        }

        public void UpdateField(float gameSpeed, float deltaTime)
        {
            CheckSmallEntitiesSpawnTime(gameSpeed, deltaTime);
            CheckWallsEventTime(gameSpeed, deltaTime);
        }

        private void CheckSmallEntitiesSpawnTime(float gameSpeed, float deltaTime)
        {
            _smallEntitiesSpawnTime = Mathf.Clamp(_smallEntitiesSpawnTime + deltaTime, 0,
                _gameSettings.DefaultEntitySpawnTime / gameSpeed);

            if (Mathf.Approximately(_smallEntitiesSpawnTime, _gameSettings.DefaultEntitySpawnTime / gameSpeed))
            {
                CreateEntity();
                _smallEntitiesSpawnTime = 0f;
            }
            
            for (int i = _fieldEntities.Count - 1; i >= 0; i--)
            {
                FieldEntityBase fieldEntity = _fieldEntities[i];

                if (fieldEntity is WallEntity)
                {
                    continue;
                }

                Rect fieldRect = GetFieldRect();

                if (fieldEntity.transform.position.y <= fieldRect.yMin - 5)
                {
                    ReleaseEntity(fieldEntity, i);
                }
            }
        }

        private void CheckWallsEventTime(float gameSpeed, float deltaTime)
        {
            if (_fieldWallsView.Active)
            {
                return;
            }
            _wallsEventTime = Mathf.Clamp(_wallsEventTime + deltaTime, 0,
                _gameSettings.TryTriggerWallsEventEachSeconds / gameSpeed);

            if (Mathf.Approximately(_wallsEventTime, _gameSettings.TryTriggerWallsEventEachSeconds / gameSpeed))
            {
                if (_gameSettings.TryTriggerWalls())
                {
                    _fieldWallsView.Activate(Random.Range(_gameSettings.MinMaxWallsSize.x, _gameSettings.MinMaxWallsSize.y), 
                        _gameSettings.WallsEventAppearanceTimeSeconds);
                }
                _wallsEventTime = 0f;
            }
        }
        
        private void OnWallsActivationCompleted()
        {
            OnWallsActivationCompletedAsync().Forget();
        }

        private async UniTaskVoid OnWallsActivationCompletedAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_gameSettings.WallsEventDurationSeconds));
            _fieldWallsView.Deactivate(_gameSettings.WallsEventAppearanceTimeSeconds);
        }

        private void CreateEntity()
        {
            FieldEntityBase entity;
            switch (Random.Range(0, 2))
            {
                case 0:
                    entity = _entityPoolManager.GetEntity<LifeWithdrawEntity>();
                    break;
                case 1:
                    entity = _entityPoolManager.GetEntity<ScoreEntity>();
                    break;
                default:
                    goto case 0;
            }

            _fieldEntities.Add(entity);
            entity.gameObject.SetActive(true);

            float entityWidth = entity.Bounds.max.x - entity.Bounds.min.x;
            
            Rect fieldRect = GetFieldRect();
            
            entity.transform.position =
                new Vector3(Random.Range(fieldRect.xMin + entityWidth / 2, fieldRect.xMax - entityWidth / 2), fieldRect.yMax + 5);
        }

        public void ReleaseEntity(FieldEntityBase entity)
        {
            int index = _fieldEntities.IndexOf(entity);
            if (index != -1)
            {
                ReleaseEntity(entity, index);
            }
        }

        private void ReleaseEntity(FieldEntityBase entity, int entityIndex)
        {
            _fieldEntities.RemoveAt(entityIndex);
            _entityPoolManager.ReleaseEntity(entity);
        }

        public void Reset()
        {
            for (int i = _fieldEntities.Count - 1; i >= 0; i--)
            {
                FieldEntityBase fieldEntity = _fieldEntities[i];
                
                if (fieldEntity is WallEntity)
                {
                    continue;
                }
                
                ReleaseEntity(fieldEntity, i);
            }
            
            _fieldWallsView.Deactivate(0);
            _smallEntitiesSpawnTime = 0f;
            _wallsEventTime = 0f;
        }
    }
}