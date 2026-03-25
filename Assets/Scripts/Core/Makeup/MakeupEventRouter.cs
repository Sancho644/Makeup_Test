using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using Zenject;

namespace Core.Makeup
{
    public class MakeupEventRouter : MonoBehaviour
    {
        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;
        [Inject] private readonly IMakeupSequenceController _makeupSequenceController;

        private void Awake()
        {
            _gameEventsDispatcher.AddListener<TapMakeupHandlerEvent>(OnTapHandler);
            _gameEventsDispatcher.AddListener<HandlePointerReleaseEvent>(OnHandReleased);
        }

        private void OnDestroy()
        {
            _gameEventsDispatcher.RemoveListener<TapMakeupHandlerEvent>(OnTapHandler);
            _gameEventsDispatcher.RemoveListener<HandlePointerReleaseEvent>(OnHandReleased);
        }

        private void OnHandReleased(HandlePointerReleaseEvent @event)
        {
            _makeupSequenceController.OnHandReleased(@event.Position);
        }

        private void OnTapHandler(TapMakeupHandlerEvent @event)
        {
            _makeupSequenceController.StartStep(@event.Style);
        }
    }
}