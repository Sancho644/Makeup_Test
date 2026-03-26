using Core.Makeup.Domain;
using Core.Makeup.Input;
using UnityEngine;

namespace Core.Makeup.Views
{
    [RequireComponent(typeof(MakeupItemAnimator))]
    public class MakeupSlot : MonoBehaviour
    {
        public MakeupStyle Style;
        public MakeupItemAnimator ItemAnimator;
    }
}