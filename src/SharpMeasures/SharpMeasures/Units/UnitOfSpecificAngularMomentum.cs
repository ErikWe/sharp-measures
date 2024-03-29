﻿namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("SquareMetrePerSecond", "s[Per]", new[] { "KilogramSquareMetrePerSecond", "Kilogram" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAngularMomentum), typeof(UnitOfMass))]
[Unit(typeof(SpecificAngularMomentum))]
public readonly partial record struct UnitOfSpecificAngularMomentum { }
