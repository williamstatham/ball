using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Scripts.Systems.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private ParticleSystem dieVFX;
        
        public Bounds Bounds => collider.bounds;

        public event Action<Collider> OnPlayerEnterTrigger = (trigger) => { }; 

        private void Start()
        {
            collider.gameObject
                .OnTriggerEnterAsObservable()
                .Subscribe(OnColliderEnterTrigger)
                .AddTo(this);
        }

        private void OnColliderEnterTrigger(Collider other)
        {
            OnPlayerEnterTrigger.Invoke(other);
        }

        public void showDieVFX()
        {
            renderer.enabled = false;
            Instantiate(dieVFX, transform.position, Quaternion.identity, transform);
        }

        public void Reset()
        {
            renderer.enabled = true;
        }
    }
}