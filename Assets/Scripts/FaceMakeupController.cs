using UnityEngine;

namespace DefaultNamespace
{
    public class FaceMakeupController : MonoBehaviour
    {
        [SerializeField] private MakeupItem acne;

        public void EnableMakeup(float makeupAlpha, MakeupType makeupType)
        {
            if (makeupType == MakeupType.Cream)
            {
                acne.PlayMakeupAnimation(makeupAlpha);
            }
        }
    }
}