using GameEvents;

namespace Core.Makeup
{
    public class CreamMakeupStrategy : AbstractMakeupStrategy
    {
        public override MakeupStepRuntime Step { get; protected set; }

        public CreamMakeupStrategy(IMakeupStepProvider stepProvider, IMakeupHandView handView,
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
                var itemPosition = HandView.GetHandItemPosition();
                Step.MakeupApplicatorAnimator.PlayJumpAnimation(itemPosition,
                    () =>
                    {
                        Step.ItemRoot.transform.SetParent(itemPosition);
                        HandView.MoveTo(Step.PrepareMakeupPosition, () => { HandView.EnableDragging(true); });
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

            HandView.EnableDragging(false);

            HandView.MoveTo(Step.MakeupPosition, () =>
            {
                HandView.PlayMakeup(() =>
                {
                    HandView.MoveTo(Step.ItemDefaultPosition, () =>
                    {
                        Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition);
                        Step.ItemRoot.position = Step.ItemDefaultPosition.position;
                        HandView.ReturnTo(End);
                    });
                });

                ResultRenderer?.ApplyMakeup(Step.Style, Step.ResultAlpha);
            });
        }
    }
}