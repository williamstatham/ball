using UnityEngine;

namespace Game.Scripts.Systems.Field.Entities
{
    public abstract class FieldEntityBase : MonoBehaviour
    {
        [SerializeField] private new Collider collider;

        public Collider Collider => collider;
        public Bounds Bounds => collider.bounds;
    }
}