using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_code_22{

    class Directory{
        private String name;
        private Dictionary<String, Directory> subDirs;
        private int files;
        private Directory parentDir;

        public Directory(String name){
            this.name = name;
            subDirs = new Dictionary<String, Directory>();
            files = 0;
        } 

        public Directory(String name, Directory parentDir){
            this.name = name;
            this.parentDir = parentDir;
            subDirs = new Dictionary<string, Directory>();
            files = 0;
        } 

        public String getName(){
            return this.name;
        }
        public Directory getParentDir(){
            return this.parentDir;
        }

        public void addSubDirectory(Directory subDir){
            this.subDirs.Add(subDir.getName(), subDir);
        }

        public Directory getSubDirectoryByName(String name){
            return this.subDirs[name];
        } 

        public void addFile(int size){
            this.files += size;
        }

        public int getDirSize(){
            int size = this.files;
            foreach(Directory d in this.subDirs.Values){
                size += d.getDirSize();
            }
            return size;
        }
    }


    class Day7{
        private static List<Directory> processData(String[] data){

            List<Directory> directories = new List<Directory>(); 

            Directory rootDir = new Directory("empty");
            Directory currentDir = new Directory("empty"); 

            foreach(String row in data){
                var elements = row.Split(" ");

                if(elements[0] == "$"){
                    switch(elements[1]){
                        case "cd": 
                            if (elements[2] =="/"){
                                rootDir = new Directory(elements[2]);
                                currentDir = rootDir;
                                directories.Add(rootDir);
                            }
                            else if (elements[2] == ".."){
                                currentDir = currentDir.getParentDir();
                            }
                            else{
                                currentDir = currentDir.getSubDirectoryByName(elements[2]);
                            }
                            break;
                        case "ls":
                            break;
                        default: 
                            Console.WriteLine("Something went wrong!");
                            break;
                    }
                }

                else if(elements[0] == "dir"){
                    Directory newDir = new Directory(elements[1], currentDir);
                    currentDir.addSubDirectory(newDir);
                    directories.Add(newDir);
                }

                else{
                    int fSize;
                    if (int.TryParse(elements[0], out fSize)){
                        currentDir.addFile(fSize);
                    }
                    else{
                        Console.WriteLine("Something went wrong!");
                    }
                }

                //Console.WriteLine("{0} {1} {2}",row, currentDir.getName(),currentDir.getDirSize());
            }
            return directories;
        }

        private static void result(List<Directory> data){
            int sum = 0;

            foreach(var dir in data){
                int sz = dir.getDirSize();
                if (sz<= 100000){
                    sum+=sz;
                }
            }

            Console.WriteLine(sum);
            
        }

        private static void result2(List<Directory> data){
            int requiredSpace = 30000000;
            int lowest = data[0].getDirSize() , space = 70000000 - data[0].getDirSize();
            Console.WriteLine("av Space: " + space);            
            for( int i = 1; i<data.Count(); i++){
                int sz = data[i].getDirSize();
                if (space + sz > requiredSpace && sz < lowest){
                    lowest = sz;
                }
            }

            Console.WriteLine(lowest);
        }


        public static void run(){
            Console.WriteLine("Day 7");
            String[] data = Helpers.getData("./Data/d7.txt");
            var dData = processData(data);
            //Console.WriteLine(dData[0].getName());
            //result(dData);
            result2(dData);
        }
    }
    
}