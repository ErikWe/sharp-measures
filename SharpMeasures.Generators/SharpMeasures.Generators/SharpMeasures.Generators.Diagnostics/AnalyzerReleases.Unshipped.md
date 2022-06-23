; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
Measures1000 | Usage | Error | Expected a partial type
Measures1001 | Usage | Error | Expected a scalar quantity
Measures1002 | Usage | Error | Expected a vector quantity
Measures1003 | Usage | Error | Expected a unit
Measures1004 | Usage | Error | Type already defined
Measures1020 | Usage | Error | Expected a strictly unbiased scalar quantity
Measures1021 | Usage | Error | Expected a biased scalar quantity
Measures1022 | Usage | Error | Unit does not support biased quantities
Measures1080 | Usage | Info | Expected a non-empty list
Measures1081 | Usage | Info | Item has already been listed
Measures1100 | Naming | Warning | Invalid unit name
Measures1101 | Naming | Warning | Invalid plural form of unit name
Measures1102 | Naming | Warning | Duplicate unit name
Measures1103 | Naming | Warning | Duplicate unit plural form
Measures1104 | Usage | Warning | Expected the name of a unit
Measures1105 | Usage | Warning | Cyclic unit dependency
Measures1120 | Usage | Warning | Prefix not recognized
Measures1121 | Usage | Warning | Invalid scaled unit expression
Measures1122 | Usage | Warning | Invalid biased unit expression
Measures1123 | Usage | Warning | The unit does not support biases
Measures1124 | Usage | Warning | The unit does not support biases
Measures1140 | Usage | Warning | Invalid derivation ID
Measures1141 | Naming | Warning | Duplicate derivation ID
Measures1142 | Usage | Warning | Derivation ID not recognized
Measures1143 | Usage | Warning | Invalid unit derivation expression
Measures1144 | Usage | Warning | Expected a non-empty signature
Measures1145 | Usage | Warning | Unit list not matching signature
Measures1146 | Usage | Warning | Unit not derivable
Measures1147 | Usage | Warning | Ambiguous unit derivation signature
Measures1200 | Usage | Info | Expected both unit name and symbol
Measures1220 | Naming | Warning | Invalid name of constant
Measures1221 | Naming | Warning | Invalid name for multiples of constant
Measures1222 | Naming | Warning | Duplicate name of constant
Measures1223 | Naming | Warning | Duplicate name for multiples of constant
Measures1224 | Naming | Warning | Constant shares name with unit
Measures1225 | Usage | Info | Constant multiples is disabled
Measures1240 | Usage | Warning | Unrecognized cast operator behaviour
Measures1260 | Usage | Warning | Contradictory attributes
Measures1261 | Usage | Warning | Quantity group missing root quantity
Measures1262 | Usage | Info | Inclusion or exclusion had no effect
Measures1263 | Usage | Info | Difference is disabled but a quantity was specified
Measures1420 | Usage | Warning | Invalid vector dimension
Measures1421 | Usage | Warning | Missing vector dimension
Measures1422 | Usage | Warning | Duplicate vector dimension
Measures1440 | Usage | Warning | Invalid dimension of vector constant
Measures1460 | Usage | Info | Vector group already specified
Measures1900 | Documentation | Warning | Unresolved documentation dependency
Measures1901 | Documentation | Warning | Documentation tag not found