using Core.Makeup.Domain;
using Core.Makeup.Events;
using Core.Makeup.Views;
using GameEvents;

namespace Core.Makeup.Strategies
{
    public abstract class AbstractMakeupStrategy
    {
        public abstract MakeupStepData Step { get; protected set; }

        protected readonly IMakeupStepResolver StepResolver;
        protected readonly IHandPresentation HandPresentation;
        protected readonly IMakeupResultRenderer ResultRenderer;
        protected readonly IGameEventsDispatcher GameEventsDispatcher;

        public AbstractMakeupStrategy(
            IMakeupStepResolver stepResolver,
            IHandPresentation handPresentation,
            IMakeupResultRenderer resultRenderer,
            IGameEventsDispatcher gameEventsDispatcher)
        {
            StepResolver = stepResolver;
            HandPresentation = handPresentation;
            ResultRenderer = resultRenderer;
            GameEventsDispatcher = gameEventsDispatcher;
        }

        public abstract void Start(MakeupStyle style);

        public abstract void OnHandReleased();

        protected void End()
        {
            GameEventsDispatcher.Dispatch(new MakeupEndEvent());
        }
    }
}