using UnityEngine;

namespace Core.Makeup
{
    public interface IFaceZoneChecker
    {
        bool IsInFaceZone(Vector2 screenPos);
    }
}
