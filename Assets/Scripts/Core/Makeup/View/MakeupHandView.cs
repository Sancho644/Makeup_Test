using System;
using UnityEngine;

namespace Core.Makeup.View
{
    public class MakeupHandView : MonoBehaviour, IMakeupHandView
    {
        [SerializeField] private MakeupHand hand;

        public void ShowHand(Action onComplete)
        {
            if (hand == null)
            {
                onComplete?.Invoke();
                return;
            }

            hand.PlayEntranceHandAnimation(onComplete);
        }

        public void PickUp(RectTransform itemRoot, Action onComplete)
        {
            if (hand == null)
            {
                onComplete?.Invoke();
                return;
            }

            hand.PlayPickupAnimation(itemRoot, onComplete);
        }
        
        public void SetItemGraphics(GameObject graphics)
        {
            if (hand == null)
            {
                return;
            }

            hand.SetItemGraphics(graphics);
        }

        public void MoveTo(RectTransform target, Action onComplete)
        {
            if (hand == null)
            {
                onComplete?.Invoke();
                return;
            }

            hand.MoveHand(target, onComplete);
        }

        public void PlayApply(Action onComplete)
        {
            if (hand == null)
            {
                onComplete?.Invoke();
                return;
            }

            hand.PlayMakeupAnimation(onComplete);
        }

        public void ReturnTo(RectTransform itemDefaultPosition, Action onComplete)
        {
            if (hand == null)
            {
                onComplete?.Invoke();
                return;
            }

            hand.PlayFinishMakeupAnimation(itemDefaultPosition, onComplete);
        }

        public void EnableDragging(bool enable)
        {
            if (hand == null)
            {
                return;
            }

            hand.EnableDragging(enable);
        }
    }
}
