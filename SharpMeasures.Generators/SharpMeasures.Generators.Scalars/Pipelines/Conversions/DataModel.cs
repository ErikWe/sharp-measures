namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IConvertibleQuantity> conversions, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Conversions = conversions.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
