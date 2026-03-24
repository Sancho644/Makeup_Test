using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class JumpTween : MonoBehaviour
    {
        [SerializeField] private RectTransform targetPosition;
        [SerializeField] private float jumpHeight = 100f;
        [SerializeField] private float moveDuration = 0.6f;
        [SerializeField] private float jumpPower = 50f;
        [SerializeField] private float jumpDuration = 1f;
        [SerializeField] private int numJumps = 1;

        private Sequence _sequence;
        private Transform _itemPosition;
        private Action _onComplete;

        public void Setup(RectTransform itemPosition, Action onComplete)
        {
            _itemPosition = itemPosition;
            _onComplete = onComplete;
        }

        public void Play()
        {
            if (_itemPosition == null)
            {
                return;
            }

            _sequence?.Kill(true);

            var startPos = _itemPosition.position;
            var upPos = (startPos + Vector3.up + Vector3.right) * jumpHeight;
            _sequence = DOTween.Sequence();

            _sequence.Append(_itemPosition.DOJump(upPos, jumpPower, numJumps, jumpDuration).SetEase(Ease.OutQuad));
            _sequence.Append(_itemPosition.DOMove(targetPosition.position, moveDuration).SetEase(Ease.InOutQuad));
            _sequence.OnComplete(() => { _onComplete?.Invoke(); });
            _sequence.Play();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}