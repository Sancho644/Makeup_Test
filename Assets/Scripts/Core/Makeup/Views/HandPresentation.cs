using System;
using Core.Makeup.Input;
using UnityEngine;

namespace Core.Makeup.Views
{
    [RequireComponent(typeof(HandAnimator), typeof(HandDragController))]
    public class HandPresentation : MonoBehaviour, IHandPresentation
    {
        [SerializeField] private HandAnimator handAnimator;
        [SerializeField] private HandDragController handDragController;

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

        public void PlayMakeup(Action onComplete)
        {
            handAnimator.PlayMakeupAnimation(onComplete);
        }

        public void ReturnTo(Action onComplete)
        {
            handAnimator.PlayFinishMakeupAnimation(onComplete);
        }

        public void EnableDragging(bool enable)
        {
            handDragController.EnableDragging(enable);
        }
    }
}