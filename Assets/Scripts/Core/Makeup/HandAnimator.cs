using System;
using UI.Animations;
using UnityEngine;

namespace Core.Makeup
{
    public class HandAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform handDefaultPosition;
        [SerializeField] private RectTransform itemPosition;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animations")] 
        [SerializeField] private MoveTween moveTween;
        [SerializeField] private FadeTween fadeTween;
        [SerializeField] private MakeupTween makeupTween;

        public RectTransform ItemPosition => itemPosition;
        
        private void Start()
        {
            canvasGroup.alpha = 0;
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

        public void PlayFinishMakeupAnimation(RectTransform itemDefaultPosition, Action onComplete)
        {
            moveTween.Setup(itemDefaultPosition, () =>
            {
                onComplete?.Invoke();
                moveTween.Setup(handDefaultPosition, () =>
                {
                    fadeTween.Setup(0f, () => { });
                    fadeTween.Play();
                });
                moveTween.Play();
            });
            moveTween.Play();
        }
    }
}