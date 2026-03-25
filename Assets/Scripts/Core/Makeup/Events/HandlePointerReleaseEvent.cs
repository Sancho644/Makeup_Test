using GameEvents;
using UnityEngine;

namespace Core.Makeup.Events
{
    public class HandlePointerReleaseEvent : IGameEvent
    {
        public Vector2 Position;

        public HandlePointerReleaseEvent(Vector2 position)
        {
            Position = position;
        }
    }
}