using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MakeupStateMachine : MonoBehaviour
    {
        [Serializable]
        private class MakeupStepConfig
        {
            public MakeupType Type;
            public MakeupTapHandler TapHandler;
            public RectTransform ItemRoot;
            public RectTransform ItemDefaultPosition;
            public RectTransform MakeupPosition;
            public GameObject ItemGraphics;
            public float ResultAlpha;
        }
        
        [SerializeField] private MakeupHand makeupHand;
        [SerializeField] private FaceMakeupController faceMakeupController;
        [SerializeField] private RectTransform faceZone;
        [SerializeField] private List<MakeupStepConfig> steps;

        private MakeupState _currentState;
        private MakeupType _currentType;
        private MakeupStepConfig _currentStep;

        private void Awake()
        {
            foreach (var step in steps)
            {
                step.TapHandler.OnPressed += ()=> StartStep(step);
            }
            makeupHand.OnHandRelease += OnHandReleased;
        }

        private void Start()
        {
            _currentState = MakeupState.Idle;
        }

        private void OnDestroy()
        {
            makeupHand.OnHandRelease -= OnHandReleased;
        }

        private void StartStep(MakeupStepConfig step)
        {
            if (_currentState != MakeupState.Idle)
            {
                return;
            }

            _currentState = MakeupState.Pickup;
            
            _currentStep = step;
            _currentType = step.Type;
            
            makeupHand.PlayEntranceHandAnimation(() =>
            {
                makeupHand.PlayPickupAnimation(_currentStep.ItemRoot, () =>
                {
                    makeupHand.SetItemGraphics(_currentStep.ItemGraphics);
                    _currentStep.TapHandler.EnableImage(false);
                    makeupHand.MoveHand(_currentStep.MakeupPosition, () =>
                    {
                        makeupHand.EnableDragging(true);
                        _currentState = MakeupState.Control;
                    });
                });
            });
        }

        private void OnHandReleased(Vector2 screenPos)
        {
            if (_currentState != MakeupState.Control)
            {
                return;
            }

            if (IsInFaceZone(screenPos))
            {
                _currentState = MakeupState.Apply;
                makeupHand.EnableDragging(false);
                makeupHand.PlayMakeupAnimation(() =>
                {
                    makeupHand.PlayFinishMakeupAnimation(_currentStep.ItemDefaultPosition,() =>
                    {
                        _currentStep.ItemRoot.position = _currentStep.ItemDefaultPosition.position;
                        _currentStep.TapHandler.EnableImage(true);
                        _currentState = MakeupState.Idle;
                    });
                });

                faceMakeupController.EnableMakeup(_currentStep.ResultAlpha, _currentType);
            }
        }

        private bool IsInFaceZone(Vector2 screenPos)
        {
            if (faceZone == null)
            {
                return false;
            }

            return RectTransformUtility.RectangleContainsScreenPoint(
                faceZone,
                screenPos,
                null
            );
        }
    }
}