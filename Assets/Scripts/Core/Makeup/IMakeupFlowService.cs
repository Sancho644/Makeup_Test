using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupFlowService
    {
        MakeupState CurrentState { get; }
        void StartStep(MakeupType type);
        void OnHandReleased(Vector2 screenPos);
        void Reset();
    }
}
