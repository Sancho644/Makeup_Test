using Core.Makeup.Domain;
using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Makeup
{
    public class MakeupTapHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private MakeupStyle style;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;

        public void OnPointerDown(PointerEventData eventData)
        {
            _gameEventsDispatcher.Dispatch(new TapMakeupHandlerEvent(style));
        }
    }
}