namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.SourceBuilding;

internal static class VectorDocumentationTags
{
    public const string VectorHeader = nameof(VectorHeader);

    public const string Zero = nameof(Zero);

    public static string Component(int componentIndex, int dimension) => $"Component_{VectorTexts.GetUpperCasedComponentName(componentIndex, dimension)}";
    public const string Components = nameof(Components);

    public const string Constructor_Components = nameof(Constructor_Components);
    public const string Constructor_ComponentTuple = nameof(Constructor_ComponentTuple);
    public const string Constructor_Scalars = nameof(Constructor_Scalars);
    public const string Constructor_ScalarTuple = nameof(Constructor_ScalarTuple);
    public const string Constructor_Doubles = nameof(Constructor_Doubles);
    public const string Constructor_DoubleTuple = nameof(Constructor_DoubleTuple);
    public const string Constructor_Vector = nameof(Constructor_Vector);

    public const string Constructor_Unit_Scalars = nameof(Constructor_Unit_Scalars);
    public const string Constructor_Unit_ScalarTuple = nameof(Constructor_Unit_ScalarTuple);
    public const string Constructor_Unit_Doubles = nameof(Constructor_Unit_Doubles);
    public const string Constructor_Unit_DoubleTuple = nameof(Constructor_Unit_DoubleTuple);
    public const string Constructor_Unit_Vector = nameof(Constructor_Unit_Vector);
}
