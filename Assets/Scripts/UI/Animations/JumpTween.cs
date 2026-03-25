using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class JumpTween : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 100f;
        [SerializeField] private float moveDuration = 0.6f;
        [SerializeField] private float jumpPower = 50f;
        [SerializeField] private float jumpDuration = 1f;
        [SerializeField] private int numJumps = 1;

        private Sequence _sequence;
        private Transform _targetPosition;
        private Action _onComplete;

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

            var startPos = transform.position;
            var upPos = startPos + Vector3.up * jumpHeight;
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOJump(upPos, jumpPower, numJumps, jumpDuration).SetEase(Ease.OutQuad));
            _sequence.Append(transform.DOMove(_targetPosition.position, moveDuration).SetEase(Ease.InOutQuad));
            _sequence.OnComplete(() => { _onComplete?.Invoke(); });
            _sequence.Play();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}