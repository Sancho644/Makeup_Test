using System;
using UnityEngine;

namespace Core.Makeup
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private RectTransform handRoot;
        
        private bool _isDragging;
        private RectTransform _handParent;
        
        public event Action<Vector2> OnHandRelease;

        private void Start()
        {
            _handParent = handRoot != null ? handRoot.parent as RectTransform : null;
        }
        
        private void Update()
        {
            if (_isDragging && GetPointerHeld())
            {
                FollowPointer();
            }

            if (_isDragging && GetPointerUp())
            {
                HandlePointerRelease();
            }
        }
        
        public void EnableDragging(bool enable)
        {
            _isDragging = enable;
        }
        
        private bool GetPointerHeld()
        {
            if (Input.touchCount > 0)
            {
                var phase = Input.GetTouch(0).phase;
                return phase == TouchPhase.Moved || phase == TouchPhase.Stationary;
            }

            return Input.GetMouseButton(0);
        }

        private void FollowPointer()
        {
            if (handRoot == null || _handParent == null)
            {
                return;
            }

            var screenPos = GetPointerPosition();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _handParent,
                screenPos,
                null,
                out Vector2 localPoint
            );

            handRoot.anchoredPosition = localPoint;
        }
        
        private bool GetPointerUp()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).phase == TouchPhase.Ended
                       || Input.GetTouch(0).phase == TouchPhase.Canceled;
            }

            return Input.GetMouseButtonUp(0);
        }

        private void HandlePointerRelease()
        {
            OnHandRelease?.Invoke(GetPointerPosition());
        }
        
        private Vector2 GetPointerPosition()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }

            return Input.mousePosition;
        }
    }
}