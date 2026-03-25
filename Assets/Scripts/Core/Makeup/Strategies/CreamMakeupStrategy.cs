using Core.Makeup.Domain;
using GameEvents;

namespace Core.Makeup
{
    public class CreamMakeupStrategy : AbstractMakeupStrategy
    {
        public override MakeupStepData Step { get; protected set; }

        public CreamMakeupStrategy(IMakeupStepResolver stepResolver, IHandPresentation handPresentation,
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
                var itemPosition = HandPresentation.GetHandItemPosition();
                Step.MakeupApplicatorAnimator.PlayJumpAnimation(itemPosition,
                    () =>
                    {
                        Step.ItemRoot.transform.SetParent(itemPosition);
                        HandPresentation.MoveTo(Step.PrepareMakeupPosition, () => { HandPresentation.EnableDragging(true); });
                    });
            });
        }

        public override void OnHandReleased()
        {
            if (Step.IsEmpty())
            {
                End();
                return;
            }

            HandPresentation.EnableDragging(false);

            HandPresentation.MoveTo(Step.MakeupPosition, () =>
            {
                HandPresentation.PlayMakeup(() =>
                {
                    HandPresentation.MoveTo(Step.ItemDefaultPosition, () =>
                    {
                        Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition);
                        Step.ItemRoot.position = Step.ItemDefaultPosition.position;
                        HandPresentation.ReturnTo(End);
                    });
                });

                ResultRenderer?.ApplyMakeup(Step.Style, Step.ResultAlpha);
            });
        }
    }
}