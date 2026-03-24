using GameEvents;

namespace Core.Makeup.Events
{
    public class TapMakeupHandlerEvent : IGameEvent
    {
        public readonly MakeupType Type;

        public TapMakeupHandlerEvent(MakeupType type)
        {
            Type = type;
        }
    }
}