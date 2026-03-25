using Core.Makeup.Events;
using GameEvents;

namespace Core.Makeup
{
    public abstract class AbstractMakeupStrategy
    {
        public abstract MakeupStepRuntime Step { get; protected set; }

        protected readonly IMakeupStepProvider StepProvider;
        protected readonly IMakeupHandView HandView;
        protected readonly IMakeupResultRenderer ResultRenderer;
        protected readonly IGameEventsDispatcher GameEventsDispatcher;

        public AbstractMakeupStrategy(
            IMakeupStepProvider stepProvider,
            IMakeupHandView handView,
            IMakeupResultRenderer resultRenderer,
            IGameEventsDispatcher gameEventsDispatcher)
        {
            StepProvider = stepProvider;
            HandView = handView;
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