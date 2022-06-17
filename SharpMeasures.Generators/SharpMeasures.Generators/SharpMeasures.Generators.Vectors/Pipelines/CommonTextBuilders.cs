namespace SharpMeasures.Generators.Vectors.Pipelines;

using SharpMeasures.Generators.SourceBuilding;

internal static class CommonTextBuilders
{
    public static VectorTextBuilder DeconstructComponents(string componentName)
    {
        return new VectorTextBuilder(deconstructComponentsComponent, ", ");

        string deconstructComponentsComponent(int componentIndex, int dimension)
        {
            return $"out {componentName} {VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension)}";
        }
    }

    public static class Upper
    {
        public static VectorTextBuilder Component(string componentType)
        {
            return new VectorTextBuilder(component, ", ");

            string component(int componentIndex, int dimension)
            {
                return $"{componentType} {GetComponentName(componentIndex, dimension)}";
            }
        }

        private static string GetComponentName(int componentIndex, int dimension) => VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);
    }

    public static class Lower
    {
        public static VectorTextBuilder Component(string componentType)
        {
            return new VectorTextBuilder(component, ", ");

            string component(int componentIndex, int dimension)
            {
                return $"{componentType} {GetComponentName(componentIndex, dimension)}";
            }
        }

        public static VectorTextBuilder NewComponent(string componentType)
        {
            return new VectorTextBuilder(newComponent, ", ");

            string newComponent(int componentIndex, int dimension)
            {
                return $"new {componentType}({GetComponentName(componentIndex, dimension)})";
            }
        }

        public static VectorTextBuilder ScalarMultiplyUnit(string unitParameterName, string unitQuantityName)
        {
            return new VectorTextBuilder(scalarMultiplyUnit, ", ");

            string scalarMultiplyUnit(int componentIndex, int dimension)
            {
                return $"{GetComponentName(componentIndex, dimension)} * {unitParameterName}.{unitQuantityName}.Magnitude";
            }
        }

        private static string GetComponentName(int componentIndex, int dimension) => VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
    }
}
