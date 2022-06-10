namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

internal static class VectorDocumentationTags
{
    public const string VectorHeader = nameof(VectorHeader);

    public const string Zero = nameof(Zero);

    public static string Component(int componentIndex, int dimension) => $"Component_{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension)}";
    public const string Components = nameof(Components);

    public const string Constructor_Components = nameof(Constructor_Components);
    public const string Constructor_Scalars = nameof(Constructor_Scalars);
    public const string Constructor_Vector = nameof(Constructor_Vector);

    public const string Constructor_Unit_Scalars = nameof(Constructor_Unit_Scalars);
    public const string Constructor_Unit_Vector = nameof(Constructor_Unit_Vector);

    public const string Magnitude = nameof(Magnitude);
    public const string SquaredMagnitude = nameof(SquaredMagnitude);

    public const string InUnit = nameof(InUnit);

    public const string Normalize = nameof(Normalize);
    public const string Transform = nameof(Transform);
    new public const string ToString = nameof(ToString);
    public const string Deconstruct = nameof(Deconstruct);

    public const string Plus = nameof(Plus);
    public const string Negate = nameof(Negate);
    public const string Multiply_Scalar = nameof(Multiply_Scalar);
    public const string Divide_Scalar = nameof(Divide_Scalar);
    public const string Remainder_Scalar = nameof(Remainder_Scalar);

    public static class Units
    {
        public static string ConstantWithName(string name) => $"Constant_{name}";
        public static string ConstantMultiples(string name) => $"InMultiplesOf_{name}";
        public static string UnitWithName(UnitInstance unitName) => $"InUnit_{unitName.Name}";
    }

    public static class Operators
    {
        public const string Plus = $"Operator_{nameof(Plus)}";
        public const string Negate = $"Operator_{nameof(Negate)}";
        public const string Multiply_Scalar_LHS = $"Operator_{nameof(Multiply_Scalar_LHS)}";
        public const string Multiply_Scalar_RHS = $"Operator_{nameof(Multiply_Scalar_RHS)}";
        public const string Divide_Scalar = $"Operator_{nameof(Divide_Scalar)}";
        public const string Remainder_Scalar = $"Operator_{nameof(Remainder_Scalar)}";

        public const string Cast_ComponentTuple = $"Operator_{nameof(Cast_ComponentTuple)}";
    }
}
