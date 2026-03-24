using GameEvents;

namespace Core.Makeup.Events
{
    public class TapMakeupHandlerEvent : IGameEvent
    {
        public readonly MakeupStyle Style;
        public readonly MakeupType Type;
        public readonly ColorType Color;

        public TapMakeupHandlerEvent(MakeupType type, ColorType color)
        {
            Type = type;
            Color = color;
            Style = new MakeupStyle()
            {
                Color = color,
                Type =  type
            };
        }
    }
}