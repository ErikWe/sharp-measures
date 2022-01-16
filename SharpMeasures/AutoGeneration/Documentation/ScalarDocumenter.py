import sys
import re
from pathlib import Path
from . import Utility
from . import DocsReader

def ModifyText(text, quantityName, quantityData):
    unit = Utility.getUnitName(quantityName, quantityData)
    bases = Utility.getBases(quantityName, quantityData)
    inverse = Utility.getInverseQuantity(quantityName, quantityData)
    square = Utility.getSquareQuantity(quantityName, quantityData)
    cube = Utility.getCubeQuantity(quantityName, quantityData)
    squareRoot = Utility.getSquareRootQuantity(quantityName, quantityData)
    cubeRoot = Utility.getCubeRootQuantity(quantityName, quantityData)
    vector = Utility.getVectorName(quantityName, quantityData)

    text = text.replace('#Quantity#', quantityName)
    text = text.replace('#quantity#', Utility.lowerCase(quantityName))
    text = text.replace('#Unit#', unit)
    text = text.replace('#unit#', Utility.lowerCase(unit))

    if vector:
        text = text.replace('#VectorQuantity#', vector)
        text = text.replace('#vectorQuantity#', Utility.lowerCase(vector))

    if inverse:
        text = text.replace('#InverseQuantity#', inverse)
        text = text.replace('#inverseQuantity#', Utility.lowerCase(inverse))

    if square:
        text = text.replace('#SquareQuantity#', square)
        text = text.replace('#squareQuantity#', Utility.lowerCase(square))

    if cube:
        text = text.replace('#CubeQuantity#', cube)
        text = text.replace('#cubeQuantity#', Utility.lowerCase(cube))

    if squareRoot:
        text = text.replace('#SquareRootQuantity#', squareRoot)
        text = text.replace('#squareRootQuantity#', Utility.lowerCase(squareRoot))

    if cubeRoot:
        text = text.replace('#CubeRootQuantity#', cubeRoot)
        text = text.replace('#CubeRootQuantity#', Utility.lowerCase(cubeRoot))

    for i in range(10):
        text = text.replace('#Base' + str(i) + '#', bases['singular'][i % len(bases['singular'])])
        text = text.replace('#Base' + str(i) + 's#', bases['plural'][i % len(bases['plural'])])

    return text

def ProduceDocumentationLines(line, quantityName, quantityData):
    regexResult = re.findall('#Document:([A-z0-9_\-]*)(#|\(([A-z0-9, _\-=]*)\)#)', line)

    tag = regexResult[0][0]
    arguments = regexResult[0][2].split(',') if regexResult[0][2] != '' else []
    parameterValues = {}

    for argument in arguments:
        parameter = argument.split('=')[0].strip()
        value = argument.split('=')[1].strip()

        parameterValues[parameter] = value

    text, parameters = DocsReader.readScalarTag(quantityName, tag)

    if not text:
        print('Could not resolve documentation tag: [' + tag + '] for quantity: [' + quantityName + '].')
        return []

    for parameter in parameters:
        if parameter in parameterValues:
            text = text.replace('#Param:' + parameter + '#', parameterValues[parameter])
        else:
            print('Parameter was not specified: [' + parameter + '], from tag: [' + tag + '], in quantity: [' + quantityName + '].')
            text = text.replace('#Param:' + parameter + '#', 'ParameterNotSpecified')

    for parameter, value in parameterValues.items():
        if not parameter in parameters:
            print('Parameter was specified, but not part of signature: [' + parameter + '], with value: [' + value + '], from tag: [' + tag + '], in quantity: [' + quantityName + '].')

    if '#Param:' in text:
        regexResult = re.findall('(#Param:)([A-z0-9_\-]*)(#)', text)
        for result in regexResult:
            print('Parameter was requested, but not part of signature: [' + result[1] + '], from tag: [' + tag + '], in quantity: [' + quantityName + '].')

    return ModifyText(text, quantityName, quantityData).split('\n')

def CorrectLine(line):
    if line[:4] == '/// ':
        return line

    if line[:3] == '///':
        return '/// ' + line[3:]

    if line[:2] == '//':
        if line[2] == ' ':
            return '/// ' + line[3:]
        else:
            return '/// ' + line[2:]

    if line[0] == '/':
        if line[1] == ' ':
            return '/// ' + line[2:]
        else:
            return '/// ' + line[1:]

    if line[0] == ' ':
        return '/// ' + line[1:]
    else:
        return '/// ' + line

def documentationPass(text, quantityName, quantityData):
    rebuilt = ""
    modified = False

    for line in text.split('\n'):
        if '#' in line:
            indent = re.findall('[ ]*', line)[0]

        if '#Document:' in line:
            modified = True
            for documentLine in ProduceDocumentationLines(line, quantityName, quantityData):
                rebuilt += indent + CorrectLine(documentLine) + '\n'
        else:
            rebuilt += line + '\n'

    return rebuilt, modified

def document(text, quantityName, quantityData):
    while True:
        updatedText, modified = documentationPass(text, quantityName, quantityData)
        if modified:
            text = updatedText
        else:
            return text