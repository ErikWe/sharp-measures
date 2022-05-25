namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Utility;

internal static class DocumentationTags
{
    public const string ScalarHeader = nameof(ScalarHeader);

    public const string Zero = nameof(Zero);

    public const string Magnitude = nameof(Magnitude);

    public const string Constructor_Scalar = nameof(Constructor_Scalar);
    public const string Constructor_Double = nameof(Constructor_Double);
    public const string Constructor_DoubleUnit = nameof(Constructor_DoubleUnit);
    public const string Constructor_ScalarUnit = nameof(Constructor_ScalarUnit);

    public const string InUnit = nameof(InUnit);

    new public const string ToString = nameof(ToString);

    public const string ToScalar = nameof(ToScalar);
    public const string ToDouble = nameof(ToDouble);
    public const string FromScalar = nameof(FromScalar);
    public const string FromDouble = nameof(FromDouble);

    public static class Operators
    {
        public const string ToScalar = $"Operator_{nameof(ToScalar)}";
        public const string ToDouble = $"Operator_{nameof(ToDouble)}";
        public const string FromScalar = $"Operator_{nameof(FromScalar)}";
        public const string FromDouble = $"Operator_{nameof(FromDouble)}";
    }

    public static class Units
    {
        public static string BaseWithName(UnitName unitName) => $"One_{unitName.Name}";
        public static string UnitWithName(UnitName unitName) => $"InUnit_{unitName.Name}";
    }

    public static class Comparable
    {
        public const string CompareTo_SameType = nameof(CompareTo_SameType);

        public static class Operators
        {
            public const string LessThan_SameType = $"Operator_{nameof(LessThan_SameType)}";
            public const string GreaterThan_SameType = $"Operator_{nameof(GreaterThan_SameType)}";
            public const string LessThanOrEqual_SameType = $"Operator_{nameof(LessThanOrEqual_SameType)}";
            public const string GreaterThanOrEqual_SameType = $"Operator_{nameof(GreaterThanOrEqual_SameType)}";
        }
    }

    public static class StandardMaths
    {
        public const string IsNaN = nameof(IsNaN);
        public const string IsZero = nameof(IsZero);
        public const string IsPositive = nameof(IsPositive);
        public const string IsNegative = nameof(IsNegative);
        public const string IsFinite = nameof(IsFinite);
        public const string IsInfinity = nameof(IsInfinity);
        public const string IsPositiveInfinity = nameof(IsPositiveInfinity);
        public const string IsNegativeInfinity = nameof(IsNegativeInfinity);

        public const string Absolute = nameof(Absolute);
        public const string Floor = nameof(Floor);
        public const string Ceiling = nameof(Ceiling);
        public const string Round = nameof(Round);
        public const string Truncate = nameof(Truncate);

        public const string Sign = nameof(Sign);

        public const string UnaryPlus = nameof(UnaryPlus);
        public const string Negate = nameof(Negate);

        public const string Multiply_Unhandled = nameof(Multiply_Unhandled);
        public const string Divide_Unhandled = nameof(Divide_Unhandled);

        public const string Remainder_Scalar = nameof(Remainder_Scalar);
        public const string Multiply_Scalar = nameof(Multiply_Scalar);
        public const string Divide_Scalar = nameof(Divide_Scalar);

        public const string Remainder_Double = nameof(Remainder_Double);
        public const string Multiply_Double = nameof(Multiply_Double);
        public const string Divide_Double = nameof(Divide_Double);

        public const string Multiply_Generic = nameof(Multiply_Generic);
        public const string Divide_Generic = nameof(Divide_Generic);

        public const string Multiply_IScalar = nameof(Multiply_IScalar);
        public const string Divide_IScalar = nameof(Divide_IScalar);

        public static class Operators
        {
            public const string UnaryPlus = $"Operator_{nameof(UnaryPlus)}";
            public const string Negate = $"Operator_{nameof(Negate)}";

            public const string Multiply_Unhandled_LHS = $"Operator_{nameof(Multiply_Unhandled_LHS)}";
            public const string Multiply_Unhandled_RHS = $"Operator_{nameof(Multiply_Unhandled_RHS)}";
            public const string Divide_Unhandled_LHS = $"Operator_{nameof(Divide_Unhandled_LHS)}";
            public const string Divide_Unhandled_RHS = $"Operator_{nameof(Divide_Unhandled_RHS)}";

            public const string Remainder_Scalar = $"Operator_{nameof(Remainder_Scalar)}";
            public const string Multiply_Scalar_LHS = $"Operator_{nameof(Multiply_Scalar_LHS)}";
            public const string Multiply_Scalar_RHS = $"Operator_{nameof(Multiply_Scalar_RHS)}";
            public const string Divide_Scalar_LHS = $"Operator_{nameof(Divide_Scalar_LHS)}";
            public const string Divide_Scalar_RHS = $"Operator_{nameof(Divide_Scalar_RHS)}";

            public const string Remainder_Double = $"Operator_{nameof(Remainder_Double)}";
            public const string Multiply_Double_LHS = $"Operator_{nameof(Multiply_Double_LHS)}";
            public const string Multiply_Double_RHS = $"Operator_{nameof(Multiply_Double_RHS)}";
            public const string Divide_Double_LHS = $"Operator_{nameof(Divide_Double_LHS)}";
            public const string Divide_Double_RHS = $"Operator_{nameof(Divide_Double_RHS)}";
        }
    }
}
