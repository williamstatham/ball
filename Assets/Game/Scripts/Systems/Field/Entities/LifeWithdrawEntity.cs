using Game.Scripts.Core.Pooling;
using UnityEngine;

namespace Game.Scripts.Systems.Field.Entities
{
    public class LifeWithdrawEntity : FieldEntityBase, IPoolable, ICanDieFieldEntity
    {
        [SerializeField] private int withdrawLives;
        [SerializeField] private ParticleSystem deathVFX;

        public int WithdrawLives => withdrawLives;

        public void ShowDeathVFX()
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
    }
}