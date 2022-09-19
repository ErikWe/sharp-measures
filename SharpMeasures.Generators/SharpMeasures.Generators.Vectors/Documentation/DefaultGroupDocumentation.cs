namespace SharpMeasures.Generators.Vectors.Documentation;

using System;

internal sealed class DefaultGroupDocumentation : IGroupDocumentationStrategy, IEquatable<DefaultGroupDocumentation>
{
    public bool Equals(DefaultGroupDocumentation? other) => other is not null;
    public override bool Equals(object? obj) => obj is DefaultGroupDocumentation other && Equals(other);
    
    public static bool operator ==(DefaultGroupDocumentation? lhs, DefaultGroupDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultGroupDocumentation? lhs, DefaultGroupDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
