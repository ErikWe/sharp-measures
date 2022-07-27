namespace SharpMeasures.Generators.Vectors.Documentation;

using System;

internal class DefaultVectorGroupDocumentation : IVectorGroupDocumentationStrategy, IEquatable<DefaultVectorGroupDocumentation>
{
    public DefaultVectorGroupDocumentation(VectorGroupDataModel model) { }

    public virtual bool Equals(DefaultVectorGroupDocumentation? other) => other is not null;

    public override bool Equals(object? obj) => obj is DefaultVectorGroupDocumentation other && Equals(other);
    
    public static bool operator ==(DefaultVectorGroupDocumentation? lhs, DefaultVectorGroupDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultVectorGroupDocumentation? lhs, DefaultVectorGroupDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
