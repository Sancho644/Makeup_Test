using Core.Makeup.Settings;

namespace Core.Makeup
{
    public class MakeupStepResolver : IMakeupStepProvider
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

        public bool TryGetStep(MakeupType type, out MakeupStepRuntime step)
        {
            step = default;

            if (_staticSettings == null || _windowReferences == null)
            {
                return false;
            }

            if (!_staticSettings.TryGetStaticStep(type, out var staticStep))
            {
                return false;
            }

            if (!_windowReferences.TryGetSceneStep(type, out var sceneStep))
            {
                return false;
            }

            step = new MakeupStepRuntime(
                type,
                sceneStep.ItemRoot,
                sceneStep.ItemDefaultPosition,
                sceneStep.MakeupPosition,
                sceneStep.ItemGraphics,
                staticStep.ResultAlpha);

            return true;
        }
    }
}
