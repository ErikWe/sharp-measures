namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Utility;

public interface IUnitInclusionList : IUnitList
{
    public abstract InclusionStackingMode StackingMode { get; }
}
