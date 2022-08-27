namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IConvertibleScalar> Conversions => conversions;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IConvertibleScalar> conversions { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IConvertibleScalar> conversions, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        this.conversions = conversions.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
