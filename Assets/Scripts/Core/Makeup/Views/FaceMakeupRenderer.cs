using System.Collections.Generic;
using Core.Makeup.Domain;
using Core.Makeup.Events;
using Core.Makeup.Input;
using GameEvents;
using UnityEngine;
using Zenject;

namespace Core.Makeup.Views
{
    public class FaceMakeupRenderer : MonoBehaviour, IMakeupResultRenderer
    {
        [SerializeField] private List<MakeupSlot> slots;

        [Inject] private readonly IGameEventsDispatcher _eventsDispatcher;

        private Dictionary<MakeupStyle, MakeupItemAnimator> _map;

        private void Awake()
        {
            _eventsDispatcher.AddListener<MakeupEraseEvent>(OnMakeupErase);
        }

        private void Start()
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

        private void OnDestroy()
        {
            _eventsDispatcher.RemoveListener<MakeupEraseEvent>(OnMakeupErase);
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

        private void OnMakeupErase(MakeupEraseEvent @event)
        {
            foreach (var makeup in _map)
            {
                if (makeup.Key.Type == MakeupType.Cream)
                {
                    continue;
                }
                
                makeup.Value.PlayMakeupAnimation(0);
            }
        }
    }
}