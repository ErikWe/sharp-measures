namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void ExpressionNull()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public class UnitOfTime { }

[DerivableUnit(null, typeof(UnitOfLength), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.LengthTimeSpeed_NoDerivable, source);
    }

    [Fact]
    public void TypeNotUnit()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

public class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", typeof(UnitOfLength), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.LengthTimeSpeed_NoDerivable, source);
    }

    [Fact]
    public void NoUnitArguments()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[DerivableUnit(""{{0}} / {{1}}"")]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.LengthTimeSpeed_NoDerivable, source);
    }

    [Fact]
    public void MoreUnitsThanExpressionExpected()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public class UnitOfTime { }

[DerivableUnit(""{{0}}"", typeof(UnitOfLength), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.LengthTimeSpeed_NoDerivable, source);
    }

    [Fact]
    public void FewerUnitsThanExpressionExpected()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }

[DerivableUnit(""{{0}} / {{1}}"", typeof(UnitOfLength))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.LengthTimeSpeed_NoDerivable, source);
    }

    [Fact]
    public void NullUnit()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", null, typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.LengthTimeSpeed_NoDerivable, source);
    }
}