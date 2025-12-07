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
    return [startIndex]

def processLine(beamIndices: List[int], line: str,) -> List[int] :
    newIndices: List[int] = []
    for i in beamIndices:
        if(line[i] == '^'):
            if(i == 0):
                newIndices.append(i+1)
            elif(i == len(line)-1):
                newIndices.append(i-1)
            else:
                newIndices.append(i+1)
                newIndices.append(i-1)
        else:
            newIndices.append(i)
    return newIndices



if(__name__ == '__main__'):
    d = data
    beamIndices = initStartDict(d[0])
    nLines = len(d)
    n = 1
    for line in d[1:] :
        n+=1
        print(n, 'of', nLines)
        beamIndices = processLine(beamIndices, line)
        
    print(len(beamIndices))

