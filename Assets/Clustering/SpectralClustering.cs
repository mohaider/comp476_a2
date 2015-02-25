using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Clustering
{
    /**
     * Author: Mohammed R Haider 
     * Year: 2015
     * Contact: mrhaider@gmail.com
     * 
     * 
     * References: 
     * Ulrik Von Luxburg tutorial "A Tutorial on Spectral Clustering"
     * Statistics and Computing 17(4) 2007
     * 
     * 
     */
    class SpectralClustering
    {
        #region properties and variables
        private int[,] _adjacencyMatrix; //represents the nodes of a graph that are adjaent to another vertex. Ensure that this matrix is nxn, otherwise this will be padded by -1
        private float[,] _degreeMatrix; //a diagonal matrix whose elements_i are the sum of the elements of columns in the AdjacencyMatrix
        private float[,] _laplacianMatrix; //this is equal to _degreeMatrix -_adjacencyMatrix
        private int _gridX;             //size of the matrix in the X direction
        private int _gridY;             //size of the matrix in the Y direction
        private int _oneDGridSize;      //basically the n of of an nXn matrix
        private int _nClusters;                  //number of clusters to look for 
        
        private SpectralClusterType currentState;
        public enum SpectralClusterType
        {
            unnormalized =0,            //
            shiAndMalikNormalized,      //Normalized according to shi and malik(2000)
            JordanAndWeissNormalized    //Normalized according to Jordan and weiss
        }
        #endregion

        #region constructor

        public SpectralClustering(int[,] adjacencyMatrix, int clusterSize)
        {
            _gridX = adjacencyMatrix.GetLength(0);
            _gridY = adjacencyMatrix.GetLength(1);
            if (clusterSize == 0)
                throw new Exception("Cluster size cannot be set to 0");

            if (_gridX != _gridY)
            {
                if (_gridX > _gridY)
                {
                    _oneDGridSize = _gridX;
                    PadAndFillAdjacencyMatrix(adjacencyMatrix,"y"); //the matrix x > y, so we create a new adjacency matrix and pad the extra y space with 0s
                }
                else //gridY > gridX
                {
                    _oneDGridSize = _gridY;
                    PadAndFillAdjacencyMatrix(adjacencyMatrix, "x");//the matrix y>x, so we create a new adjacency matrix and pad the extra x space with 0s
                }
            }
            else
            {
                _adjacencyMatrix = adjacencyMatrix;
                _oneDGridSize = _gridX; 
            }
           
            CreateDegreeMatrix();

        }

   


        #endregion

        #region class functions
        private void PadAndFillAdjacencyMatrix(int[,] oldAdjacencyMatrix, string p)
        {
            _adjacencyMatrix = new int[_oneDGridSize, _oneDGridSize];
            switch (p)
            {
                case "x": //this case we pad the 
                    for (int y = 0; y < _oneDGridSize; y++)
                    {
                        for (int x = 0; x < _oneDGridSize; x++)
                        {
                            if (x >= _gridX)
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
                            if (y >= _gridY)
                                _adjacencyMatrix[x, y] = 0;
                            else
                            {
                                _adjacencyMatrix[x, y] = oldAdjacencyMatrix[x, y];
                            }
                        }
                    }


                    break;


            }
        }
        private void CreateDegreeMatrix()
        {
            _degreeMatrix= new float[_oneDGridSize, _oneDGridSize];
            float sum = 0;
            for (int x = 0; x < _oneDGridSize; x++)
            {
                for (int y = 0; y < _oneDGridSize; y++)
                {
                    _degreeMatrix[x, y] = 0;
                    sum += _adjacencyMatrix[x, y];

                }
                _degreeMatrix[x, x] = sum; //this ensures a diagonal matrix
                sum = 0;
            }
        }
        /// <summary>
        /// since this code is primarly used for a unity project, it is completely unoptimized. 
        /// As of this time, I was developing on unity 4.6.1 which uses the mono framework(i believe it is a 
        /// stripped down version of the 3.5 .net framework) so support for matrix libraries was lacking
        /// Anyways, down to the good stuff. This essentially calculates _degreeMatrix -_adjacencyMatrix and sets 
        /// it to the laplace matrix. This is a non normalized matrix
        /// </summary>
        private void ComputeLaplacianMatrix()
        {
            for (int x = 0; x < _oneDGridSize; x++)
            {
                for (int y = 0; y < _oneDGridSize; y++)
                {
                    _laplacianMatrix[x, y] = _degreeMatrix[x, y] - _laplacianMatrix[x,y];
                }
            }

        }

        private void ComputeNormalizedShiMalik()
        {
            for (int y = 0; y < _oneDGridSize; y++)
            {
                for (int x = 0; x < _oneDGridSize; x++)
                {
                    
                }
            }
        }

        private void ComputeNormalizedJordanWeiss()
        {
           

        }



        #endregion
        #region sets and gets
        public SpectralClusterType CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public int[,] AdjacencyMatrix
        {
            get { return _adjacencyMatrix; }
            set { _adjacencyMatrix = value; }
        }

        public float[,] DegreeMatrix
        {
            get { return _degreeMatrix; }
            set { _degreeMatrix = value; }
        }


        #endregion
    }
}
