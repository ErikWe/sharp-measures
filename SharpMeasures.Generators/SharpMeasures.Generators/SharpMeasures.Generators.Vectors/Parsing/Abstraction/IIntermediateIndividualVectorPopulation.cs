﻿namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IIntermediateIndividualVectorPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IIndividualVectorType> VectorBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IIntermediateIndividualVectorSpecializationType> VectorSpecializations { get; }
}