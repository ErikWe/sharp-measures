namespace SharpMeasures.Generators.Quantities.Utility;

/// <summary>Defines some common notations for producing the name of a property describing a quantity in multiples of a constant, based on the name
/// of the constant.</summary>
public static class CommonConstantMultiplesPropertyNotations
{
    /// <summary>Prepends "In" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples of a constant "StandardGravity" would be "InStandardGravity".</remarks>
    public const string PrependIn = "In[*]";

    /// <summary>Prepends "In" and appends "Multiples" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples of a constant "StandardGravity" would be "InStandardGravityMultiples".</remarks>
    public const string PrependInAndAppendMultiples = "In[*]Multiples";

    /// <summary>Prepends "InMultiplesOf" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples of a constant "StandardGravity" would be "InMultiplesOfStandardGravity".</remarks>
    public const string PrependInMultiplesOf = "InMultiplesOf[*]";

    /// <summary>Prepends "In" and appends the lower-cased "s" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples a constant "PlanckLength" would be "InPlanckLengths".</remarks>
    public const string PrependInAndAppendS = "In[*]s";

    /// <summary>Prepends "In" and appends the lower-cased "es" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples of a constant "PlanckMass" would be "InPlanckMasses".</remarks>
    public const string PrependInAndAppendEs = "In[*]es";

    /// <summary>Appends "Multiples" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples of a constant "StandardGravity" would be "StandardGravityMultiples".</remarks>
    public const string AppendMultiples = "[*]Multiples";

    /// <summary>Prepends "MultiplesOf" to the name of the constant.</summary>
    /// <remarks>For example; the property describing multiples of a constant "StandardGravity" would be "MultiplesOfStandardGravity".</remarks>
    public const string PrependMultiplesOf = "MultiplesOf[*]";
}
