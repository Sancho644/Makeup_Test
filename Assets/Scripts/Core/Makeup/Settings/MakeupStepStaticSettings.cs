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
            public MakeupType Type;
            public float ResultAlpha;
        }

        [SerializeField] private List<StaticStep> steps;

        public bool TryGetStaticStep(MakeupType type, out StaticStep step)
        {
            step = steps.FirstOrDefault(x => x.Type == type);
            return step != null;
        }
    }
}
