using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Makeup.Input
{
    public class HandDragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform handRoot;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;

        private bool _isDragging;
        private RectTransform _handParent;
        private Vector2 _dragOffset;

        private void Start()
        {
            _handParent = handRoot != null ? handRoot.parent as RectTransform : null;
        }

        public void EnableDragging(bool enable)
        {
            _isDragging = enable;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_handParent, eventData.position,
                eventData.pressEventCamera, out var localPoint);

            _dragOffset = handRoot.anchoredPosition - localPoint;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                return;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_handParent, eventData.position,
                eventData.pressEventCamera, out var localPoint);

            handRoot.anchoredPosition = localPoint + _dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                return;
            }

            _gameEventsDispatcher.Dispatch(new HandlePointerReleaseEvent(eventData.position));
        }
    }
}