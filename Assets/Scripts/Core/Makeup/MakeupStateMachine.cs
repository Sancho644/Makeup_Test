using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using Zenject;

namespace Core.Makeup
{
    public class MakeupStateMachine : MonoBehaviour
    {
        [SerializeField] private HandView handView;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;
        [Inject] private readonly IMakeupFlowService _makeupFlowService;

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
            _makeupFlowService.OnHandReleased(@event.Position);
        }

        private void OnTapHandler(TapMakeupHandlerEvent @event)
        {
            _makeupFlowService.StartStep(@event.Style);
        }
    }
}