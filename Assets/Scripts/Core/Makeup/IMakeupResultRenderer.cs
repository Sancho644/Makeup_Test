namespace Core.Makeup
{
    public interface IMakeupResultRenderer
    {
        void ApplyMakeup(MakeupType type, float alpha);
    }
}