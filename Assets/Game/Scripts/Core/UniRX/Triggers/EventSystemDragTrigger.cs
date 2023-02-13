using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Core.UniRX.Triggers
{
    [DisallowMultipleComponent]
    public sealed class EventSystemDragTrigger : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler
    {
        private Subject<PointerEventData> _click;
        private Subject<PointerEventData> _beginDrag;
        private Subject<PointerEventData> _drag;
        private Subject<PointerEventData> _endDrag;

        public IObservable<PointerEventData> OnBeginDragAsObservable()
        {
            return _beginDrag ??= new Subject<PointerEventData>();
        }

        public IObservable<PointerEventData> OnDragAsObservable()
        {
            return _drag ??= new Subject<PointerEventData>();
        }

        public IObservable<PointerEventData> OnEndDragAsObservable()
        {
            return _endDrag ??= new Subject<PointerEventData>();
        }

        public IObservable<PointerEventData> OnClickAsObservable()
        {
            return _click ??= new Subject<PointerEventData>();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _click?.OnNext(eventData);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _beginDrag?.OnNext(eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _drag?.OnNext(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _endDrag?.OnNext(eventData);
        }
    }
}