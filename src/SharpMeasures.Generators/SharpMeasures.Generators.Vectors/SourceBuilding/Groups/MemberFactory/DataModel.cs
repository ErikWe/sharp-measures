namespace SharpMeasures.Generators.Vectors.SourceBuilding.Groups.MemberFactory;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Group { get; }

    public NamedType Unit { get; }
    public string UnitParameterName { get; }

    public NamedType? Scalar { get; }

    public IReadOnlyDictionary<int, NamedType> MembersByDimension { get; }

    public GroupSourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType group, NamedType unit, string unitParameterName, NamedType? scalar, IReadOnlyDictionary<int, NamedType> membersByDimension, GroupSourceBuildingContext sourceBuildingContext)
    {
        Group = group;

        Unit = unit;
        UnitParameterName = unitParameterName;

        Scalar = scalar;

        MembersByDimension = membersByDimension.AsReadOnlyEquatable();

        SourceBuildingContext = sourceBuildingContext;
    }
}
