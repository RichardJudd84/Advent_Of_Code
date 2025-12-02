import os

def getLineStrings(path: str) -> list[str]:
    """Reads a file and returns a list of strings, each representing a line in the file."""
    with open(path, 'r') as file:
        return [line.strip() for line in file.readlines()]

def getBasePath():
    basePath = os.path.dirname(os.path.abspath(__file__))
    print("Script path:", basePath)

def parseCSV(lines: list[str]):
    idRanges = []
    for line in lines:
        newLine = line.strip(',').split(',')

        idRanges.extend(newLine)
    return idRanges

def expandRange(rangeString: str, delimiter: str = '-'):
    start, end = rangeString.split(delimiter)
    return list(range(int(start), int(end)+1))