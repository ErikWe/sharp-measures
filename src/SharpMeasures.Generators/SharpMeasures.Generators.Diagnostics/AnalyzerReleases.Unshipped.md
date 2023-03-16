; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
Measures1000 | Usage | Error | Expected a partial type
Measures1001 | Usage | Error | Expected a non-static type
Measures1002 | Usage | Error | Expected a static type
Measures1003 | Usage | Error | Expected a scalar quantity
Measures1004 | Usage | Error | Expected a vector quantity
Measures1005 | Usage | Error | Expected a vector group
Measures1006 | Usage | Error | Expected a vector group member
Measures1007 | Usage | Error | Expected a quantity
Measures1008 | Usage | Error | Expected a unit
Measures1009 | Usage | Error | Type already defined
Measures1020 | Usage | Error | Expected a strictly unbiased scalar quantity
Measures1021 | Usage | Error | Expected a biased scalar quantity
Measures1022 | Usage | Error | Unit does not support biased quantities
Measures1040 | Usage | Error | Invalid derivation expression
Measures1041 | Usage | Error | Invalid derivation signature
Measures1060 | Usage | Warning | No regex pattern specified
Measures1080 | Usage | Info | Expected a non-empty list
Measures1081 | Usage | Warning | Item has already been listed
Measures1100 | Naming | Error | Invalid unit name
Measures1101 | Naming | Error | Invalid plural form of unit name
Measures1102 | Naming | Error | Duplicate unit name
Measures1103 | Naming | Error | Duplicate unit plural form
Measures1104 | Usage | Error | Expected the name of a unit
Measures1105 | Usage | Error | Cyclic unit dependency
Measures1106 | Usage | Warning | Derivable unit should not use FixedUnit
Measures1120 | Usage | Error | Invalid scaled unit expression
Measures1121 | Usage | Error | Invalid biased unit expression
Measures1122 | Usage | Error | Unit does not support biased instances
Measures1140 | Usage | Error | Unnamed derivation signature
Measures1141 | Usage | Error | Ambiguous unit derivation signature
Measures1142 | Usage | Error | Duplicate derivation ID
Measures1143 | Usage | Error | Duplicate derivation signature
Measures1144 | Usage | Error | Derivation ID not recognized
Measures1145 | Usage | Error | Unit list not matching signature
Measures1146 | Usage | Error | Unit not derivable
Measures1147 | Usage | Error | Unit with bias term cannot be derived
Measures1148 | Usage | Error | Derivation signature is not permutable
Measures1149 | Usage | Error | Unmatched derivation expression unit
Measures1150 | Usage | Error | Unit is not included in expression
Measures1200 | Usage | Info | Expected both unit name and symbol
Measures1201 | Usage | Error | Quantity group missing root quantity
Measures1202 | Usage | Info | Difference is disabled but a quantity was specified
Measures1220 | Naming | Error | Invalid name of constant
Measures1221 | Naming | Error | Invalid name for multiples of constant
Measures1222 | Naming | Error | Duplicate name of constant
Measures1223 | Naming | Error | Constant shares name with unit
Measures1224 | Usage | Info | Constant multiples is disabled
Measures1225 | Usage | Error | Invalid expression for constant
Measures1226 | Usage | Error | Quantity convertible to itself
Measures1240 | Usage | Warning | Contradictory attributes
Measures1241 | Usage | Warning | Inclusion or exclusion had no effect
Measures1242 | Usage | Info | Union inclusion stacking mode is redundant
Measures1260 | Naming | Error | Invalid name of quantity operation
Measures1261 | Naming | Info | Quantity operation name redundant
Measures1262 | Usage | Error | Duplicate quantity operation
Measures1263 | Usage | Error | Invalid quantity operation
Measures1264 | Usage | Warning | Quantity operation cannot be mirrored
Measures1265 | Usage | Warning | Quantity operation method cannot be mirrored
Measures1266 | Usage | Info | Quantity operation not mirrored
Measures1280 | Naming | Error | Invalid name of process
Measures1281 | Naming | Error | Duplicate name of process
Measures1282 | Usage | Error | Invalid processing expression
Measures1283 | Usage | Warning | Process with parameter cannot be a property
Measures1284 | Usage | Error | Process has unmatched parameter definitions
Measures1285 | Usage | Error | Invalid process parameter type
Measures1286 | Naming | Error | Invalid name of process parameter
Measures1287 | Naming | Error | Duplicate process parameter name
Measures1420 | Usage | Error | Invalid vector dimension
Measures1421 | Usage | Error | Missing vector dimension
Measures1422 | Usage | Error | Vector of unexpected dimension
Measures1423 | Usage | Error | Vector group already contains dimension
Measures1424 | Usage | Error | No vector of appropiate dimension in group
Measures1425 | Naming | Warning | Vector name and dimension conflict
Measures1426 | Naming | Warning | Vector group name suggests dimension
Measures1427 | Usage | Error | Non-overlapping vector dimensions
Measures1440 | Usage | Error | Invalid dimension of vector constant
Measures1460 | Usage | Error | Vector does not support cross product
Measures1800 | Usage | Error | Unrecognized enum value
Measures1900 | Documentation | Error | Unresolved documentation dependency
Measures1901 | Documentation | Error | Duplicate documentation tag
Measures1902 | Documentation | Error | Documentation tag not found