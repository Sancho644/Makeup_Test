using System;
using UI.Animations;
using UnityEngine;

namespace Core.Makeup
{
    public class HandAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform handDefaultAnchored;
        [SerializeField] private RectTransform itemPosition;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animations")] 
        [SerializeField] private JumpTween jumpTween;
        [SerializeField] private MoveTween moveTween;
        [SerializeField] private FadeTween fadeTween;
        [SerializeField] private MakeupTween makeupTween;

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
                    fadeTween.Setup(0f, () => { });
                    fadeTween.Play();
                });
                moveTween.Play();
            });
            moveTween.Play();
        }

        public void PlayPickColorAnimation(Action onComplete)
        {
            
        }

        public void SetItemGraphics(GameObject graphics)
        {
            Instantiate(graphics, itemPosition);
        }

        private void RemoveItemGraphics()
        {
            foreach (Transform child in itemPosition.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}