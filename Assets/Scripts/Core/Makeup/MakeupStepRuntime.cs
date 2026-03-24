using UnityEngine;

namespace Core.Makeup
{
    public readonly struct MakeupStepRuntime
    {
        public readonly MakeupStyle Style;
        public readonly RectTransform ItemRoot;
        public readonly RectTransform ItemDefaultPosition;
        public readonly RectTransform MakeupPosition;
        public readonly RectTransform ColorPalettePosition;
        public readonly GameObject ItemGraphics;
        public readonly float ResultAlpha;

        public MakeupStepRuntime(
            MakeupStyle style,
            RectTransform itemRoot,
            RectTransform itemDefaultPosition,
            RectTransform makeupPosition,
            RectTransform colorPalettePosition,
            GameObject itemGraphics,
            float resultAlpha)
        {
            Style = style;
            ItemRoot = itemRoot;
            ItemDefaultPosition = itemDefaultPosition;
            MakeupPosition = makeupPosition;
            ColorPalettePosition = colorPalettePosition;
            ItemGraphics = itemGraphics;
            ResultAlpha = resultAlpha;
        }
    }
}