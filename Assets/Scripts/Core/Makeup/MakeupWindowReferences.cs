using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Makeup
{
    public class MakeupWindowReferences : MonoBehaviour
    {
        [Serializable]
        public class SceneStep
        {
            public MakeupType Type;
            public RectTransform ItemRoot;
            public RectTransform ItemDefaultPosition;
            public RectTransform MakeupPosition;
            public GameObject ItemGraphics;
        }

        [SerializeField] private List<SceneStep> steps;

        public bool TryGetSceneStep(MakeupType type, out SceneStep step)
        {
            step = steps.FirstOrDefault(x => x.Type == type);
            return step != null;
        }
    }
}