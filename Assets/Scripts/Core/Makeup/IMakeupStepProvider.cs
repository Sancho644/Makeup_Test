namespace Core.Makeup
{
    public interface IMakeupStepProvider
    {
        bool TryGetStep(MakeupStyle style, out MakeupStepRuntime step);
    }
}
