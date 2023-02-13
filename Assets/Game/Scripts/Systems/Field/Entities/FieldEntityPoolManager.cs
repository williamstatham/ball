using System;
using Game.Scripts.Core.Pooling;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Systems.Field.Entities
{
    public sealed class FieldEntityPoolManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private PrefabPool<LifeWithdrawEntity> lifeWithdrawEntities = new();
        [SerializeField] private PrefabPool<ScoreEntity> scoreEntities = new();

        void IInitializable.Initialize()
        {
            lifeWithdrawEntities.Initialize(gameObject);
            scoreEntities.Initialize(gameObject);
        }

        public T GetEntity<T>() where T : FieldEntityBase
        {
            Type entityType = typeof(T);
            if (entityType == typeof(LifeWithdrawEntity))
            {
                lifeWithdrawEntities.ObjectPool.Get(out LifeWithdrawEntity lifeWithdrawEntity);
                return lifeWithdrawEntity as T;
            }
            if (entityType == typeof(ScoreEntity))
            {
                scoreEntities.ObjectPool.Get(out ScoreEntity scoreEntity);
                return scoreEntity as T;
            }

            throw new NotSupportedException();
        }

        public void ReleaseEntity<T>(T instance) where T : FieldEntityBase
        {
            switch (instance)
            {
                case LifeWithdrawEntity lifeWithdrawEntity:
                    lifeWithdrawEntities.ObjectPool.Release(lifeWithdrawEntity);
                    break;
                case ScoreEntity scoreEntity:
                    scoreEntities.ObjectPool.Release(scoreEntity);
                    break;
                default: throw new NotSupportedException();
            }
        }
    }
}