using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeTween : MonoBehaviour
    {
        [SerializeField] private Ease ease = Ease.InOutSine;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float duration = 0.5f;

        private Action _onComplete;
        private float _targetFade;
        private Sequence _sequence;

        public void Setup(float targetFade, Action onComplete)
        {
            _targetFade = targetFade;
            _onComplete = onComplete;
        }

        public void Play()
        {
            _sequence?.Kill(true);

            _sequence = DOTween.Sequence();

            _sequence.Append(canvasGroup.DOFade(_targetFade, duration).SetEase(ease));
            _sequence.OnComplete(() => { _onComplete?.Invoke(); });
            _sequence.Play();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}