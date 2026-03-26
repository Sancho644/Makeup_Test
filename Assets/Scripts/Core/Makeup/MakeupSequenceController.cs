using System;
using System.Collections.Generic;
using Core.Makeup.Domain;
using Core.Makeup.Events;
using Core.Makeup.Strategies;
using Core.Makeup.Views;
using GameEvents;
using UnityEngine;

namespace Core.Makeup
{
    public class MakeupSequenceController : IMakeupSequenceController, IDisposable
    {
        private readonly IMakeupStepResolver _stepResolver;
        private readonly IHandPresentation _handPresentation;
        private readonly IMakeupResultRenderer _resultRenderer;
        private readonly IFaceZoneChecker _faceZoneChecker;
        private readonly IGameEventsDispatcher _gameEventsDispatcher;

        private MakeupStyle _currentStyle;
        private bool _hasStep;

        private readonly Dictionary<MakeupType, AbstractMakeupStrategy> _makeupStrategies;

        public MakeupSequenceController(
            IMakeupStepResolver stepResolver,
            IHandPresentation handPresentation,
            IMakeupResultRenderer resultRenderer,
            IFaceZoneChecker faceZoneChecker,
            IGameEventsDispatcher gameEventsDispatcher)
        {
            _stepResolver = stepResolver;
            _handPresentation = handPresentation;
            _resultRenderer = resultRenderer;
            _faceZoneChecker = faceZoneChecker;
            _gameEventsDispatcher = gameEventsDispatcher;

            _gameEventsDispatcher.AddListener<MakeupEndEvent>(OnMakeupEnd);

            _makeupStrategies = new Dictionary<MakeupType, AbstractMakeupStrategy>()
            {
                {
                    MakeupType.Cream,
                    new CreamMakeupStrategy(_stepResolver, _handPresentation, _resultRenderer, _gameEventsDispatcher)
                },
                {
                    MakeupType.Eyeshadow,
                    new EyeshadowMakeupStrategy(_stepResolver, _handPresentation, _resultRenderer, _gameEventsDispatcher)
                },
                {
                    MakeupType.Lipstick,
                    new LipstickMakeupStrategy(_stepResolver, _handPresentation, _resultRenderer, _gameEventsDispatcher)
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