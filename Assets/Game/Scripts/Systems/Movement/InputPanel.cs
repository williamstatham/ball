using System;
using Game.Scripts.Core.UniRX.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Systems.Movement
{
    [DisallowMultipleComponent, RequireComponent(typeof(RawImage), typeof(EventSystemDragTrigger))]
    public sealed class InputPanel : MonoBehaviour
    {
        [SerializeField] private RawImage image;
        [SerializeField] private EventSystemDragTrigger dragTrigger;

        public IObservable<PointerEventData> OnBeginDragAsObservable => dragTrigger.OnBeginDragAsObservable();
        public IObservable<PointerEventData> OnDragAsObservable => dragTrigger.OnDragAsObservable();
        public IObservable<PointerEventData> OnClickAsObservable => dragTrigger.OnClickAsObservable();

        public void EnableInput(bool state)
        {
            image.raycastTarget = state;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            image = GetComponent<RawImage>();
            dragTrigger = GetComponent<EventSystemDragTrigger>();
        }
#endif
    }
}