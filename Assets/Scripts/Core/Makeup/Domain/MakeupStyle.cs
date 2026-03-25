using System;

namespace Core.Makeup.Domain
{
    [Serializable]
    public struct MakeupStyle : IEquatable<MakeupStyle>
    {
        public MakeupType Type;
        public ColorType Color;

        public bool Equals(MakeupStyle other)
        {
            return Type == other.Type && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            return obj is MakeupStyle other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Type, (int)Color);
        }
    }
}