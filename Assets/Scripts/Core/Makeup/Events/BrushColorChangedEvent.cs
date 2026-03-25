using Core.Makeup.Domain;
using GameEvents;

namespace Core.Makeup.Events
{
    public class BrushColorChangedEvent : IGameEvent
    {
        public MakeupStyle Style;

        public BrushColorChangedEvent(MakeupStyle style)
        {
            Style = style;
        }
    }
}