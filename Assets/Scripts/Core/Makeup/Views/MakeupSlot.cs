using Core.Makeup.Domain;
using UnityEngine;

namespace Core.Makeup.View
{
    [RequireComponent(typeof(MakeupItemAnimator))]
    public class MakeupSlot : MonoBehaviour
    {
        public MakeupStyle Style;
        public MakeupItemAnimator ItemAnimator;
    }
}