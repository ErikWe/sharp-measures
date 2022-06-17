﻿namespace SharpMeasures.Generators.Units.Pipelines.Common;

using SharpMeasures.Generators.Units.Documentation;

internal readonly record struct DataModel(DefinedType Unit, DefinedType Quantity, bool BiasTerm, IDocumentationStrategy Documentation,
    string QuantityParameterName);