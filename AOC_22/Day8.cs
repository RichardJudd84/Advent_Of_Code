using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{
    class Day8{

        private static int[,] processData(string[] data){
            int width = data[0].Length, height = data.Length;
            int[,] treeGrid = new int[height,width];

            for (int y = 0; y < height; y++){
                for (int x = 0; x < width; x++){
                    Char ch = data[y][x];
                    int n = int.Parse(ch.ToString());

                    treeGrid[y,x] = n;
                    //Console.Write(n + " ");
                }
                //Console.WriteLine();
            }
            return treeGrid;
        }

        private static bool isVisible(int[,] treeGrid, int y, int x ){
            int height = treeGrid.GetLength(0), width = treeGrid.GetLength(1);
            int t = treeGrid[y,x];
            bool iv, ivl = true, ivr = true, ivu= true, ivd = true;

            if(y == 0 || y == height-1 || x == 0 || y == width-1){
                iv = true;
            }
            else{
                for (int l = 0; l<x; l++) if (treeGrid[y,l]>=t) ivl = false;                    
                for (int r = x+1 ; r<width; r++) if (treeGrid[y,r]>=t) ivr = false;                    
                for (int u = 0; u<y; u++) if (treeGrid[u,x]>=t) ivu = false;                    
                for (int d = y+1; d<height; d++) if (treeGrid[d,x]>=t) ivd = false;   

                if (!ivu && !ivd && !ivl && !ivr) iv = false;
                else iv = true;               
            }

            //Console.WriteLine(" tree is Visible:" + iv);
            return iv;
        }

        private static int scenicScore(int[,] treeGrid, int y, int x){
            int height = treeGrid.GetLength(0), width = treeGrid.GetLength(1);
            int t = treeGrid[y,x];
            int uv=0, dv=0, lv=0, rv=0;

                for (int l = x-1; l>=0; l--){
                    if (treeGrid[y,l]>=t){
                        lv++;
                        break;
                    } 
                    else lv++;
                } 

                for (int r = x+1 ; r<width; r++){
                    if (treeGrid[y,r]>=t){
                        rv++;
                        break;
                    } 
                    else rv++;
                }  

                for (int u = y-1; u>=0; u--){
                    if (treeGrid[u,x]>=t){
                        uv++;
                        break;
                    } 
                    else uv++;

                }  

                for (int d = y+1; d<height; d++){
                    if (treeGrid[d,x]>=t){
                        dv++;
                        break;
                    } 
                    else dv++;
                } 

                Console.WriteLine($"up: {uv}, down: {dv}, left: {lv}, right: {rv}");
                int res = uv * dv * lv * rv;
                return res; 

        }

        private static void result(int[,] treeGrid){
            int cnt = 0;
            int yLen = treeGrid.GetLength(0), xLen = treeGrid.GetLength(1);
            for (int y = 0; y < yLen; y++){
                for (int x = 0; x < xLen; x++ ){
                    Char vis = 'f';
                    if(isVisible(treeGrid, y, x)){
                        vis = 't';
                        cnt++;
                    }
                    Console.Write(vis);
                }
                Console.WriteLine();
            } 
            Console.WriteLine("Count: " + cnt);        
        }

        private static void result2(int[,] treeGrid){
            int yLen = treeGrid.GetLength(0), xLen = treeGrid.GetLength(1);
            int max = 0;

            for (int y = 0; y < yLen; y++){
                for (int x = 0; x < xLen; x++ ){
                    int score = scenicScore(treeGrid, y,x);
                    if(score > max){
                        max = score;
                    }
                }
            } 
            Console.WriteLine("Scenic score: " + max);
   
        }


        public static void run(){
            Console.WriteLine("Day 1");
            String[] data = Helpers.getData("./Data/d8.txt");
            int[,] treeGrid = processData(data);
            int score = scenicScore(treeGrid, 1,2);
            Console.WriteLine(score);

            //result(treeGrid);
            result2(treeGrid);
        }
    }
    
}