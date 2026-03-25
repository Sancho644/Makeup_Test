namespace Core.Makeup
{
    public interface IMakeupStepProvider
    {
        public bool TryGetStep(MakeupStyle style, out MakeupStepData step);
    }
}
