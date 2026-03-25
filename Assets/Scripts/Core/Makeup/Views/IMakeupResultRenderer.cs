using Core.Makeup.Domain;

namespace Core.Makeup
{
    public interface IMakeupResultRenderer
    {
        public void ApplyMakeup(MakeupStyle style, float alpha);
    }
}