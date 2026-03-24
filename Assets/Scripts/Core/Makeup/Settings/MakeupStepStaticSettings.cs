using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Makeup.Settings
{
    [CreateAssetMenu(
        fileName = "MakeupStepStaticSettings",
        menuName = "ScriptableObjects/MakeupStepStaticSettings")]
    public class MakeupStepStaticSettings : ScriptableObject
    {
        [Serializable]
        public class StaticStep
        {
            public MakeupStyle Style;
            public float ResultAlpha;
        }

        [SerializeField] private List<StaticStep> steps;

        public bool TryGetStaticStep(MakeupStyle style, out StaticStep step)
        {
            step = steps.FirstOrDefault(x => x.Style.Equals(style));
            return step != null;
        }
    }
}
