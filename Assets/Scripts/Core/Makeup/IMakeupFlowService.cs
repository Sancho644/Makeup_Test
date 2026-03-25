using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupFlowService
    {
        public void StartStep(MakeupStyle style);
        public void OnHandReleased(Vector2 screenPos);
        public void Reset();
    }
}
