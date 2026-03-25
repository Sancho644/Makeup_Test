using Core.Makeup.Domain;

namespace Core.Makeup
{
    public interface IMakeupStepResolver
    {
        public bool TryGetStep(MakeupStyle style, out MakeupStepData step);
    }
}
