namespace SharpMeasures.Generators.Vectors.Processing;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal readonly record struct ProcessedGeneratedVector
{
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public string? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public ProcessedGeneratedVector(UnitInterface unit, ScalarInterface? scalar, string? defaultUnit, string? defaultUnitSymbol)
    {
        Unit = unit;
        Scalar = scalar;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;
    }
}
