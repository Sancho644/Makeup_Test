using System;
using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupHandView
    {
        void ShowHand(Action onComplete);
        void PickUp(RectTransform itemRoot, Action onComplete);
        void MoveTo(RectTransform target, Action onComplete);
        void PlayApply(Action onComplete);
        void ReturnTo(RectTransform itemDefaultPosition, Action onComplete);
        void EnableDragging(bool enable);
        void SetItemGraphics(GameObject graphics);
    }
}
