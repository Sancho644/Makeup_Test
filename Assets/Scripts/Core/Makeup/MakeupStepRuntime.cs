using UnityEngine;

namespace Core.Makeup
{
    public readonly struct MakeupStepRuntime
    {
        public readonly MakeupType Type;
        public readonly RectTransform ItemRoot;
        public readonly RectTransform ItemDefaultPosition;
        public readonly RectTransform MakeupPosition;
        public readonly GameObject ItemGraphics;
        public readonly float ResultAlpha;

        public MakeupStepRuntime(
            MakeupType type,
            RectTransform itemRoot,
            RectTransform itemDefaultPosition,
            RectTransform makeupPosition,
            GameObject itemGraphics,
            float resultAlpha)
        {
            Type = type;
            ItemRoot = itemRoot;
            ItemDefaultPosition = itemDefaultPosition;
            MakeupPosition = makeupPosition;
            ItemGraphics = itemGraphics;
            ResultAlpha = resultAlpha;
        }
    }
}
