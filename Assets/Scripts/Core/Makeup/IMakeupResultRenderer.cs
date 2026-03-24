namespace Core.Makeup
{
    public interface IMakeupResultRenderer
    {
        void ApplyMakeup(MakeupStyle style, float alpha);
    }
}