import sys
import os  
from typing import Tuple, List
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getAdjacentPositions(data: List[str], row:int, col:int):
    maxY = len(data)-1
    maxX = len(data[0])-1

    positions: List[Tuple[int,int]] = []
    for y in range(row-1, row+2):
        if(y<0 or y > maxY):
            continue
        
        for x in range(col-1 , col+2):
            if(x < 0 or  x > maxX):
                continue
            if(y == row and x == col):
                continue
            positions.append((x,y))

    return positions

def getNumberAdjacentRolls(data: List[str], row:int, col:int):
    count = 0
    positions = getAdjacentPositions(data, row, col)
    for x,y in positions:
        if(data[y][x] == '@'):
            count+=1

    return count


if (__name__ == '__main__'):
    d = data
    count = 0

    for col in range(len(d)):
        for row in range(len(d[col])):
            if(d[row][col] == '.'):
                continue
            numberAdjacentRolls = getNumberAdjacentRolls(d, row, col)
            if(numberAdjacentRolls < 4):
                print(row, col , numberAdjacentRolls)
                count += 1
    
    print(count)