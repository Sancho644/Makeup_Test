using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Toggle))]
    public class MakeupTab : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private GameObject activeGraphics;
        [SerializeField] private GameObject inactiveGraphics;
        [SerializeField] private GameObject activeTab;

        private void Awake()
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void Start()
        {
            Refresh(toggle.isOn);
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool value)
        {
            Refresh(value);
        }

        private void Refresh(bool value)
        {
            activeGraphics.SetActive(value);
            inactiveGraphics.SetActive(!value);
            activeTab.SetActive(value);
        }
    }
}