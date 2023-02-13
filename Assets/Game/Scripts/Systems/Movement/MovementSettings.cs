using UnityEngine;

namespace Game.Scripts.Systems.Movement
{
    [CreateAssetMenu(fileName = nameof(MovementSettings), menuName = "Descriptors/Movement/" + nameof(MovementSettings))]
    public sealed class MovementSettings : ScriptableObject
    {
        [SerializeField] private Vector2 speed = Vector2.one;

        public Vector2 Speed => speed;
    }
}