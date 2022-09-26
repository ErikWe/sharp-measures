namespace SharpMeasures.Generators.Vectors.Pipelines.Groups.MemberFactory;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Group { get; }

    public NamedType Unit { get; }
    public string UnitParameterName { get; }

    public NamedType? Scalar { get; }

    public IReadOnlyDictionary<int, NamedType> MembersByDimension { get; }

    public IGroupDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType group, NamedType unit, string unitParameterName, NamedType? scalar, IReadOnlyDictionary<int, NamedType> membersByDimension, IGroupDocumentationStrategy documentation)
    {
        Group = group;

        Unit = unit;
        UnitParameterName = unitParameterName;

        Scalar = scalar;

        MembersByDimension = membersByDimension.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
