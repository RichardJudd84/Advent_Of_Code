import sys
import os  
from typing import *
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def parseData(data: List[str]):
    pathDict = {}
    for line in data:
        splitVals = line.split(':')
        key = splitVals[0]
        values = splitVals[1].strip().split()
        pathDict[key] = values
    return pathDict

def getPathsRecursive(pathDict: dict, key:str):
    if key == 'out':
        return 1
    else:
        nextPaths = pathDict[key]
        nPaths = 0
        for pathKey in nextPaths:
            nPaths += getPathsRecursive(pathDict, pathKey)
        return nPaths 
    
if(__name__ == '__main__'):
    d = data
    pathDict = parseData(d)

    nPaths = getPathsRecursive(pathDict, 'you')
    
    print(nPaths)