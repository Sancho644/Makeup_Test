using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class ZigZagTween : MonoBehaviour
    {
        [SerializeField] private int steps = 6;
        [SerializeField] private float stepX = 2f;
        [SerializeField] private float stepY = 1f;
        [SerializeField] private float duration = 1f;

        private Action _onComplete;
        private Sequence _sequence;

        public void Setup(Action onComplete)
        {
            _onComplete = onComplete;
        }

        public void Play()
        {
            _sequence?.Kill(true);
            
            _sequence = DOTween.Sequence();
            
            var pos = transform.position;
            var goRight = true;

            for (int i = 0; i < steps; i++)
            {
                float dir = goRight ? 1 : -1;

                pos += new Vector3(dir * stepX, -stepY, 0);

                _sequence.Append(transform.DOMove(pos, duration)
                    .SetEase(Ease.InOutSine));

                goRight = !goRight;
            }
            _sequence.OnComplete(() => { _onComplete?.Invoke(); });
            _sequence.Play();
        }
        
        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}