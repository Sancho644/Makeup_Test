using GameEvents;
using UnityEngine;

namespace Core.Makeup
{
    public class LipstickMakeupStrategy : AbstractMakeupStrategy
    {
        public override MakeupStepRuntime Step { get; protected set; }

        public LipstickMakeupStrategy(IMakeupStepProvider stepProvider, IMakeupHandView handView,
            IMakeupResultRenderer resultRenderer, IGameEventsDispatcher gameEventsDispatcher) : base(stepProvider,
            handView, resultRenderer, gameEventsDispatcher)
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
                    Step.ItemRoot.SetParent(itemPosition, false);
                    Step.ItemRoot.anchoredPosition = Vector2.zero;
                    Step.ItemRoot.localScale = Vector3.one;
                    Step.ItemRoot.localRotation = Quaternion.identity;
                    HandView.MoveTo(Step.PrepareMakeupPosition, () => { HandView.EnableDragging(true); });
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

            HandView.PlayMakeup(() =>
            {
                HandView.ReturnTo(Step.ItemDefaultPosition, () =>
                {
                    Step.ItemRoot.transform.SetParent(Step.ItemDefaultPosition);
                    Step.ItemRoot.position = Step.ItemDefaultPosition.position;
                    End();
                });
            });

            ResultRenderer?.ApplyMakeup(Step.Style, Step.ResultAlpha);
        }
    }
}