import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getCoords(data: List[str]):
    return [(int(x), int(y)) for line in data for x, y in [line.split(',')]]


def getPerimiter(coords: List[Tuple[int,int]]):
    perimiter: List[Tuple[int,int]] = []
    for i in range(len(coords)):
        ax, ay = coords[i]
        bx, by = coords[(i+1)%len(coords)]
        if (ax == bx and ay < by):
            # vertical Line down
            col = ax
            for row in range(ay, by+1):
                perimiter.append((col,row))
        elif (ax == bx and ay > by):
            # vertical Line up
            col = ax
            for row in range(ay,by-1,-1):
                perimiter.append((col,row))
        elif (ay == by and ax < bx):
            # horizontal line rigth
            row = ay
            for col in range(ax,bx+1):
                perimiter.append((col,row))
        elif (ay == by and ax > bx):
            # horizontal line left
            row = ay
            for col in range(ax,bx-1,-1):
                perimiter.append((col,row))
    return perimiter


def getMaxList(list: List[str],coords: List[Tuple[int,int]], perimiter: List[Tuple[int,int]]):
    largestAreas: List[Tuple(int,Tuple(int,int), Tuple(int,int))] = []

    sz = len(list)
    for a in range(sz-1):
        print(sz-a)
        coordA = list[a]
        for b in range(1,sz):
            coordB = list[b]
            area = getsquareSize(coordA, coordB)
            largestAreas.append((area,coordA,coordB))

    largestAreas.sort()
    cont = True
    while(cont):
        area, coordA, coordB = largestAreas.pop()
        if(not isPerimeterinArea(coordA, coordB, coords)):
            if(not isPerimeterinArea(coordA, coordB, perimiter)):
                print(area, coordA, coordB)
                cont = False
            else:
                print('p fail', area)


def isPerimeterinArea(coordA: str ,coordB: str, perimiter: List[Tuple[int,int]]):

    a = coordA.split(',')
    b = coordB.split(',')
    ax = int(a[0])
    ay = int(a[1])
    bx = int(b[0])
    by = int(b[1])

    for px,py in perimiter:
        
        isXin = px > min(ax,bx) and px < max(ax,bx)
        isYin = py > min(ay,by) and py < max(ay,by)
        if(isXin and isYin):
            return True
    return False

if(__name__ == '__main__'):
    d = data
    coords = (getCoords(d))
    perimiter = getPerimiter(coords)
    getMaxList(d,coords,perimiter)


