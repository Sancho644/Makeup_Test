namespace Core.Makeup
{
    public interface IMakeupStepProvider
    {
        bool TryGetStep(MakeupType type, out MakeupStepRuntime step);
    }
}
