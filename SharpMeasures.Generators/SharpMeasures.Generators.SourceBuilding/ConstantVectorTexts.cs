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
        public static string AddAddendVector(int dimension, bool lhsComponent, bool rhsComponent) => Builders.Upper.AddAddendVector(lhsComponent, rhsComponent).GetText(dimension);
        public static string SubtractSubtrahendVector(int dimension, bool lhsComponent, bool rhsComponent) => Builders.Upper.SubtractSubtrahendVector(lhsComponent, rhsComponent).GetText(dimension);
        public static string SubtractFromMinuendVector(int dimension, bool lhsComponent, bool rhsComponent) => Builders.Upper.SubtractFromMinuendVector(lhsComponent, rhsComponent).GetText(dimension);
        public static string MultiplyFactorScalar(int dimension, bool component, bool magnitude) => Builders.Upper.MultiplyFactorScalar(component, magnitude).GetText(dimension);
        public static string DivideDivisorScalar(int dimension, bool component, bool magnitude) => Builders.Upper.DivideDivisorScalar(component, magnitude).GetText(dimension);
        public static string Negate(int dimension) => Builders.Upper.Negate.GetText(dimension);
        public static string NegateA(int dimension) => Builders.Upper.NegateA.GetText(dimension);
        public static string AddBVector(int dimension, bool lhsComponent, bool rhsComponent) => Builders.Upper.AddBVector(lhsComponent, rhsComponent).GetText(dimension);
        public static string SubtractBVector(int dimension, bool lhsComponent, bool rhsComponent) => Builders.Upper.SubtractBVector(lhsComponent, rhsComponent).GetText(dimension);
        public static string MultiplyAByScalarB(int dimension, bool component, bool magnitude) => Builders.Upper.MultiplyAByScalarB(component, magnitude).GetText(dimension);
        public static string MultiplyScalarAByB(int dimension, bool component, bool magnitude) => Builders.Upper.MultiplyScalarAByB(component, magnitude).GetText(dimension);
        public static string DivideAByScalarB(int dimension, bool component, bool magnitude) => Builders.Upper.DivideAByScalarB(component, magnitude).GetText(dimension);
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
            public static VectorTextBuilder AddAddendVector(bool lhsComponent, bool rhsComponent) => new(AddAddendVectorDelegate(lhsComponent, rhsComponent), ", ");
            public static VectorTextBuilder SubtractSubtrahendVector(bool lhsComponent, bool rhsComponent) => new(SubtractSubtrahendVectorDelegate(lhsComponent, rhsComponent), ", ");
            public static VectorTextBuilder SubtractFromMinuendVector(bool lhsComponent, bool rhsComponent) => new(SubtractFromMinuendVectorDelegate(lhsComponent, rhsComponent), ", ");
            public static VectorTextBuilder MultiplyFactorScalar(bool component, bool magnitude) => new(MultiplyFactorScalarDelegate(component, magnitude), ", ");
            public static VectorTextBuilder DivideDivisorScalar(bool component, bool magnitude) => new(DivideDivisorScalarDelegate(component, magnitude), ", ");
            public static VectorTextBuilder Negate { get; } = new(NegateDelegate, ", ");
            public static VectorTextBuilder NegateA { get; } = new(NegateADelegate, ", ");
            public static VectorTextBuilder AddBVector(bool lhsComponent, bool rhsComponent) => new(AddBVectorDelegate(lhsComponent, rhsComponent), ", ");
            public static VectorTextBuilder SubtractBVector(bool lhsComponent, bool rhsComponent) => new(SubtractBVectorDelegate(lhsComponent, rhsComponent), ", ");
            public static VectorTextBuilder MultiplyAByScalarB(bool component, bool magnitude) => new(MultiplyAByScalarBDelegate(component, magnitude), ", ");
            public static VectorTextBuilder MultiplyScalarAByB(bool component, bool magnitude) => new(MultiplyScalarAByBDelegate(component, magnitude), ", ");
            public static VectorTextBuilder DivideAByScalarB(bool component, bool magnitude) => new(DivideAByScalarBDelegate(component, magnitude), ", ");

            private static string NameDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)}";
            private static string ComponentsAccessDelegate(int componentIndex, int dimension) => $"components.{GetComponentName(componentIndex, dimension)}";
            private static string ComponentsPropertyAccessDelegate(int componentIndex, int dimension) => $"Components.{GetComponentName(componentIndex, dimension)}";
            private static string ScalarDelegate(int componentIndex, int dimension) => $"global::SharpMeasures.Scalar {GetComponentName(componentIndex, dimension)}";
            private static string MagnitudeDelegate(int componentIndex, int dimension) => $"{GetComponentName(componentIndex, dimension)}.Magnitude";

            private static Func<int, int, string> AddAddendVectorDelegate(bool lhsComponent, bool rhsComponent)
            {
                return addAddendVector;

                string addAddendVector(int componentIndex, int dimension)
                {
                    string componentName = GetComponentName(componentIndex, dimension);

                    return $"{componentName}{(lhsComponent ? ".Magnitude" : string.Empty)} + addend.{componentName}{(rhsComponent ? ".Magnitude" : string.Empty)}";
                }
            }

            private static Func<int, int, string> SubtractSubtrahendVectorDelegate(bool lhsComponent, bool rhsComponent)
            {
                return subtractSubtrahendVector;

                string subtractSubtrahendVector(int componentIndex, int dimension)
                {
                    string componentName = GetComponentName(componentIndex, dimension);

                    return $"{componentName}{(lhsComponent ? ".Magnitude" : string.Empty)} - subtrahend.{componentName}{(rhsComponent ? ".Magnitude" : string.Empty)}";
                }
            }

            private static Func<int, int, string> SubtractFromMinuendVectorDelegate(bool lhsComponent, bool rhsComponent)
            {
                return subtractFromMinuendVector;

                string subtractFromMinuendVector(int componentIndex, int dimension)
                {
                    string componentName = GetComponentName(componentIndex, dimension);

                    return $"minuend.{componentName}{(lhsComponent ? ".Magnitude" : string.Empty)} - {componentName}{(rhsComponent ? ".Magnitude" : string.Empty)}";
                }
            }

            private static Func<int, int, string> MultiplyFactorScalarDelegate(bool component, bool magnitude) => (componentIndex, dimension) => $"{GetComponentName(componentIndex, dimension)}{(component ? ".Magnitude" : string.Empty)} * factor{(magnitude ? ".Magnitude" : string.Empty)}";
            private static Func<int, int, string> DivideDivisorScalarDelegate(bool component, bool magnitude) => (componentIndex, dimension) => $"{GetComponentName(componentIndex, dimension)}{(component ? ".Magnitude" : string.Empty)} / divisor{(magnitude ? ".Magnitude" : string.Empty)}";
            private static string NegateDelegate(int componentIndex, int dimension) => $"-{GetComponentName(componentIndex, dimension)}";
            private static string NegateADelegate(int componentIndex, int dimension) => $"-a.{GetComponentName(componentIndex, dimension)}";

            private static Func<int, int, string> AddBVectorDelegate(bool lhsComponent, bool rhsComponent)
            {
                return addBVector;

                string addBVector(int componentIndex, int dimension)
                {
                    string componentName = GetComponentName(componentIndex, dimension);

                    return $"a.{componentName}{(lhsComponent ? ".Magnitude" : string.Empty)} + b.{componentName}{(rhsComponent ? ".Magnitude" : string.Empty)}";
                }
            }

            private static Func<int, int, string> SubtractBVectorDelegate(bool lhsComponent, bool rhsComponent)
            {
                return subtractBVectorDelegate;

                string subtractBVectorDelegate(int componentIndex, int dimension)
                {
                    string componentName = GetComponentName(componentIndex, dimension);

                    return $"a.{componentName}{(lhsComponent ? ".Magnitude" : string.Empty)} - b.{componentName}{(rhsComponent ? ".Magnitude" : string.Empty)}";
                }
            }

            private static Func<int, int, string> MultiplyAByScalarBDelegate(bool component, bool magnitude) => (componentIndex, dimension) => $"a.{GetComponentName(componentIndex, dimension)}{(component ? ".Magnitude" : string.Empty)} * b{(magnitude ? ".Magnitude" : string.Empty)}";
            private static Func<int, int, string> MultiplyScalarAByBDelegate(bool component, bool magnitude) => (componentIndex, dimension) => $"a{(magnitude ? ".Magnitude" : string.Empty)} * b.{GetComponentName(componentIndex, dimension)}{(component ? ".Magnitude" : string.Empty)}";
            private static Func<int, int, string> DivideAByScalarBDelegate(bool component, bool magnitude) => (componentIndex, dimension) => $"a.{GetComponentName(componentIndex, dimension)}{(component ? ".Magnitude" : string.Empty)} / b{(magnitude ? ".Magnitude" : string.Empty)}";

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

        private static string SampleValuesDelegate(int componentIndex, int _) => $"{2.1 * (componentIndex + 1) * (componentIndex % 2 is 1 ? 1 : -1)}";
        private static string DeconstructScalarHeaderDelegate(int componentIndex, int dimension) => $"out global::SharpMeasures.Scalar {VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension)}";
        private static string MultiplyScalarLHSDelegate(int componentIndex, int dimension) => $"a.{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension)} * b";
        private static string MultiplyScalarRHSDelegate(int componentIndex, int dimension) => $"a * b.{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension)}";
    }
}
