using Core.Makeup.Domain;

namespace Core.Makeup.Views
{
    public interface IMakeupResultRenderer
    {
        public void ApplyMakeup(MakeupStyle style, float alpha);
    }
}