using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class MoveTween : MonoBehaviour
    {
        [SerializeField] private float duration = 1f;

        private RectTransform _targetPosition;
        private Action _onComplete;
        private Sequence _sequence;

        public void Setup(RectTransform targetPosition, Action onComplete)
        {
            _targetPosition = targetPosition;
            _onComplete = onComplete;
        }

        public void Play()
        {
            if (_targetPosition == null)
            {
                return;
            }

            _sequence?.Kill(true);

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(_targetPosition.position, duration).SetEase(Ease.InOutSine));
            _sequence.OnComplete(() => { _onComplete?.Invoke(); });
            _sequence.Play();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}