import sys
import os  
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))
from utils.helpers import *
import input_paths
from solution_1 import *

testData = getLineStrings(input_paths.input1ExamplePath)
data = getLineStrings(input_paths.input1Path)

def getLastPair(coordsList: List[List(int)], sortedCoords: queue.PriorityQueue):
    clusterNumber = 0
    clusters: Dict[int, Set[str]] = {}
    clusterMappings: Dict[List[int], int] = {}
    n=0
    while(not sortedCoords.empty()):
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
        print(len(clusters), coord1[0] * coord2[0])
        if(coord1 in coordsList):
            coordsList.remove(coord1)
        if(coord2 in coordsList):
            coordsList.remove(coord2)
        print(len(clusters), len(coordsList), coord1[0] * coord2[0])
        if(len(coordsList)==0 and len(clusters) == 1):
            print('complete')
            return

if(__name__ == '__main__'):
    d = data 
    coords = getCoords(d)
    sortedPairs = getClosestPairs(coords)
    getLastPair(coords, sortedPairs)


