using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class MakeupTapHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject graphics;

        public event Action OnPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPressed?.Invoke();
        }

        public void EnableImage(bool enable)
        {
            graphics.SetActive(enable);
        }
    }
}