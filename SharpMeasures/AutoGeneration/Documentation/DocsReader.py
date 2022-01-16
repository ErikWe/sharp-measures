import re
from pathlib import Path

def readGenericTag(tag):
    return readTag(scalarGeneric, tag)

def readScalarTag(quantityName, tag):
    if quantityName in scalars:
        
        if not scalars[quantityName]:
            return readGenericTag(tag)

        content = readTag(scalars[quantityName], tag)
        return content if content[0] else readGenericTag(tag)
    else:
        scalars[quantityName] = readQuantity(quantityName)
        return readScalarTag(quantityName, tag)

def readTag(text, tag):
    regexResult = re.findall('(#Document:' + tag + '(#|\(([A-z0-9, _\-]*)\)#)\n)(.+?)(\n#\/Document:' + tag + '#)', text, flags=re.S)
    if len(regexResult) == 0:
        return False, False

    content = regexResult[0][3]
    parameters = regexResult[0][2].split(',') if regexResult[0][2] != '' else []
    
    for i, parameter in enumerate(parameters):
        parameters[i] = parameter.strip()

    return content, parameters

def readQuantity(quantityName):
    try:
        f = open(str(Path(__file__).parent) + '\\Scalars\\' + quantityName + '.txt', 'r')
        text = f.read()
        f.close()
        return text
    except FileNotFoundError:
        return False

scalars = {}
vectors = {}

scalarGeneric = readQuantity('Generic')