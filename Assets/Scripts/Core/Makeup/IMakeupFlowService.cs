using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupFlowService
    {
        MakeupState CurrentState { get; }
        void StartStep(MakeupStyle style);
        void OnHandReleased(Vector2 screenPos);
        void Reset();
    }
}
