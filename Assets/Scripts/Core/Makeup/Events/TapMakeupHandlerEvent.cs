using Core.Makeup.Domain;
using GameEvents;

namespace Core.Makeup.Events
{
    public class TapMakeupHandlerEvent : IGameEvent
    {
        public readonly MakeupStyle Style;

        public TapMakeupHandlerEvent(MakeupStyle style)
        {
            Style = style;
        }
    }
}