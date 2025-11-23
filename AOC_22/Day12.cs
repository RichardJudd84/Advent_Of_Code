using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day12{
        private static int[,] map;
        private static int[] start, end;

        private static int height, width;
        private static void parseData(String[] data){
            height = data.Length;
            width = data[0].Length;
            map = new int[height, width];
            for(int y = 0; y<height; y++){
                for(int x = 0; x<width; x++){
                    map[y,x] = (int)data[y][x];
                    if(data[y][x] == 'S') {
                        start = new int[]{y,x};
                        map[y,x] = (int)'a';
                    }
                    else if(data[y][x] == 'E') {
                        end = new int[]{y,x};
                        map[y,x] = (int)'z';
                    }
                    
                    //Console.Write(map[y,x]+" ");
                }
                //Console.WriteLine();
            }
        }
        private static void DFS(){
            HashSet<String> explored = new HashSet<String>();
            List<int[]> frontier = new List<int[]>(), nextFrontier = new List<int[]>(); 
            frontier.Add(start);
            
            int[][] directions = new int[][]{new int[]{1,0},new int[]{-1,0},new int[]{0,-1},new int[]{0,1}};
            bool found = false;
            int steps = 0;
            int y = start[0], x = start[1];
            explored.Add($"{y},{x}");
            while(!found){
                if (frontier.Count == 0) {
                    Console.WriteLine("got stuck!");
                    break;
                } 
                Console.Write(steps + ": ");
                foreach(var loc in frontier){
                    y = loc[0];
                    x = loc[1];
                    Console.Write($"({y},{x}): {map[y,x]},");

                    foreach(var dir in directions){
                        int ty = loc[0]+dir[0], tx =  loc[1]+dir[1];
                        
                        if(ty<0 || ty >= height || tx<0 || tx>= width) continue;
                        else if(explored.Contains($"{ty},{tx}"))continue;
                        else if(map[ty,tx] - map[y,x] > 1) continue;//Console.WriteLine($"Big Leap: {(char)map[ty,tx]} - {(char)map[y,x]}");
                        else if(end[0] == ty && end[1] == tx){
                            found  = true;
                            Console.WriteLine("found");
                        } 
                        
                        else{
                            //Console.WriteLine(ty +" "+ tx);
                            nextFrontier.Add(new int[]{ty,tx});
                            explored.Add($"{ty},{tx}");
                        }                   
                    }
                }
                frontier = nextFrontier;
                nextFrontier = new List<int[]>();
                steps++;
                Console.WriteLine();
            }

            Console.WriteLine(steps);

        }

        
        private static void DFS2(){
            HashSet<String> explored = new HashSet<String>();
            List<int[]> frontier = new List<int[]>(), nextFrontier = new List<int[]>(); 
            frontier.Add(end);
            
            int[][] directions = new int[][]{new int[]{1,0},new int[]{-1,0},new int[]{0,-1},new int[]{0,1}};
            bool found = false;
            int steps = 0;
            int y = end[0], x = end[1];
            explored.Add($"{y},{x}");
            while(!found){
                if (frontier.Count == 0) {
                    Console.WriteLine("got stuck!");
                    break;
                } 
                Console.Write(steps + ": ");
                foreach(var loc in frontier){
                    y = loc[0];
                    x = loc[1];
                    Console.Write($"({y},{x}): {map[y,x]},");

                    foreach(var dir in directions){
                        int ty = loc[0]+dir[0], tx =  loc[1]+dir[1];
                        
                        if(ty<0 || ty >= height || tx<0 || tx>= width) continue;
                        else if(explored.Contains($"{ty},{tx}"))continue;
                        else if(map[y,x] - map[ty,tx] > 1) continue;
                        else if(map[ty,tx] == (int)'a'){
                            found  = true;
                            Console.WriteLine("found");
                        } 
                        
                        else{
                            //Console.WriteLine(ty +" "+ tx);
                            nextFrontier.Add(new int[]{ty,tx});
                            explored.Add($"{ty},{tx}");
                        }                   
                    }
                }
                frontier = nextFrontier;
                nextFrontier = new List<int[]>();
                steps++;
                Console.WriteLine();
            }

            Console.WriteLine(steps);

        }

        private static void result(int[] data){

        }

        private static void result2(int[] data){
        }


        public static void run(){
            Console.WriteLine("Day 12");
            String[] data = Helpers.getData("./Data/d12.txt");
            parseData(data);
            DFS2();
            //result(cData);
            //result2(cData);
        }
    }
    
}