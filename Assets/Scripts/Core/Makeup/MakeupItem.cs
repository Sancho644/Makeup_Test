using UI.Animations;
using UnityEngine;

namespace Core.Makeup
{
    public class MakeupItem : MonoBehaviour
    {
        [SerializeField] private bool enableOnStart;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private FadeTween fadeTween;

        private void Start()
        {
            if (!enableOnStart)
            {
                canvasGroup.alpha = 0;
            }
        }

        public void PlayMakeupAnimation(float alphaValue)
        {
            fadeTween.Setup(alphaValue, () => {});
            fadeTween.Play();
        }
    }
}