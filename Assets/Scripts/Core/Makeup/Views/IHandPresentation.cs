using System;
using UnityEngine;

namespace Core.Makeup.Views
{
    public interface IHandPresentation
    {
        public void ShowHand(Action onComplete);
        public void MoveTo(RectTransform target, Action onComplete);
        public void PlayMakeup(Action onComplete);
        public void ReturnTo(Action onComplete);
        public void EnableDragging(bool enable);
        public RectTransform GetHandItemPosition();
    }
}