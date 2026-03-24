using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using Zenject;

namespace Core.Makeup
{
    public class MakeupStateMachine : MonoBehaviour
    {
        [SerializeField] private HandAnimator handAnimator;
        [SerializeField] private HandView handView;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;
        [Inject] private readonly IMakeupFlowService _makeupFlowService;

        private void Awake()
        {
            _gameEventsDispatcher.AddListener<TapMakeupHandlerEvent>(OnTapHandler);
            handView.OnHandRelease += OnHandReleased;
        }

        private void OnDestroy()
        {
            _gameEventsDispatcher.RemoveListener<TapMakeupHandlerEvent>(OnTapHandler);
            handView.OnHandRelease -= OnHandReleased;
        }

        private void OnTapHandler(TapMakeupHandlerEvent @event)
        {
            _makeupFlowService.StartStep(@event.Style);
        }

        private void OnHandReleased(Vector2 screenPos)
        {
            _makeupFlowService.OnHandReleased(screenPos);
        }
    }
}