namespace ErikWe.SharpMeasures.SourceGenerators.Tests;

using ErikWe.SharpMeasures.SourceGenerators.ScalarQuantities;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ScalarQuantityGeneratorTests
{
    [Fact]
    public Task ScalarQuantityGenerator_ShouldBeExactCode()
    {
        string source = @"
using ErikWe.SharpMeasures.Attributes;

namespace Foo
{
    namespace Bar
    {
        [ScalarQuantity]
        public readonly partial record struct Time() { }
    }
}";

        return Utility.VerifyGenerator<ScalarQuantityGenerator>(source);
    }
}