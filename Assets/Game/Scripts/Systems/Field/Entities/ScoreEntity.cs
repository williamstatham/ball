using Game.Scripts.Core.Pooling;
using UnityEngine;

namespace Game.Scripts.Systems.Field.Entities
{
    public class ScoreEntity : FieldEntityBase, IPoolable, ICanDieFieldEntity
    {
        [SerializeField] private int scoreIncrement;
        [SerializeField] private ParticleSystem deathVFX;

        public int ScoreIncrement => scoreIncrement;

        public void ShowDeathVFX()
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
    }
}