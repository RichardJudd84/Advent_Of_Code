import sys
import os  
from typing import *
import math
import queue
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from utils.helpers import *
import input_paths

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getCsv(line: str):
    return([int(v) for v in line.split(',')])

def getCoords(data: List[str]):
    return [getCsv(line) for line in data]

def getDistance(coords1: List[int], coords2: List[int],):
    x1,y1,z1 = coords1
    x2,y2,z2 = coords2
    distance = math.sqrt((( x2 - x1)**2) +  ((y2 - y1) ** 2) + ((z2 - z1) ** 2))
    return distance

def getClosestPairs(coords: List[List[int]]):
    sortedList = queue.PriorityQueue()
    for i1 in range(0,len(coords)-1):
        coords1 = coords[i1]
        for i2 in range(i1+1,len(coords)):
            coords2 = coords[i2]
            dist = getDistance(coords1, coords2)
            sortedList.put((dist,(coords1,coords2)))
    return(sortedList)

def getClusters(iterations: int, sortedCoords: queue.PriorityQueue):
    clusterNumber = 0
    clusters: Dict[int, Set[str]] = {}
    clusterMappings: Dict[List[int], int] = {}
    n=0
    while( n < iterations):
        sortedCoord = sortedCoords.get()
        dist, coords =  sortedCoord
        coord1 , coord2 = coords
        has1 = getCoordKey(coord1) in clusterMappings
        has2 = getCoordKey(coord2) in clusterMappings
        if(has1 and has2):
            clusterNr1 = clusterMappings[getCoordKey(coord1)]
            clusterNr2 = clusterMappings[getCoordKey(coord2)]
            if(clusterNr1 != clusterNr2):
                # Join clusters
                for coordKey in clusters[clusterNr2]:
                    clusters[clusterNr1].add(coordKey)
                    clusterMappings[coordKey] = clusterNr1
                clusters.pop(clusterNr2);
        if(has1):
            clusterNr = clusterMappings[getCoordKey(coord1)]
            clusters[clusterNr].add(getCoordKey(coord2))
            clusterMappings[getCoordKey(coord2)] = clusterNr

        elif(has2):
            clusterNr = clusterMappings[getCoordKey(coord2)]
            clusters[clusterNr].add(getCoordKey(coord1))
            clusterMappings[getCoordKey(coord1)] = clusterNr

        else:
            clusterNumber += 1
            newClusterSList = {getCoordKey(coord1), getCoordKey(coord2)}
            clusters[clusterNumber] = newClusterSList
            clusterMappings[getCoordKey(coord1)] = clusterNumber
            clusterMappings[getCoordKey(coord2)] = clusterNumber   
        n+=1  
       
    return clusters


def getCoordKey(coords: List[int]):
    return "".join(['c'+str(coord) for coord in coords])
    

if(__name__ == '__main__'):
    d = data 
    iterations = 1000
    coords = getCoords(d)
    sortedPairs = getClosestPairs(coords)
    clusters = getClusters(iterations, sortedPairs)
    clusterSizes = [len(clusters[cluster]) for cluster in clusters]
    clusterSizes.sort()

    product = 1
    for n in clusterSizes[-3:]:
        product *= n
        print(n,product)
    print(product)
