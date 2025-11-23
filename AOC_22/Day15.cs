using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_code_22{
    class Day15{

        private static List<List<int>> sensors = new List<List<int>>();
        private static int xNorm = 0, yNorm = 0;
        private static String[,] grid = new String[0,0];

        public static void ParseData(String[] data){
            foreach(var row in data){
                sensors.Add((from num in Regex.Matches(row, @"(\-?\d+)") select int.Parse(num.Value)).ToList());
            }
            // normalise 
            int xMax = 0, xMin = 1000000000, yMax = 0, yMin = 1000000000;
            foreach(var s in sensors){
                int sx = s[0], sy = s[1], bx = s[2], by = s[3];
                int manhatten = Math.Abs(sx-bx) + Math.Abs(sy-by);
                int xmax = sx+manhatten, xmin = sx-manhatten, ymax = sy + manhatten, ymin = sy - manhatten;
                if (xmax > xMax) xMax = xmax;
                if (xmin < xMin) xMin = xmin;
                if (ymax > yMax) yMax = ymax;
                if (ymin < yMin) yMin = ymin;
            }
            //Console.WriteLine($" x min: {xMin}, x max: {xMax}, y min: {yMin}, y max: {yMax}");
            xNorm = xMin;
            yNorm = yMin;
            /*Console.WriteLine($"y max: {yMax-yNorm+1}, x max: {yMax-yNorm+1}");
            grid = new String[yMax-yNorm+1, yMax-yNorm+1];
            for(int y = 0; y< grid.GetLength(0); y++){
                for(int x = 0; x < grid.GetLength(1); x++) grid[y,x] = ".";
            }*/
        }

        private static HashSet<int> result(int yRow){
            HashSet<int> xPositions = new HashSet<int>(); 

            foreach(var s in sensors){                
                int sx = s[0], sy = s[1], bx = s[2], by = s[3];
                int manhatten = Math.Abs(sx-bx) + Math.Abs(sy-by);
                //Console.WriteLine($"sx: {sx}, sy: {sy}, bx: {bx}, by: {by}");
                //Console.WriteLine($" Manhatten distance: {manhatten}");

                for(int y = sy -manhatten, m = -manhatten ; y <= sy+manhatten; y++, m++){
                    if(y!=yRow) continue;
                    for(int x = sx - Math.Abs(Math.Abs(m)-manhatten); x <= sx + Math.Abs(Math.Abs(m)-manhatten); x++){
                        if( !((y == sy && x == sx)||(y == by && x == bx)) ) xPositions.Add(x);
                    }
                }
            }
            //Console.WriteLine(xPositions.Count);
            return xPositions;
        }
        private static void result1(){
            foreach(var s in sensors){                
                int sx = s[0]-xNorm, sy = s[1]-yNorm, bx = s[2]-xNorm, by = s[3]-yNorm;
                int manhatten = Math.Abs(sx-bx) + Math.Abs(sy-by);
                Console.WriteLine($"sx: {sx}, sy: {sy}, bx: {bx}, by: {by}");
                Console.WriteLine($" Manhatten distance: {manhatten}");

                for(int y = sy -manhatten, m = -manhatten ; y <= sy+manhatten; y++, m++){
                    for(int x = sx - Math.Abs(Math.Abs(m)-manhatten); x <= sx + Math.Abs(Math.Abs(m)-manhatten); x++){
                        grid[y,x] = "#";
                    }
                }
                grid[sy,sx] = "S";
                grid[by,bx] = "B";
            }

            Helpers.printGrid(grid);

            int ty = 2000000-yNorm , cnt = 0;

            for(int x = 0; x < grid.GetLength(1); x++){
                if(grid[ty,x] == "#") cnt++; 
            }

            Console.WriteLine(cnt);

        }

        private static int[] getXRange(int sx, int bx, int sy, int by, int y){
                int manhatten = Math.Abs(sx-bx) + Math.Abs(sy-by);
                if (y >= sy-manhatten && y <=sy + manhatten){
                    int xoffset = Math.Abs(Math.Abs(y-sy)-manhatten);
                    int xmin = sx-xoffset;
                    int xmax = sx+xoffset;
                /*Console.WriteLine($"sx: {sx}, sy: {sy}, bx: {bx}, by: {by}");
                Console.WriteLine($" Manhatten distance: {manhatten}");
                Console.WriteLine($" ypos: {y}");
                Console.WriteLine($" offset: {xoffset}");
                Console.WriteLine($" xmin: {xmin}, xmax: {xmax}");
                Console.ReadLine();*/
                return new int[]{xmin, xmax};
                }
                else return new int[0];
            }

        private static void result2(){
            int sz = 4000000;
            

            for (int y = 0; y <= sz; y++ ){
                //Console.ReadLine();
                if(y%1000 == 0)Console.WriteLine(y);
                List<int[]> remaining = new List<int[]>{new int[2]{0, sz}};

                foreach(var s in sensors){
                    if (remaining.Count == 0){
                        //Console.WriteLine("Nothing remains on row:");
                        break;
                    }

                    int sx = s[0], sy = s[1], bx = s[2], by = s[3];
                    var xrange = getXRange(sx,bx, sy, by, y);
                    if(xrange.Length == 0) continue;

                    //Console.Write("Remaining ranges: ");

                    //foreach (var range in remaining) Console.Write($"({range[0]}, {range[1]}), ");
                    //Console.WriteLine();

                    int xmin = xrange[0], xmax = xrange[1];                    
                    List<int[]> nxtRemaining = new List<int[]>();

                    //Console.WriteLine($"removing: ({xmin}, {xmax})");
                    
                    foreach(var range in remaining){

                        int rmin = range[0], rmax = range[1];                       

                        if (xmin <= rmin && xmax >= rmax) continue; // new range cover old, old deleted
                        else if (rmin < xmin &&  xmax < rmax){ // new range fits in old range, split old range 
                            nxtRemaining.Add(new int[]{rmin, xmin-1}); 
                            nxtRemaining.Add(new int[]{xmax+1, rmax});
                        }
                        else if (xmin > rmin && xmin <= rmax) nxtRemaining.Add(new int[]{rmin, xmin-1}); // new range covers right of old 
                        else if (xmax >= rmin && xmax < rmax) nxtRemaining.Add(new int[]{xmax+1, rmax}); // new range covers left of old 
                        else if (xmin > rmax || xmax < rmin) nxtRemaining.Add(range); // if new range is outside of old
                        else Console.WriteLine( $"Missed something!:({range[0]}, {range[1]})"); 
                    }

                    //Console.WriteLine($"before: {remaining.Count}, after: {nxtRemaining.Count}");
                    //foreach (var range in nxtRemaining) Console.Write($"({range[0]}, {range[1]}), ");
                    //Console.WriteLine();
                    //Console.WriteLine();
                    

                    remaining = nxtRemaining;
   
                }  
                
                
                if (remaining.Count > 0) {
                    Console.WriteLine($"count: {remaining.Count}, pos: ({remaining[0][0]}, {remaining[0][1]})");
                    Console.WriteLine($"res: {(remaining[0][0] * 4000000L)+ y}");
                    Console.ReadLine();
                }
            }      

        }


        public static void run(){
            Console.WriteLine("Day 15");
            // 29214561 too low
            String[] data = Helpers.getData("./Data/d15.txt");
            ParseData(data);
            /*var s = sensors[6];
            int sx = s[0], sy = s[1], bx = s[2], by = s[3];
            for(int y = -2; y <= 20; y++){
                var r = getXRange(sx,bx,sy,by,y);
                if(r.Length >0){
                    var min = r[0];
                    var max = r[1];
                    Console.WriteLine($"{y}: x min: {min}, x max: {max}");
                }
                else Console.WriteLine($"{y}");
            }*/

            result2();
            //var lst = (from num in Regex.Matches(data[0], @"(\-?\d+)") select int.Parse(num.Value)).ToList();
            //foreach(var row in lst) Console.WriteLine(row);
        
        }
    }
    
}