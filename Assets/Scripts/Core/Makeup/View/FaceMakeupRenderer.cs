using System.Collections.Generic;
using UnityEngine;

namespace Core.Makeup.View
{
    public class FaceMakeupRenderer : MonoBehaviour, IMakeupResultRenderer
    {
        [SerializeField] private List<MakeupSlot> slots;

        private Dictionary<MakeupStyle, MakeupItemAnimator> _map;

        private void Awake()
        {
            _map = new Dictionary<MakeupStyle, MakeupItemAnimator>();

            foreach (var slot in slots)
            {
                if (slot != null && slot.ItemAnimator != null)
                {
                    _map[slot.Style] = slot.ItemAnimator;
                }
            }
        }

        public void ApplyMakeup(MakeupStyle style, float alpha)
        {
            if (_map == null)
            {
                return;
            }

            foreach (var makeup in _map)
            {
                if (makeup.Key.Type == style.Type && makeup.Key.Color != style.Color)
                {
                    makeup.Value.SetAlpha(0);
                }
            }
            
            if (_map.TryGetValue(style, out var item))
            {
                item.PlayMakeupAnimation(alpha);
            }
        }
    }
}