using UnityEngine;
using System.Collections;
using System;
using Assets.Clustering;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
public class matrixtest : MonoBehaviour
{
    int _oneDGridSize = 0;
    // Use this for initialization
    int gridX = 0;
    int gridY = 0;
    void Start()
    {
/*        Matrix<Complex> A = Matrix<Complex>.Build.Random(200, 200);
        SparseMatrix S = SparseMatrix.OfMatrix(A);
        string s = A.ToString();
        string s1 = S.ToString();
        print(s1);
        print(s);*/
       
        int[,] grid = new int[1,1];
        int counter = 0;
        string line="";
     
        // Read the file and display it line by line.
        System.IO.StreamReader file =
           new System.IO.StreamReader("boolGrid.txt");
        while ((line = file.ReadLine()) != null)
        {
            //first two lines should be the grid size of x and y
            if (counter == 0)
            {
                gridX = int.Parse(line);

                counter++;
                continue;
            }
            if (counter == 1)
            {
                gridY = int.Parse(line);
                grid = new int[gridX,gridY];
                counter++;
                continue;
            }
            //comma counter
            int commaCounter=0;
            for (int i = 0; i < line.Length; i++)
            {
                if (i%2 != 0)
                    continue;
                grid[counter - 2, i/2] = int.Parse(""+line[i]);
            }



            counter++;





        }
        file.Close();
        if (gridX > gridY)
        {
            _oneDGridSize =gridX;
            grid = PadAndFillAdjacencyMatrix(grid, "y");
        }
        else
        {
            _oneDGridSize  =gridY;
            grid =PadAndFillAdjacencyMatrix(grid, "x");
        }
        
                string output ="";


        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {

                output += "" + grid[x, y];
                if (y < gridY - 1)
                    output += ",";
                else
                    output += ";";

            }
            if (x < gridX - 1)
                output += "\r\n"; //newline
        }
        System.IO.StreamWriter file1 = new System.IO.StreamWriter("boolGrid1.txt");
                file1.WriteLine(output);

                file1.Close();


       // SpectralClustering cluster = new SpectralClustering(grid, 3);
        print("here");

    }

    private int[,] PadAndFillAdjacencyMatrix(int[,] oldAdjacencyMatrix, string p)
    {
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
        gridX = gridY = _oneDGridSize;
        return _adjacencyMatrix;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
