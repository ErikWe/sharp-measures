namespace SharpMeasures.Generators.Scalars.SourceBuilding.Maths;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType scalar, NamedType unit, bool implementSum, bool implementDifference, NamedType? difference, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        Unit = unit;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        SourceBuildingContext = sourceBuildingContext;
    }
}
