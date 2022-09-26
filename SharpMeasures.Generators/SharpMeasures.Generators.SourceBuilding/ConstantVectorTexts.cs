namespace SharpMeasures.Generators.SourceBuilding;

using System;
using System.Diagnostics.CodeAnalysis;

public static class ConstantVectorTexts
{
    public static string Zeros(int dimension) => Builders.Zeros.GetText(dimension);
    public static string UnnamedScalars(int dimension) => Builders.UnnamedScalars.GetText(dimension);
    public static string SampleValues(int dimension) => Builders.SampleValues.GetText(dimension);
    public static string DeconstructScalarHeader(int dimension) => Builders.DeconstructScalarHeader.GetText(dimension);
    public static string MultiplyScalarLHS(int dimension) => Builders.MultiplyScalarLHS.GetText(dimension);

    [SuppressMessage("Design", "CA1034", Justification = "Static")]
    public static class Upper
    {
        public static string Name(int dimension) => Builders.Upper.Name.GetText(dimension);
        public static string ComponentsAccess(int dimension) => Builders.Upper.ComponentsAccess.GetText(dimension);
        public static string ComponentsPropertyAccess(int dimension) => Builders.Upper.ComponentsPropertyAccess.GetText(dimension);
        public static string Scalar(int dimension) => Builders.Upper.Scalar.GetText(dimension);
        public static string Magnitude(int dimension) => Builders.Upper.Magnitude.GetText(dimension);
        public static string AddAddendVector(int dimension) => Builders.Upper.AddAddendVector.GetText(dimension);
        public static string SubtractSubtrahendVector(int dimension) => Builders.Upper.SubtractSubtrahendVector.GetText(dimension);
        public static string SubtractFromMinuendVector(int dimension) => Builders.Upper.SubtractFromMinuendVector.GetText(dimension);
        public static string MultiplyFactorScalar(int dimension) => Builders.Upper.MultiplyFactorScalar.GetText(dimension);
        public static string DivideDivisorScalar(int dimension) => Builders.Upper.DivideDivisorScalar.GetText(dimension);
        public static string Negate(int dimension) => Builders.Upper.Negate.GetText(dimension);
        public static string NegateA(int dimension) => Builders.Upper.NegateA.GetText(dimension);
        public static string AddBVector(int dimension) => Builders.Upper.AddBVector.GetText(dimension);
        public static string SubtractBVector(int dimension) => Builders.Upper.SubtractBVector.GetText(dimension);
        public static string MultiplyAScalar(int dimension) => Builders.Upper.MultiplyAScalar.GetText(dimension);
        public static string MultiplyBScalar(int dimension) => Builders.Upper.MultiplyBScalar.GetText(dimension);
        public static string DivideAScalar(int dimension) => Builders.Upper.DivideAScalar.GetText(dimension);
    }

    [SuppressMessage("Design", "CA1034", Justification = "Static")]
    public static class Lower
    {
        public static string Name(int dimension) => Builders.Lower.Name.GetText(dimension);
        public static string ComponentsAccess(int dimension) => Builders.Lower.ComponentsAccess.GetText(dimension);
        public static string Scalar(int dimension) => Builders.Lower.Scalar.GetText(dimension);
        public static string NewScalar(int dimension) => Builders.Lower.NewScalar.GetText(dimension);
    }

    [SuppressMessage("Design", "CA1034", Justification = "Static")]
    public static class Builders
    {
        public static VectorTextBuilder Zeros { get; } = new("0", ", ");
        public static VectorTextBuilder UnnamedScalars { get; } = new("global::SharpMeasures.Scalar", ", ");
        public static VectorTextBuilder SampleValues { get; } = new(SampleValuesDelegate, ", ");
        public static VectorTextBuilder DeconstructScalarHeader { get; } = new(DeconstructScalarHeaderDelegate, ", ");
        public static VectorTextBuilder MultiplyScalarLHS { get; } = new(MultiplyScalarLHSDelegate, ", ");
        public static VectorTextBuilder MultiplyScalarRHS { get; } = new(MultiplyScalarRHSDelegate, ", ");

        [SuppressMessage("Design", "CA1034", Justification = "Static")]
        public static class Upper
        {
            public static VectorTextBuilder Name { get; } = new(NameDelegate, ", ");
            public static VectorTextBuilder ComponentsAccess { get; } = new(ComponentsAccessDelegate, ", ");
            public static VectorTextBuilder ComponentsPropertyAccess { get; } = new(ComponentsPropertyAccessDelegate, ", ");
            public static VectorTextBuilder Scalar { get; } = new(ScalarDelegate, ", ");
            public static VectorTextBuilder Magnitude { get; } = new(MagnitudeDelegate, ", ");
            public static VectorTextBuilder AddAddendVector { get; } = new(AddAddendVectorDelegate, ", ");
            public static VectorTextBuilder SubtractSubtrahendVector { get; } = new(SubtractSubtrahendVectorDelegate, ", ");
            public static VectorTextBuilder SubtractFromMinuendVector { get; } = new(SubtractFromMinuendVectorDelegate, ", ");
            public static VectorTextBuilder MultiplyFactorScalar { get; } = new(MultiplyFactorScalarDelegate, ", ");
            public static VectorTextBuilder DivideDivisorScalar { get; } = new(DivideDivisorScalarDelegate, ", ");
            public static VectorTextBuilder Negate { get; } = new(NegateDelegate, ", ");
            public static VectorTextBuilder NegateA { get; } = new(NegateADelegate, ", ");
            public static VectorTextBuilder AddBVector { get; } = new(AddBVectorDelegate, ", ");
            public static VectorTextBuilder SubtractBVector { get; } = new(SubtractBVectorDelegate, ", ");
            public static VectorTextBuilder MultiplyAScalar { get; } = new(MultiplyAScalarDelegate, ", ");
            public static VectorTextBuilder MultiplyBScalar { get; } = new(MultiplyBScalarDelegate, ", ");
            public static VectorTextBuilder DivideAScalar { get; } = new(DivideAScalarDelegate, ", ");

            private static string NameDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)}";
            private static string ComponentsAccessDelegate(int componentIndex, int dimension) => $"components.{GetComponentName(componentIndex, dimension)}";
            private static string ComponentsPropertyAccessDelegate(int componentIndex, int dimension) => $"Components.{GetComponentName(componentIndex, dimension)}";
            private static string ScalarDelegate(int componentIndex, int dimension) => $"global::SharpMeasures.Scalar {GetComponentName(componentIndex, dimension)}";
            private static string MagnitudeDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)}.Magnitude";

            private static string AddAddendVectorDelegate(int componentIndex, int dimension)
            {
                string componentName = GetComponentName(componentIndex, dimension);

                return $"{componentName} + addend.{componentName}";
            }

            private static string SubtractSubtrahendVectorDelegate(int componentIndex, int dimension)
            {
                string componentName = GetComponentName(componentIndex, dimension);

                return $"{componentName} - subtrahend.{componentName}";
            }

            private static string SubtractFromMinuendVectorDelegate(int componentIndex, int dimension)
            {
                string componentName = GetComponentName(componentIndex, dimension);

                return $"minuend.{componentName} - {componentName}";
            }

            private static string MultiplyFactorScalarDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)} * factor";
            private static string DivideDivisorScalarDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)} / divisor";
            private static string NegateDelegate(int componentIndex, int dimension) => $"-{GetComponentName(componentIndex, dimension)}";
            private static string NegateADelegate(int componentIndex, int dimension) => $"-a.{GetComponentName(componentIndex, dimension)}";

            private static string AddBVectorDelegate(int componentIndex, int dimension)
            {
                string componentName = GetComponentName(componentIndex, dimension);

                return $"a.{componentName} + b.{componentName}";
            }

            private static string SubtractBVectorDelegate(int componentIndex, int dimension)
            {
                string componentName = GetComponentName(componentIndex, dimension);

                return $"a.{componentName} - b.{componentName}";
            }

            private static string MultiplyAScalarDelegate(int componentIndex, int dimension) => $"a.{GetComponentName(componentIndex, dimension)} * b";
            private static string MultiplyBScalarDelegate(int componentIndex, int dimension) => $"a * b.{GetComponentName(componentIndex, dimension)}";
            private static string DivideAScalarDelegate(int componentIndex, int dimension) => $"a.{GetComponentName(componentIndex, dimension)} / b";

            private static string GetComponentName(int componentIndex, int dimension) => VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);
        }

        [SuppressMessage("Design", "CA1034", Justification = "Static")]
        public static class Lower
        {
            public static VectorTextBuilder Name { get; } = new(NameDelegate, ", ");
            public static VectorTextBuilder ComponentsAccess { get; } = new(ComponentsAccessDelegate, ", ");
            public static VectorTextBuilder Scalar { get; } = new(ScalarDelegate, ", ");
            public static VectorTextBuilder NewScalar { get; } = new(NewScalarDelegate, ", ");
            public static VectorTextBuilder Magnitude { get; } = new(MagnitudeDelegate, ", ");

            private static string NameDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)}";
            private static string ComponentsAccessDelegate(int componentIndex, int dimension) => $"components.{GetComponentName(componentIndex, dimension)}";
            private static string ScalarDelegate(int componentIndex, int dimension) => $"global::SharpMeasures.Scalar {GetComponentName(componentIndex, dimension)}";
            private static string NewScalarDelegate(int componentIndex, int dimension) => $"new global::SharpMeasures.Scalar({GetComponentName(componentIndex, dimension)})";
            private static string MagnitudeDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)}.Magnitude";

            private static string GetComponentName(int componentIndex, int dimension) => VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
        }

        private static string SampleValuesDelegate(int componentIndex, int _) => $"{2.1 * (componentIndex + 1) * Math.Sign(componentIndex)}";
        private static string DeconstructScalarHeaderDelegate(int componentIndex, int dimension) => $"out global::SharpMeasures.Scalar {VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension)}";
        private static string MultiplyScalarLHSDelegate(int componentIndex, int dimension) => $"a.{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension)} * b";
        private static string MultiplyScalarRHSDelegate(int componentIndex, int dimension) => $"a * b.{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension)}";
    }
}
