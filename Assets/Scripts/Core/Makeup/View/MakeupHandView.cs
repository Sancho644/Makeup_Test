using System;
using UnityEngine;

namespace Core.Makeup.View
{
    public class MakeupHandView : MonoBehaviour, IMakeupHandView
    {
        [SerializeField] private HandAnimator handAnimator;
        [SerializeField] private HandView handView;

        public void ShowHand(Action onComplete)
        {
            if (handAnimator == null)
            {
                onComplete?.Invoke();
                return;
            }

            handAnimator.PlayEntranceHandAnimation(onComplete);
        }

        public void PickUp(RectTransform itemRoot, Action onComplete)
        {
            if (handAnimator == null)
            {
                onComplete?.Invoke();
                return;
            }

            handAnimator.PlayPickupAnimation(itemRoot, onComplete);
        }
        
        public void SetItemGraphics(GameObject graphics)
        {
            if (handAnimator == null)
            {
                return;
            }

            handAnimator.SetItemGraphics(graphics);
        }

        public void MoveTo(RectTransform target, Action onComplete)
        {
            if (handAnimator == null)
            {
                onComplete?.Invoke();
                return;
            }

            handAnimator.MoveHand(target, onComplete);
        }

        public void PlayApply(Action onComplete)
        {
            if (handAnimator == null)
            {
                onComplete?.Invoke();
                return;
            }

            handAnimator.PlayMakeupAnimation(onComplete);
        }

        public void PlayPickColor(Action onComplete)
        {
            if (handAnimator == null)
            {
                onComplete?.Invoke();
                return;
            }

            handAnimator.PlayPickColorAnimation(onComplete);
            
        }

        public void ReturnTo(RectTransform itemDefaultPosition, Action onComplete)
        {
            if (handAnimator == null)
            {
                onComplete?.Invoke();
                return;
            }

            handAnimator.PlayFinishMakeupAnimation(itemDefaultPosition, onComplete);
        }

        public void EnableDragging(bool enable)
        {
            if (handAnimator == null)
            {
                return;
            }

            handView.EnableDragging(enable);
        }
    }
}
