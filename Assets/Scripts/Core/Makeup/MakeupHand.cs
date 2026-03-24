using System;
using UI.Animations;
using UnityEngine;

namespace Core.Makeup
{
    public class MakeupHand : MonoBehaviour
    {
        [SerializeField] private RectTransform handRoot;
        [SerializeField] private RectTransform handDefaultAnchored;
        [SerializeField] private RectTransform itemPosition;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animations")] 
        [SerializeField] private JumpTween jumpTween;
        [SerializeField] private MoveTween moveTween;
        [SerializeField] private FadeTween fadeTween;
        [SerializeField] private MakeupTween makeupTween;

        public event Action<Vector2> OnHandRelease;

        private RectTransform _handParent;
        private bool _isDragging;

        private void Start()
        {
            _handParent = handRoot != null ? handRoot.parent as RectTransform : null;
            canvasGroup.alpha = 0;
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

        public void PlayEntranceHandAnimation(Action onComplete)
        {
            fadeTween.Setup(1f, onComplete);
            fadeTween.Play();
        }

        public void MoveHand(RectTransform position, Action onComplete)
        {
            moveTween.Setup(position, onComplete);
            moveTween.Play();
        }

        public void PlayMakeupAnimation(Action onComplete)
        {
            makeupTween.Setup(onComplete);
            makeupTween.Play();
        }

        public void PlayPickupAnimation(RectTransform itemRect, Action onComplete)
        {
            jumpTween.Setup(itemRect, onComplete);
            jumpTween.Play();
        }

        public void PlayFinishMakeupAnimation(RectTransform itemDefaultPosition, Action onComplete)
        {
            moveTween.Setup(itemDefaultPosition, () =>
            {
                RemoveItemGraphics();
                onComplete?.Invoke();
                moveTween.Setup(handDefaultAnchored, () =>
                {
                    fadeTween.Setup(0f, () => {});
                    fadeTween.Play();
                });
                moveTween.Play();
            });
            moveTween.Play();
        }

        public void SetItemGraphics(GameObject graphics)
        {
            Instantiate(graphics, itemPosition);
        }

        public void EnableDragging(bool enable)
        {
            _isDragging = enable;
        }

        private void RemoveItemGraphics()
        {
            foreach (Transform child in itemPosition.transform)
            {
                Destroy(child.gameObject);
            }
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

        private Vector2 GetPointerPosition()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }

            return Input.mousePosition;
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
    }
}