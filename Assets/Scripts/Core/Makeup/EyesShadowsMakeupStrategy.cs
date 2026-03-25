using GameEvents;
using UnityEngine;

namespace Core.Makeup
{
    public class EyesShadowsMakeupStrategy : AbstractMakeupStrategy
    {
        public override MakeupStepRuntime Step { get; protected set; }

        public EyesShadowsMakeupStrategy(IMakeupStepProvider stepProvider, IMakeupHandView handView,
            IMakeupResultRenderer resultRenderer, IGameEventsDispatcher gameEventsDispatcher) : base(stepProvider,
            handView,
            resultRenderer, gameEventsDispatcher)
        {
        }

        public override void Start(MakeupStyle style)
        {
            if (StepProvider == null || !StepProvider.TryGetStep(style, out var step))
            {
                End();
                return;
            }

            Step = step;

            HandView.ShowHand(() =>
            {
                HandView.MoveTo(Step.ItemDefaultPosition, () =>
                {
                    var itemPosition = HandView.GetHandItemPosition();
                    Step.ItemRoot.SetParent(itemPosition, true);
                    Step.MakeupApplicatorAnimator.PlayJumpAnimation(itemPosition, () =>
                    {
                        Step.ItemRoot.anchoredPosition = Vector2.zero;
                        Step.ItemRoot.localScale = Vector3.one;
                        Step.ItemRoot.localRotation = Quaternion.identity;
                        HandView.MoveTo(Step.ColorPalettePosition,
                            () =>
                            {
                                HandView.PlayMakeup(() =>
                                {
                                    HandView.MoveTo(Step.PrepareMakeupPosition,
                                        () => { HandView.EnableDragging(true); });
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

            HandView.EnableDragging(false);

            HandView.MoveTo(Step.MakeupPosition, () =>
            {
                HandView.PlayMakeup(() =>
                {
                    HandView.MoveTo(Step.ItemDefaultPosition, () =>
                    {
                        Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition, false);
                        Step.ItemRoot.anchoredPosition = Vector2.zero;
                        HandView.ReturnTo(End);
                    });
                });

                ResultRenderer?.ApplyMakeup(Step.Style, Step.ResultAlpha);
            });
        }
    }
}