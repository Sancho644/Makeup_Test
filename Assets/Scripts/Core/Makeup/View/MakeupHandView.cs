using System;
using UnityEngine;

namespace Core.Makeup.View
{
    [RequireComponent(typeof(HandAnimator), typeof(HandView))]
    public class MakeupHandView : MonoBehaviour, IMakeupHandView
    {
        [SerializeField] private HandAnimator handAnimator;
        [SerializeField] private HandView handView;

        public void ShowHand(Action onComplete)
        {
            handAnimator.PlayEntranceHandAnimation(onComplete);
        }

        public RectTransform GetHandItemPosition()
        {
            return handAnimator.ItemPosition;
        }

        public void MoveTo(RectTransform target, Action onComplete)
        {
            handAnimator.MoveHand(target, onComplete);
        }

        public void PlayApply(Action onComplete)
        {
            handAnimator.PlayMakeupAnimation(onComplete);
        }

        public void PlayMakeup(Action onComplete)
        {
            handAnimator.PlayMakeupAnimation(onComplete);
        }

        public void ReturnTo(RectTransform itemDefaultPosition, Action onComplete)
        {
            handAnimator.PlayFinishMakeupAnimation(itemDefaultPosition, onComplete);
        }

        public void EnableDragging(bool enable)
        {
            handView.EnableDragging(enable);
        }
    }
}