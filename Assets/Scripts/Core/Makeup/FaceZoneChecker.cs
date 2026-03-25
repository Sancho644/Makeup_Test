using UnityEngine;

namespace Core.Makeup.View
{
    public class FaceZoneChecker : MonoBehaviour, IFaceZoneChecker
    {
        [SerializeField] private RectTransform faceZone;

        public bool IsInFaceZone(Vector2 screenPos)
        {
            if (faceZone == null)
            {
                return false;
            }

            return RectTransformUtility.RectangleContainsScreenPoint(
                faceZone,
                screenPos,
                null);
        }
    }
}
