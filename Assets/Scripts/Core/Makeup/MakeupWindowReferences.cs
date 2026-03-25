using System;
using System.Collections.Generic;
using System.Linq;
using Core.Makeup.Domain;
using UnityEngine;

namespace Core.Makeup
{
    public class MakeupWindowReferences : MonoBehaviour
    {
        [Serializable]
        public class SceneStep
        {
            public MakeupStyle Style;
            public RectTransform ItemRoot;
            public RectTransform ItemDefaultPosition;
            public RectTransform PrepareMakeupPosition;
            public RectTransform MakeupPosition;
            public RectTransform ColorPalettePosition;
            public MakeupApplicatorAnimator MakeupApplicatorAnimator;
        }

        [SerializeField] private List<SceneStep> steps;

        public bool TryGetSceneStep(MakeupStyle style, out SceneStep step)
        {
            step = steps.FirstOrDefault(x => x.Style.Equals(style));
            return step != null;
        }
    }
}