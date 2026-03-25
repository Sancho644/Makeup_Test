using Core.Makeup.Domain;
using GameEvents;
using UnityEngine;

namespace Core.Makeup
{
    public class EyeshadowMakeupStrategy : AbstractMakeupStrategy
    {
        public override MakeupStepData Step { get; protected set; }

        public EyeshadowMakeupStrategy(IMakeupStepResolver stepResolver, IHandPresentation handPresentation,
            IMakeupResultRenderer resultRenderer, IGameEventsDispatcher gameEventsDispatcher) : base(stepResolver,
            handPresentation,
            resultRenderer, gameEventsDispatcher)
        {
        }

        public override void Start(MakeupStyle style)
        {
            if (StepResolver == null || !StepResolver.TryGetStep(style, out var step))
            {
                End();
                return;
            }

            Step = step;

            HandPresentation.ShowHand(() =>
            {
                HandPresentation.MoveTo(Step.ItemDefaultPosition, () =>
                {
                    var itemPosition = HandPresentation.GetHandItemPosition();
                    Step.ItemRoot.SetParent(itemPosition, true);
                    Step.MakeupApplicatorAnimator.PlayJumpAnimation(itemPosition, () =>
                    {
                        Step.ItemRoot.anchoredPosition = Vector2.zero;
                        Step.ItemRoot.localScale = Vector3.one;
                        Step.ItemRoot.localRotation = Quaternion.identity;
                        HandPresentation.MoveTo(Step.ColorPalettePosition,
                            () =>
                            {
                                HandPresentation.PlayMakeup(() =>
                                {
                                    HandPresentation.MoveTo(Step.PrepareMakeupPosition,
                                        () => { HandPresentation.EnableDragging(true); });
                                });
                            });
                    });
                });
            });
        }

        public override void OnHandReleased()
        {
            if (Step.IsEmpty())
            {
                return;
            }

            HandPresentation.EnableDragging(false);

            HandPresentation.MoveTo(Step.MakeupPosition, () =>
            {
                HandPresentation.PlayMakeup(() =>
                {
                    HandPresentation.MoveTo(Step.ItemDefaultPosition, () =>
                    {
                        Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition, false);
                        Step.ItemRoot.anchoredPosition = Vector2.zero;
                        HandPresentation.ReturnTo(End);
                    });
                });

                ResultRenderer?.ApplyMakeup(Step.Style, Step.ResultAlpha);
            });
        }
    }
}