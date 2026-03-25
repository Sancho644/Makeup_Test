using Core.Makeup.Events;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Makeup
{
    [RequireComponent(typeof(Button))]
    public class Sponge : MonoBehaviour
    {
        [SerializeField] private Button button;

        [Inject] private readonly IGameEventsDispatcher _eventsDispatcher;

        private void Awake()
        {
            button.onClick.AddListener(OnSpongeClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnSpongeClick);
        }

        private void OnSpongeClick()
        {
            _eventsDispatcher.Dispatch(new MakeupEraseEvent());
        }
    }
}