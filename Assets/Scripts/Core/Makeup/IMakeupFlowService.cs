using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupFlowService
    {
        void StartStep(MakeupStyle style);
        void OnHandReleased(Vector2 screenPos);
        void Reset();
    }
}
