namespace SharpMeasures.Generators.Vectors.Documentation;

using System;

internal sealed class GroupDocumentationTags : IGroupDocumentationStrategy, IEquatable<GroupDocumentationTags>
{
    public GroupDocumentationTags() { }

    public string Header() => "Header";

    public string ScalarFactoryMethod(int dimension) => $"ScalarFactory_{dimension}";
    public string VectorFactoryMethod(int dimension) => $"VectorFactory_{dimension}";
    public string ComponentsFactoryMethod(int dimension) => $"ComponentsFactory_{dimension}";

    public bool Equals(GroupDocumentationTags? other) => other is not null;
    public override bool Equals(object? obj) => obj is GroupDocumentationTags other && Equals(other);

    public static bool operator ==(GroupDocumentationTags? lhs, GroupDocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(GroupDocumentationTags? lhs, GroupDocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
