using UnityEngine;

namespace Core.Makeup
{
    public class MakeupFlowService : IMakeupFlowService
    {
        private readonly IMakeupStepProvider _stepProvider;
        private readonly IMakeupHandView _handView;
        private readonly IMakeupResultRenderer _resultRenderer;
        private readonly IFaceZoneChecker _faceZoneChecker;

        private MakeupState _state;
        private MakeupStepRuntime _currentStep;
        private bool _hasStep;

        public MakeupState CurrentState => _state;

        public MakeupFlowService(
            IMakeupStepProvider stepProvider,
            IMakeupHandView handView,
            IMakeupResultRenderer resultRenderer,
            IFaceZoneChecker faceZoneChecker)
        {
            _stepProvider = stepProvider;
            _handView = handView;
            _resultRenderer = resultRenderer;
            _faceZoneChecker = faceZoneChecker;
            _state = MakeupState.Idle;
        }

        public void StartStep(MakeupStyle style)
        {
            if (_state != MakeupState.Idle)
            {
                return;
            }

            if (_stepProvider == null || !_stepProvider.TryGetStep(style, out _currentStep))
            {
                _hasStep = false;
                return;
            }

            _hasStep = true;
            _state = MakeupState.Pickup;

            if (_currentStep.Style.Type == MakeupType.Cream)
            {
                _handView.ShowHand(() =>
                {
                    _handView.PickUp(_currentStep.ItemRoot, () =>
                    {
                        _handView.SetItemGraphics(_currentStep.ItemGraphics);
                        _currentStep.ItemGraphics.SetActive(false);
                        _handView.MoveTo(_currentStep.MakeupPosition, () =>
                        {
                            _handView.EnableDragging(true);
                            _state = MakeupState.Control;
                        });
                    });
                });
            }

            if (_currentStep.Style.Type == MakeupType.Eyeshadow)
            {
                _handView.ShowHand(() =>
                {
                    _handView.PickUp(_currentStep.ItemRoot, () =>
                    {
                        _handView.SetItemGraphics(_currentStep.ItemGraphics);
                        _currentStep.ItemGraphics.SetActive(false);
                        _handView.MoveTo(_currentStep.ColorPalettePosition,
                            () =>
                            {
                                _handView.PlayPickColor(() =>
                                {
                                    _handView.MoveTo(_currentStep.MakeupPosition, () =>
                                    {
                                        _handView.EnableDragging(true);
                                        _state = MakeupState.Control;
                                    });
                                });
                            });
                    });
                });
            }
        }

        public void OnHandReleased(Vector2 screenPos)
        {
            if (_state != MakeupState.Control || !_hasStep)
            {
                return;
            }

            if (_faceZoneChecker != null && _faceZoneChecker.IsInFaceZone(screenPos))
            {
                _state = MakeupState.Apply;
                _handView.EnableDragging(false);

                _handView.PlayApply(() =>
                {
                    _handView.ReturnTo(_currentStep.ItemDefaultPosition, () =>
                    {
                        _currentStep.ItemRoot.position = _currentStep.ItemDefaultPosition.position;
                        _currentStep.ItemGraphics.SetActive(true);
                        _state = MakeupState.Idle;
                    });
                });

                _resultRenderer?.ApplyMakeup(_currentStep.Style, _currentStep.ResultAlpha);
            }
        }

        public void Reset()
        {
            _state = MakeupState.Idle;
            _hasStep = false;
        }
    }
}