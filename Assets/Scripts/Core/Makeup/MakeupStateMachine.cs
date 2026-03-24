using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using Zenject;

namespace Core.Makeup
{
    public class MakeupStateMachine : MonoBehaviour
    {
        [SerializeField] private MakeupHand makeupHand;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;
        [Inject] private readonly IMakeupFlowService _makeupFlowService;

        private MakeupType _currentType;
        private MakeupStepRuntime _currentStep;

        private void Awake()
        {
            _gameEventsDispatcher.AddListener<TapMakeupHandlerEvent>(OnTapHandler);
            makeupHand.OnHandRelease += OnHandReleased;
        }

        private void OnDestroy()
        {
            _gameEventsDispatcher.RemoveListener<TapMakeupHandlerEvent>(OnTapHandler);
            makeupHand.OnHandRelease -= OnHandReleased;
        }

        private void OnTapHandler(TapMakeupHandlerEvent @event)
        {
            _makeupFlowService.StartStep(@event.Type);
        }

        private void OnHandReleased(Vector2 screenPos)
        {
            _makeupFlowService.OnHandReleased(screenPos);
        }
    }
}