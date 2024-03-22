using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLifeMVVM.Model
{
    class GOFModel
    {
        private int[,] grid_;

        public GOFModel(int rows, int columns) 
        {
            grid_ = new int[rows,columns];
            RandomInit();
        }

        private void RandomInit() {
            var rnd = new Random();
            for (int i = 0; i < grid_.GetLength(0); i++)
            {
                for (int j = 0; j < grid_.GetLength(1); j++)
                {
                    grid_[i, j] = rnd.Next(0, 2);
                }
            }
        }

        private int[,] NextGeneration()
        {
            int width = grid_.GetLength(0);
            int height = grid_.GetLength(1);

            int[,] nextGeneration = new int[width,height];

            for (int i = 0;i < width;i++)
            {
                for (int j = 0;j < height;j++)
                {
                    int liveNeighbors = CountLiveNeighbors(i, j);
                    if (grid_[i, j] == 1)
                    {
                        if (liveNeighbors == 2 || liveNeighbors == 3)
                        {
                            nextGeneration[i, j] = 1;
                        }
                        else
                        {
                            nextGeneration[i, j] = 0;
                        }
                    }
                    else
                    {
                        if (liveNeighbors == 3)
                        {
                            nextGeneration[i, j] = 1;
                        }
                        else
                        {
                            nextGeneration[i, j] = 0;
                        }
                    }
                }
            }

            return nextGeneration;
        }

        private int CountLiveNeighbors(int x, int y)
        {
            int result = 0;

            int wigth = grid_.GetLength (0);
            int height = grid_ .GetLength (1);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = (x + i + wigth) % wigth;
                    int neighborY = (y + j + height) % height;

                    if (!(i == 0 && j == 0))
                    {
                        result += grid_[neighborX, neighborY];
                    }
                }
            }

            return result;
        }
        public int[,] Grid { get 
            {
                grid_ = NextGeneration();
                return grid_;
            } }
    }
}
