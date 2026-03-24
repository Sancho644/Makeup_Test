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
            public MakeupType Type;
            public MakeupItem Item;
        }

        [SerializeField] private List<Slot> slots = new List<Slot>();

        private Dictionary<MakeupType, MakeupItem> _map;

        private void Awake()
        {
            _map = new Dictionary<MakeupType, MakeupItem>();
            foreach (var slot in slots)
            {
                if (slot != null && slot.Item != null)
                {
                    _map[slot.Type] = slot.Item;
                }
            }
        }

        public void ApplyMakeup(MakeupType type, float alpha)
        {
            if (_map == null)
            {
                return;
            }

            if (_map.TryGetValue(type, out var item))
            {
                item.PlayMakeupAnimation(alpha);
            }
        }
    }
}