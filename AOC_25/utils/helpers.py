import os

def getLineStrings(path: str) -> list[str]:
    """Reads a file and returns a list of strings, each representing a line in the file."""
    with open(path, 'r') as file:
        return [line.strip() for line in file.readlines()]
    
def getLineStringsWithWhiteSpace(path: str) -> list[str]:
    """Reads a file and returns a list of strings, each representing a line in the file."""
    with open(path, 'r') as file:
        return [line.replace("\n", "") for line in file.readlines()]

def getBasePath():
    basePath = os.path.dirname(os.path.abspath(__file__))
    print("Script path:", basePath)

def parseCSV(lines: list[str]):
    values = []
    for line in lines:
        newLine = line.strip(',').split(',')

        values.extend(newLine)
    return values

def parseListToInt(values: list[str]):
    return [int(value) for value in parseCSV(values)]

def expandRange(rangeString: str, delimiter: str = '-'):
    start, end = rangeString.split(delimiter)
    return list(range(int(start), int(end)+1))