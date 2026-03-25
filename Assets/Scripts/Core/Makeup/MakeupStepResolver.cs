using Core.Makeup.Domain;
using Core.Makeup.Settings;

namespace Core.Makeup
{
    public class MakeupStepResolver : IMakeupStepResolver
    {
        private readonly MakeupStepStaticSettings _staticSettings;
        private readonly MakeupWindowReferences _windowReferences;

        public MakeupStepResolver(
            MakeupStepStaticSettings staticSettings,
            MakeupWindowReferences windowReferences)
        {
            _staticSettings = staticSettings;
            _windowReferences = windowReferences;
        }

        public bool TryGetStep(MakeupStyle style, out MakeupStepData step)
        {
            step = default;

            if (_staticSettings == null || _windowReferences == null)
            {
                return false;
            }

            if (!_staticSettings.TryGetStaticStep(style, out var staticStep))
            {
                return false;
            }

            if (!_windowReferences.TryGetSceneStep(style, out var sceneStep))
            {
                return false;
            }

            step = new MakeupStepData(
                style,
                sceneStep.ItemRoot,
                sceneStep.ItemDefaultPosition,
                sceneStep.PrepareMakeupPosition,
                sceneStep.MakeupPosition,
                sceneStep.ColorPalettePosition,
                sceneStep.MakeupApplicatorAnimator,
                staticStep.ResultAlpha);

            return true;
        }
    }
}
