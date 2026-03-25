using Core.Makeup.Events;
using GameEvents;
using UI.Animations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Makeup
{
    public class MakeupBrushColorController : MonoBehaviour
    {
        [SerializeField] private GameObject brushTip;
        [SerializeField] private Image brushTipImage;
        [SerializeField] private FadeTween fadeTween;

        [Inject] private readonly IMakeupStepResolver _stepResolver;
        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;

        private void Awake()
        {
            _gameEventsDispatcher.AddListener<BrushColorChangedEvent>(OnBrushColorChanged);
            _gameEventsDispatcher.AddListener<MakeupEndEvent>(OnMakeupEnd);
        }

        private void Start()
        {
            brushTip.SetActive(false);
            fadeTween.Setup(0, () => { });
            fadeTween.Play();
        }

        private void OnDestroy()
        {
            _gameEventsDispatcher.RemoveListener<BrushColorChangedEvent>(OnBrushColorChanged);
            _gameEventsDispatcher.RemoveListener<MakeupEndEvent>(OnMakeupEnd);
        }

        private void OnBrushColorChanged(BrushColorChangedEvent @event)
        {
            if (_stepResolver.TryGetStep(@event.Style, out var step))
            {
                brushTipImage.sprite = step.BrushColorSprite;
                brushTip.SetActive(true);
                fadeTween.Setup(1, () => { });
                fadeTween.Play();
            }
        }

        private void OnMakeupEnd(MakeupEndEvent @event)
        {
            fadeTween.Setup(0, () => { brushTip.SetActive(false); });
            fadeTween.Play();
        }
    }
}