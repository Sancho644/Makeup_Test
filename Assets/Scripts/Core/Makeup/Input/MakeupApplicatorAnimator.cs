using System;
using Core.Makeup.Domain;
using UI.Animations;
using UnityEngine;

namespace Core.Makeup.Input
{
    [RequireComponent(typeof(JumpTween))]
    public class MakeupApplicatorAnimator : MonoBehaviour
    {
        [SerializeField] private MakeupStyle style;
        [SerializeField] private JumpTween jumpTween;

        public void PlayJumpAnimation(RectTransform position, Action onComplete)
        {
            jumpTween.Setup(position, onComplete);
            jumpTween.Play();
        }
    }
}