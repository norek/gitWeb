using System;
using gitWeb.Core.Features.Commit;

namespace gitWeb.Core.GraphBuilder
{
    public class Link : IEquatable<Link>
    {
        public Link(Commit source, Commit target)
        {
            Source = source.Sha;
            Target = target.Sha;
            TargetHIndex = target.HIndex;
            X1 = source.X;
            Y1 = source.Y;

            X2 = target.X;
            Y2 = target.Y;
        }

        public int Y2 { get; private set; }

        public int X2 { get; private set; }

        public int Y1 { get; private set; }

        public int X1 { get; private set; }

        public int TargetHIndex { get; private set; }

        public string Source { get; private set; }
        public string Target { get; private set; }

        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Source, other.Source) && string.Equals(Target, other.Target);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Link)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^ (Target != null ? Target.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"Source: {Source}, Target: {Target}";
        }
    }
}