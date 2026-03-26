using Core.Makeup.Domain;
using Core.Makeup.Views;
using GameEvents;
using UnityEngine;

namespace Core.Makeup.Strategies
{
    public class LipstickMakeupStrategy : AbstractMakeupStrategy
    {
        public override MakeupStepData Step { get; protected set; }

        public LipstickMakeupStrategy(IMakeupStepResolver stepResolver, IHandPresentation handPresentation,
            IMakeupResultRenderer resultRenderer, IGameEventsDispatcher gameEventsDispatcher) : base(stepResolver,
            handPresentation, resultRenderer, gameEventsDispatcher)
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
                .Step(done => HandPresentation.MoveTo(Step.ItemDefaultPosition, done))
                .Step(done =>
                {
                    var itemPosition = HandPresentation.GetHandItemPosition();
                    Step.ItemRoot.SetParent(itemPosition, true);
                    Step.MakeupApplicatorAnimator.PlayJumpAnimation(itemPosition, () =>
                    {
                        Step.ItemRoot.anchoredPosition = Vector2.zero;
                        Step.ItemRoot.localScale = Vector3.one;
                        Step.ItemRoot.localRotation = Quaternion.identity;
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
                    Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition, false);
                    Step.ItemRoot.anchoredPosition = Vector2.zero;
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
