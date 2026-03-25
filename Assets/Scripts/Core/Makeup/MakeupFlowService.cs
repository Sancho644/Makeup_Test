using System;
using System.Collections.Generic;
using Core.Makeup.Events;
using GameEvents;
using UnityEngine;

namespace Core.Makeup
{
    public class MakeupFlowService : IMakeupFlowService, IDisposable
    {
        private readonly IMakeupStepProvider _stepProvider;
        private readonly IMakeupHandView _handView;
        private readonly IMakeupResultRenderer _resultRenderer;
        private readonly IFaceZoneChecker _faceZoneChecker;
        private readonly IGameEventsDispatcher _gameEventsDispatcher;

        private MakeupStyle _currentStyle;
        private bool _hasStep;

        private readonly Dictionary<MakeupType, AbstractMakeupStrategy> _makeupStrategies;

        public MakeupFlowService(
            IMakeupStepProvider stepProvider,
            IMakeupHandView handView,
            IMakeupResultRenderer resultRenderer,
            IFaceZoneChecker faceZoneChecker,
            IGameEventsDispatcher gameEventsDispatcher)
        {
            _stepProvider = stepProvider;
            _handView = handView;
            _resultRenderer = resultRenderer;
            _faceZoneChecker = faceZoneChecker;
            _gameEventsDispatcher = gameEventsDispatcher;

            _gameEventsDispatcher.AddListener<MakeupEndEvent>(OnMakeupEnd);

            _makeupStrategies = new Dictionary<MakeupType, AbstractMakeupStrategy>()
            {
                {
                    MakeupType.Cream,
                    new CreamMakeupStrategy(_stepProvider, _handView, _resultRenderer, _gameEventsDispatcher)
                },
                {
                    MakeupType.Eyeshadow,
                    new EyesShadowsMakeupStrategy(_stepProvider, _handView, _resultRenderer, _gameEventsDispatcher)
                },
                {
                    MakeupType.Lipstick,
                    new LipstickMakeupStrategy(_stepProvider, _handView, _resultRenderer, _gameEventsDispatcher)
                }
            };
        }

        public void StartStep(MakeupStyle style)
        {
            if (_hasStep)
            {
                return;
            }

            _currentStyle = style;

            if (_makeupStrategies.TryGetValue(_currentStyle.Type, out var strategy))
            {
                _hasStep = true;
                strategy.Start(style);
            }
        }

        public void OnHandReleased(Vector2 screenPos)
        {
            if (!_hasStep)
            {
                return;
            }

            if (_faceZoneChecker != null && _faceZoneChecker.IsInFaceZone(screenPos))
            {
                if (_makeupStrategies.TryGetValue(_currentStyle.Type, out var strategy))
                {
                    strategy.OnHandReleased();
                }
            }
        }

        public void Reset()
        {
            _hasStep = false;
        }

        public void Dispose()
        {
            _gameEventsDispatcher.RemoveListener<MakeupEndEvent>(OnMakeupEnd);
        }

        private void OnMakeupEnd(MakeupEndEvent @event)
        {
            Reset();
        }
    }
}