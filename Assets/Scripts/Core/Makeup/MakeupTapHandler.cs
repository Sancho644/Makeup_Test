using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Makeup
{
    public class MakeupTapHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private MakeupType type;
        [SerializeField] private ColorType color;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;

        public void OnPointerDown(PointerEventData eventData)
        {
            _gameEventsDispatcher.Dispatch(new TapMakeupHandlerEvent(type, color));
        }
    }
}