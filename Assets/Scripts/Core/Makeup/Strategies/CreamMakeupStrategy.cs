using Core.Makeup.Domain;
using Core.Makeup.Views;
using GameEvents;

namespace Core.Makeup.Strategies
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

            new ActionSequence()
                .Step(done => HandPresentation.ShowHand(done))
                .Step(done =>
                {
                    var itemPosition = HandPresentation.GetHandItemPosition();
                    Step.MakeupApplicatorAnimator.PlayJumpAnimation(itemPosition, () =>
                    {
                        Step.ItemRoot.transform.SetParent(itemPosition);
                        done();
                    });
                })
                .Step(done => HandPresentation.MoveTo(Step.PrepareMakeupPosition, done))
                .Step(done =>
                {
                    HandPresentation.EnableDragging(true);
                    done();
                })
                .Start();
        }

        public override void OnHandReleased()
        {
            if (Step.IsEmpty())
            {
                End();
                return;
            }

            new ActionSequence()
                .Step(done =>
                {
                    HandPresentation.EnableDragging(false);
                    done();
                })
                .Step(done => HandPresentation.MoveTo(Step.MakeupPosition, done))
                .Step(done =>
                {
                    ResultRenderer?.ApplyMakeup(Step.Style, Step.ResultAlpha);
                    HandPresentation.PlayMakeup(done);
                })
                .Step(done => HandPresentation.MoveTo(Step.ItemDefaultPosition, done))
                .Step(done =>
                {
                    Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition);
                    Step.ItemRoot.position = Step.ItemDefaultPosition.position;
                    done();
                })
                .Step(done => HandPresentation.ReturnTo(() =>
                {
                    End();
                    done();
                }))
                .Start();
        }
    }
}
