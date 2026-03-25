using Core.Makeup.Domain;
using UnityEngine;

namespace Core.Makeup
{
    public interface IMakeupSequenceController
    {
        public void StartStep(MakeupStyle style);
        public void OnHandReleased(Vector2 screenPos);
        public void Reset();
    }
}
