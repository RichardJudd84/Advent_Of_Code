using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Advent_of_code_22{
    class Day14{
        private static int xNorm;
        private static int xNorm2;

        private static void printGRid(String[,] grid){
            for(int y = 0; y<grid.GetLength(0); y++){
                for(int x = 0; x<grid.GetLength(1); x++){
                    Console.Write(grid[y,x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private static String[,] processData(String[] data){
            int yMin =1000, yMax = 0, xMin = 1000, xMax= 0;
            List<List<int[]>> commands = new List<List<int[]>>(); 
            foreach(var row in data){
                List<int[]> command = new List<int[]>();
                var elements = row.Split("->");
                foreach(var element in elements){
                    var xy = element.Split(',');
                    int x = int.Parse(xy[0]);
                    int y = int.Parse(xy[1]);
                    if(x<xMin)xMin = x;
                    if(y<yMin)yMin = y;
                    if(x>xMax)xMax = x;
                    if(y>yMax)yMax = y;

                    command.Add(new int[2]{y,x});
                }
                commands.Add(command);
            }

            xNorm = xMin;

            String[,] grid  = new String[yMax+1, xMax-xMin+1];

            for(int y = 0; y<grid.GetLength(0); y++){
                for(int x = 0; x<grid.GetLength(1); x++)grid[y,x] = ".";
            }

            foreach(var command in commands){
                int cy=0, cx=0, ny=0, nx=0;
                cy = command[0][0];
                cx = command[0][1]-xNorm;
                for (int i = 1; i < command.Count; i++){
                    
                    ny = command[i][0];
                    nx = command[i][1]-xNorm;
                    //Console.WriteLine(cy +" " + cx);
                    //Console.WriteLine(ny +" " + nx);

                    int oy=0, ox=0;
                    if(cy-ny > 0)oy = -1;
                    else if(cy-ny < 0)oy = 1;
                    else if(cx-nx > 0)ox = -1;
                    else if(cx-nx < 0)ox = 1;
                    else Console.WriteLine("Misseds something!");

                    while(!(cx==nx && cy==ny)){
                        //Console.WriteLine($"cy: {cy}; yNorm: {yNorm}; max y: {grid.GetLength(0)}; cx: {cx}; xNorm: {xNorm}; max x: {grid.GetLength(1)}");
                        grid[cy,cx] = "#";
                        Console.Clear();
                        //printGRid(grid);
                        //Thread.Sleep(300);
                        //Console.WriteLine($"oy: {oy}; ox: {ox}");
                        cx += ox;
                        cy += oy;
                        //Console.WriteLine(cy +" " + cx);
                    }
                    grid[cy,cx] = "#";
                    //Console.WriteLine("Done!\n");
                } 
            }
            printGRid(grid);
            return grid;
        }

        private static String[,] addFloor(String[,] grid){

            int height = grid.GetLength(0), width = grid.GetLength(1);

            String[,] grid2 = new String[height+2, (height*2)+width];
            for(int y = 0; y<height+2; y++){
                for(int x=0; x<(height*2)+width; x++){
                    if(y == height+1) grid2[y,x] = "#";
                    else grid2[y,x] = ".";
                }
            }

            for(int y = 0; y<height; y++){
                for(int x=0; x<width; x++){
                    grid2[y,x+height] = grid[y,x];
                }
            }

            xNorm2 = xNorm - height;
            return grid2;
        }
        private static String[,] sand(String[,] grid, int norm){
            bool spill = false, landed = false;
            int[] grain = new int[]{0, 500-norm};


            int[] move(int[] pos){
                if(pos[0]+1 >= grid.GetLength(0)) spill = true;
                else if(grid[pos[0]+1, pos[1]] == ".") pos[0]+=1;
                else if(pos[1]-1 < 0) spill = true;
                else if(grid[pos[0]+1, pos[1]-1] == ".") {
                    pos[0]+=1;
                    pos[1]-=1;
                }
                else if(pos[1]+1 >= grid.GetLength(1)) spill = true;
                else if(grid[pos[0]+1, pos[1]+1] == "."){
                    pos[0]+=1;
                    pos[1]+=1;
                } 
                else landed = true;
                return pos;
            }

            while(!spill && !landed){
                grid[grain[0],grain[1]] = ".";
                grain = move(grain);
                grid[grain[0],grain[1]] = "o";
                //printGRid(grid);
                //Thread.Sleep(200);
            }
            if(!landed) grid[grain[0],grain[1]] = ".";
            return grid;
        }

        private static void result(String[] data){
            var grid = processData(data);
            int prevcnt=-1;
            int cnt = 0;
            while(prevcnt != cnt){
                prevcnt= cnt;
                cnt = 0;
                grid = sand(grid, xNorm);
                foreach(var cell in grid){
                    if (cell=="o") cnt++;
                }
            }
            printGRid(grid);
            Console.WriteLine(cnt);
            

        }

        private static void result2(String[] data){
            var grid = processData(data);
            grid = addFloor(grid);
            int prevcnt=-1;
            int cnt = 0;
            while(prevcnt != cnt){
                prevcnt= cnt;
                cnt = 0;
                grid = sand(grid, xNorm2);
                foreach(var cell in grid){
                    if (cell=="o") cnt++;
                }
            }
            printGRid(grid);
            Console.WriteLine(cnt);
        }


        public static void run(){
            Console.WriteLine("Day 14");
            String[] data = Helpers.getData("./Data/d14.txt");
            result2(data);
            
        }
    }
    
}