using UnityEngine;

namespace Game.Scripts.Core.Particles
{
    public class DestroyOnParticleSystemStopped : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            Destroy(gameObject);
        }
    }
}