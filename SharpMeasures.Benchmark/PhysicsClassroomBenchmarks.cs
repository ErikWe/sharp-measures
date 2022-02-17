namespace ErikWe.SharpMeasures.Benchmarks;

using BenchmarkDotNet.Attributes;

using System;

[MemoryDiagnoser]
public class PhysicsClassroomBenchmarks
{
    private readonly Random Random = new();

    public IAccelerationComputer AccelerationComputer { get; set; } = new AccelerationComputer();

    // 4: https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions
    [Benchmark(Baseline = true)]
    public double UsingPrimitive()
    {
        double velocity1 = Random.NextDouble();
        double velocity2 = Random.NextDouble();
        double elapsedTime = Random.NextDouble();

        double acceleration = AccelerationComputer.Compute(velocity2 - velocity1, elapsedTime);

        double distance = velocity1 * elapsedTime + 0.5 * acceleration * elapsedTime * elapsedTime;

        return distance;
    }

    // 4: https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions
    [Benchmark]
    public Quantities.Distance UsingSharpMeasures()
    {
        Quantities.Speed velocity1 = Random.NextDouble() * Quantities.Speed.OneMetrePerSecond;
        Quantities.Speed velocity2 = Random.NextDouble() * Quantities.Speed.OneMetrePerSecond;
        Quantities.Time elapsedTime = Random.NextDouble() * Quantities.Time.OneSecond;

        Quantities.Acceleration acceleration = AccelerationComputer.Compute(velocity2 - velocity1, elapsedTime);

        Quantities.Distance distance = velocity1 * elapsedTime + 0.5 * acceleration * elapsedTime * elapsedTime;

        return distance;
    }

    // 4: https://www.physicsclassroom.com/class/1DKin/Lesson-6/Sample-Problems-and-Solutions
    [Benchmark]
    public QuantityTypes.Length UsingQuantityTypes()
    {
        QuantityTypes.Velocity velocity1 = Random.NextDouble() * QuantityTypes.Velocity.MetrePerSecond;
        QuantityTypes.Velocity velocity2 = Random.NextDouble() * QuantityTypes.Velocity.MetrePerSecond;
        QuantityTypes.Time elapsedTime = Random.NextDouble() * QuantityTypes.Time.Second;

        QuantityTypes.Acceleration acceleration = AccelerationComputer.Compute(velocity2 - velocity1, elapsedTime);

        QuantityTypes.Length distance = velocity1 * elapsedTime + 0.5 * acceleration * elapsedTime * elapsedTime;

        return distance;
    }
}

public interface IAccelerationComputer
{
    public double Compute(double velocity, double elapsedTime);
    public Quantities.Acceleration Compute(Quantities.Speed velocity, Quantities.Time elapsedTime);
    public QuantityTypes.Acceleration Compute(QuantityTypes.Velocity velocity, QuantityTypes.Time elapsedTime);
}

public class AccelerationComputer : IAccelerationComputer
{
    public double Compute(double velocity, double elapsedTime)
    {
        if (elapsedTime > 3 * 60 * 60)
        {
            return velocity / (elapsedTime + 93 * 60);
        }
        else
        {
            return velocity / elapsedTime;
        }
    }

    public Quantities.Acceleration Compute(Quantities.Speed velocity, Quantities.Time elapsedTime)
    {
        if (elapsedTime.Hours > 3)
        {
            return velocity / (elapsedTime + 93 * Quantities.Time.OneMinute);
        }
        else
        {
            return velocity / elapsedTime;
        }
    }

    public QuantityTypes.Acceleration Compute(QuantityTypes.Velocity velocity, QuantityTypes.Time elapsedTime)
    {
        if (elapsedTime.ConvertTo(QuantityTypes.Time.Hour) > 3)
        {
            return velocity / (elapsedTime + 93 * QuantityTypes.Time.Minute);
        }
        else
        {
            return velocity / elapsedTime;
        }
    }
}