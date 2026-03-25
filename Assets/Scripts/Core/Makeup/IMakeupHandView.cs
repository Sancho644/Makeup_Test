using System;
using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupHandView
    {
        public void ShowHand(Action onComplete);
        public void MoveTo(RectTransform target, Action onComplete);
        public void PlayApply(Action onComplete);
        public void PlayMakeup(Action onComplete);
        public void ReturnTo(RectTransform itemDefaultPosition, Action onComplete);
        public void EnableDragging(bool enable);
        public RectTransform GetHandItemPosition();
    }
}
