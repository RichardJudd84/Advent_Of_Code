import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def initStartDict(line: str):
    startIndex = line.find('S')
    return {startIndex:1} 

def processLine(beamIndices: Dict[int,int], line: str,):

    newDict: Dict[int,int] = {}
    for i , nPaths in beamIndices.items():

        if(line[i] == '^'):
            if(i == 0):
                addPath(newDict, i+1, nPaths)
            elif(i == len(line)-1):
                addPath(newDict, i-1, nPaths)
            else:
                addPath(newDict, i+1, nPaths)
                addPath(newDict, i-1, nPaths)
        else:
            addPath(newDict, i, nPaths)
    return newDict
        

def addPath(d : Dict[int,int], key, nPaths):
    if(key in d):
        d[key] += nPaths
    else:
        d[key] = nPaths

def getNPaths(d : Dict[int,int]):
    return sum(d.values())


if(__name__ == '__main__'):
    d = data
    beamIndices = initStartDict(d[0])
    nPaths = 1
    for line in d[1:] :
        beamIndices = processLine(beamIndices, line)
  
        
    print(getNPaths(beamIndices))

