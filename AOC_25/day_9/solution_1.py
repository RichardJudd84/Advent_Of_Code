import sys
import os  
from typing import *
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getsquareSize(a:str, b:str):
    ax, ay = a.split(',')
    bx, by = b.split(',')

    x = (int(ax) , int(bx))
    y = (int(ay) , int(by))

    width = max(x) - min(x) + 1
    height = max(y) - min(y) + 1

    return width * height

def getMaxList(list: List[str]):
    largestArea = 0

    sz = len(list)
    for a in range(sz-1):
        coordA = list[a]
        for b in range(1,sz):
            coordB = list[b]
            area = getsquareSize(coordA, coordB)
            if(area > largestArea):
                largestArea = area
    print(largestArea)
        
if(__name__ == '__main__'):
    getMaxList(data)