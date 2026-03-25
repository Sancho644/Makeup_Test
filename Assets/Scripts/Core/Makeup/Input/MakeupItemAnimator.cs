using UI.Animations;
using UnityEngine;

namespace Core.Makeup
{
    [RequireComponent(typeof(CanvasGroup), typeof(FadeTween))]
    public class MakeupItemAnimator : MonoBehaviour
    {
        [SerializeField] private bool enableOnStart;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private FadeTween fadeTween;

        private void Start()
        {
            if (!enableOnStart)
            {
                SetAlpha(0);
            }
        }

        public void PlayMakeupAnimation(float alphaValue)
        {
            fadeTween.Setup(alphaValue, () => { });
            fadeTween.Play();
        }
        
        public void SetAlpha(float alphaValue)
        {
            canvasGroup.alpha = alphaValue;
        }
    }
}