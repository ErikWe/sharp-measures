namespace SharpMeasures.Generators.Vectors.Documentation;

using System;

internal class VectorGroupDocumentationTags : IVectorGroupDocumentationStrategy, IEquatable<VectorGroupDocumentationTags>
{
    public VectorGroupDocumentationTags() { }
    
    public virtual bool Equals(VectorGroupDocumentationTags? other) => other is not null;
    public override bool Equals(object? obj) => obj is VectorGroupDocumentationTags other && Equals(other);

    public static bool operator ==(VectorGroupDocumentationTags? lhs, VectorGroupDocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(VectorGroupDocumentationTags? lhs, VectorGroupDocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
