namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public record class ResolvedMethodDerivation
{
    public IReadOnlyList<IDerivedQuantity> ScalarResults => scalarResults;

    public IReadOnlyDictionary<int, IReadOnlyList<IDerivedQuantity>> VectorResults => vectorResults;

    private ReadOnlyEquatableList<IDerivedQuantity> scalarResults { get; }

    private ReadOnlyEquatableDictionary<int, IReadOnlyList<IDerivedQuantity>> vectorResults { get; }

    public ResolvedMethodDerivation(IReadOnlyList<IDerivedQuantity> scalarResults, IReadOnlyDictionary<int, IReadOnlyList<IDerivedQuantity>> vectorResults)
    {
        Dictionary<int, ReadOnlyEquatableList<IDerivedQuantity>> equatableVectorResults = new(vectorResults.Count);

        foreach (var keyValue in vectorResults)
        {
            equatableVectorResults.Add(keyValue.Key, keyValue.Value.AsReadOnlyEquatable());
        }

        this.scalarResults = scalarResults.AsReadOnlyEquatable();
        this.vectorResults = equatableVectorResults.Transform(static (list) => list as IReadOnlyList<IDerivedQuantity>).AsReadOnlyEquatable();
    }
}
