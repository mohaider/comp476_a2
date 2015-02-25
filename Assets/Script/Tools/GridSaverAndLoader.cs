using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Assets.Script.AI.PathFinding;
using UnityEngine;

namespace Assets.Script.Tools
{
    class GridSaverAndLoader:MonoBehaviour
    {
        #region class variables
        private XMLGridLoaderAndSaver saveAndLoad;
        private Grid grid;
        public bool Load = false;
        public bool startSaving = false;
        #endregion

        #region unity functions

        void Start()
        {
            saveAndLoad = new XMLGridLoaderAndSaver();
            grid = GetComponent<Grid>();
            saveAndLoad.GridSizeX = grid.GridSizeX;
            saveAndLoad.GridSizeY = grid.GridSizeY;
            if (startSaving)
            {
                int[,] adjacencyMatrix = grid.CreateAdjacencyMatrix();
              //  adjacencyMatrix = PadAndFillAdjacencyMatrix(adjacencyMatrix, adjacencyMatrix.GetLength(0),
               //     adjacencyMatrix.GetLength(1));
                string output ="";/*
                output += grid.GridSizeX;
                output += "\r\n";
                output += grid.GridSizeY;
                output += "\r\n";*/
                for (int x = 0; x < adjacencyMatrix.GetLength(0); x++)
                {
                    for (int y = 0; y < adjacencyMatrix.GetLength(1); y++)
                    {
                        output += ""+adjacencyMatrix[x,y];
                        if (y < adjacencyMatrix.GetLength(1) - 1)
                            output += ",";
                        else output += ";";
                    }
                    if (x < adjacencyMatrix.GetLength(0) - 1)
                        output += "\r\n"; //newline
                }
                
                System.IO.StreamWriter file = new System.IO.StreamWriter("adjacencyMatrix.txt");
                file.WriteLine(output);

                file.Close();
                /*Stopwatch sw = new Stopwatch();
                sw.Start();
                saveAndLoad.grid = grid.MakeListFromGrid();
                sw.Stop();
                float time = sw.ElapsedMilliseconds;
                print("total time to create a list from grid[][] in ms"+time);
                sw.Start();
                saveAndLoad.Save("grid.xml");
                sw.Stop();
                print("total time to save list to xml in ms"+(sw.ElapsedMilliseconds - time));*/
            }
        }

        #endregion
        private int[,] PadAndFillAdjacencyMatrix(int[,] oldAdjacencyMatrix, int gridX, int gridY)
        {
            int _oneDGridSize = 0;
            string p = "";
            if (gridX > gridY)
            {
                p = "y";
                _oneDGridSize = gridX;
            }
            else
            {
                p = "x";
                _oneDGridSize = gridY;
            }

            int[,] _adjacencyMatrix = new int[_oneDGridSize, _oneDGridSize];
            switch (p)
            {
                case "x": //this case we pad the 
                    for (int y = 0; y < _oneDGridSize; y++)
                    {
                        for (int x = 0; x < _oneDGridSize; x++)
                        {
                            if (x >= gridX)
                                _adjacencyMatrix[x, y] = 0;
                            else
                            {
                                _adjacencyMatrix[x, y] = oldAdjacencyMatrix[x, y];
                            }
                        }
                    }

                    break;


                case "y":
                    for (int x = 0; x < _oneDGridSize; x++)
                    {
                        for (int y = 0; y < _oneDGridSize; y++)
                        {
                            if (y >= gridY)
                                _adjacencyMatrix[x, y] = 0;
                            else
                            {
                                _adjacencyMatrix[x, y] = oldAdjacencyMatrix[x, y];
                            }
                        }
                    }


                    break;


            }
    
            return _adjacencyMatrix;
        }
    }

}
