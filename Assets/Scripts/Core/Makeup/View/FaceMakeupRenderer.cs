using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Makeup.View
{
    public class FaceMakeupRenderer : MonoBehaviour, IMakeupResultRenderer
    {
        [Serializable]
        private class Slot
        {
            public MakeupStyle Style;
            public MakeupItem Item;
        }

        [SerializeField] private List<Slot> slots;

        private Dictionary<MakeupStyle, MakeupItem> _map;

        private void Awake()
        {
            _map = new Dictionary<MakeupStyle, MakeupItem>();

            foreach (var slot in slots)
            {
                if (slot != null && slot.Item != null)
                {
                    _map[slot.Style] = slot.Item;
                }
            }
        }

        public void ApplyMakeup(MakeupStyle style, float alpha)
        {
            if (_map == null)
            {
                return;
            }

            if (_map.TryGetValue(style, out var item))
            {
                item.PlayMakeupAnimation(alpha);
            }
        }
    }
}