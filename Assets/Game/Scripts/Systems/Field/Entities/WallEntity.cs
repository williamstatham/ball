using System;
using UnityEngine;

namespace Game.Scripts.Systems.Field.Entities
{
    public class WallEntity : FieldEntityBase
    {
        public event Action<Collider> WallEnteredTrigger = (trigger) => { }; 

        private void OnTriggerEnter(Collider other)
        {
            WallEnteredTrigger.Invoke(other);
        }
    }
}