using UnityEngine;

namespace Core.Makeup
{
    public interface IFaceZoneChecker
    {
        public bool IsInFaceZone(Vector2 screenPos);
    }
}
