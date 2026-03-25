using UnityEngine;

namespace Core.Makeup
{
    public readonly struct MakeupStepRuntime
    {
        public readonly MakeupStyle Style;
        public readonly RectTransform ItemRoot;
        public readonly RectTransform ItemDefaultPosition;
        public readonly RectTransform PrepareMakeupPosition;
        public readonly RectTransform MakeupPosition;
        public readonly RectTransform ColorPalettePosition;
        public readonly MakeupApplicatorAnimator MakeupApplicatorAnimator;
        public readonly float ResultAlpha;

        public MakeupStepRuntime(
            MakeupStyle style,
            RectTransform itemRoot,
            RectTransform itemDefaultPosition,
            RectTransform prepareMakeupPosition,
            RectTransform makeupPosition,
            RectTransform colorPalettePosition,
            MakeupApplicatorAnimator makeupApplicatorAnimator,
            float resultAlpha)
        {
            Style = style;
            ItemRoot = itemRoot;
            ItemDefaultPosition = itemDefaultPosition;
            PrepareMakeupPosition = prepareMakeupPosition;
            MakeupPosition = makeupPosition;
            ColorPalettePosition = colorPalettePosition;
            MakeupApplicatorAnimator = makeupApplicatorAnimator;
            ResultAlpha = resultAlpha;
        }

        public bool IsEmpty()
        {
            return ItemRoot == null 
                   && ItemDefaultPosition == null 
                   && PrepareMakeupPosition == null 
                   && ColorPalettePosition == null;
        }
    }
}