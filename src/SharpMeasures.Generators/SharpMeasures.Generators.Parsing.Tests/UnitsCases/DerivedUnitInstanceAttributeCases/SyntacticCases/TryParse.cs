namespace SharpMeasures.Generators.Parsing.Tests.UnitsCases.DerivedUnitInstanceAttributeCases.SyntacticCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Parsing.Units;

using System;
using System.Threading.Tasks;

using Xunit;

public class TryParse
{
    private static IRawDerivedUnitInstance? Target(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser, AttributeData attributeData, AttributeSyntax attributeSyntax) => parser.TryParse(attributeData, attributeSyntax);

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeData_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", "Metres", "ID", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, _, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");
        var attributeData = Datasets.GetNullAttributeData();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullAttributeSyntax_ArgumentNullException(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", "Metres", "ID", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, _) = await CompilationStore.GetComponents(source, "Foo");
        var attributeSyntax = Datasets.GetNullAttributeSyntax();

        var exception = Record.Exception(() => Target(parser, attributeData, attributeSyntax));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullValues_Match(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance(null, null, null, null)]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedDerivationIDLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var (expectedUnitsCollectionLocation, expectedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Null(actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Null(actual.Units);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedDerivationIDLocation, actual.Syntax.DerivationID);
        Assert.Equal(expectedUnitsCollectionLocation, actual.Syntax.UnitsCollection);
        Assert.Equal(expectedUnitsElementLocations, actual.Syntax.UnitsElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", "Metres", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (expectedUnitsCollectionLocation, expectedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Equal("Metres", actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(new[] { "42", "314" }, actual.Units);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(Location.None, actual.Syntax.DerivationID);
        Assert.Equal(expectedUnitsCollectionLocation, actual.Syntax.UnitsCollection);
        Assert.Equal(expectedUnitsElementLocations, actual.Syntax.UnitsElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithoutPluralForm_Match(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (expectedUnitsCollectionLocation, expectedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(new[] { "42", "314" }, actual.Units);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(Location.None, actual.Syntax.DerivationID);
        Assert.Equal(expectedUnitsCollectionLocation, actual.Syntax.UnitsCollection);
        Assert.Equal(expectedUnitsElementLocations, actual.Syntax.UnitsElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task WithDerivationID_Match(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", null, "ID", new[] { "42", "314" })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expectedPluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var expectedDerivationIDLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var (expectedUnitsCollectionLocation, expectedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Equal("ID", actual.DerivationID);
        Assert.Equal(new[] { "42", "314" }, actual.Units);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(expectedPluralFormLocation, actual.Syntax.PluralForm);
        Assert.Equal(expectedDerivationIDLocation, actual.Syntax.DerivationID);
        Assert.Equal(expectedUnitsCollectionLocation, actual.Syntax.UnitsCollection);
        Assert.Equal(expectedUnitsElementLocations, actual.Syntax.UnitsElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task EmptyUnits_Match(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", new string[0])]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (expectedUnitsCollectionLocation, expectedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(Array.Empty<string>(), actual.Units);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(Location.None, actual.Syntax.DerivationID);
        Assert.Equal(expectedUnitsCollectionLocation, actual.Syntax.UnitsCollection);
        Assert.Equal(expectedUnitsElementLocations, actual.Syntax.UnitsElements);
    }

    [Theory]
    [ClassData(typeof(Datasets.ParserSources))]
    public async Task NullContainingUnits_Match(IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance> parser)
    {
        var source = """
            [SharpMeasures.DerivedUnitInstance("Metre", new[] { "42", null })]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var expectedAttributeNameLocation = attributeSyntax.Name.GetLocation();
        var expectedNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (expectedUnitsCollectionLocation, expectedUnitsElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        var actual = Target(parser, attributeData, attributeSyntax);

        Assert.Equal("Metre", actual!.Name);
        Assert.Null(actual.PluralForm);
        Assert.Null(actual.DerivationID);
        Assert.Equal(new[] { "42", null }, actual.Units);

        Assert.Equal(expectedAttributeNameLocation, actual.Syntax!.AttributeName);
        Assert.Equal(expectedNameLocation, actual.Syntax.Name);
        Assert.Equal(Location.None, actual.Syntax.PluralForm);
        Assert.Equal(Location.None, actual.Syntax.DerivationID);
        Assert.Equal(expectedUnitsCollectionLocation, actual.Syntax.UnitsCollection);
        Assert.Equal(expectedUnitsElementLocations, actual.Syntax.UnitsElements);
    }
}
