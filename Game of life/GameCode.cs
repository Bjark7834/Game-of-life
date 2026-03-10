using System;
using System.Collections.Generic;
using System.Text;

namespace Game_of_life
{
    public class GameCode
    {
        const int MaxX = 100;
        const int MaxY = 20;

        bool[,] GameBoard = new bool[MaxX, MaxY];
        bool[,] TempBoard = new bool[MaxX, MaxY];
        Random rnd = new Random(1);
        bool boolvalue;
        int ForT = 1;
        bool loop = true;
        int Generations = 0;


        public GameCode() 
        {
            
        }

        public void run()
        {
            

            // initialize the game board with random values
            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {


                    ForT = rnd.Next(2);
                    if (ForT == 0)
                    {
                        boolvalue = false;
                    }
                    else
                    {
                        boolvalue = true;
                    }
                    GameBoard[x, y] = boolvalue;
                }
            }



            //flying machine
            /*
            GameBoard[2, 1] = true;
            GameBoard[3, 2] = true;
            GameBoard[1, 3] = true;
            GameBoard[2, 3] = true;
            GameBoard[3, 3] = true;
            */


            // print the game board

            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    Console.Write(GameBoard[x, y] ? "█" : "▒");
                }

                Console.WriteLine();
            }



            
            Console.WriteLine("Enter number of Generations");

            while (loop == true)
            {
                string Input = Console.ReadLine();
                int Value;

                if (int.TryParse(Input, out Value))
                {
                    Generations = Convert.ToInt32(Input);
                    loop = false;
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");

                }
            }
            
            runGenerations(Generations);
        }



        // run the game for x generations
        void runGenerations(int generations)
        {
            for (int gen = 0; gen < generations; gen++)
            {

                for (int y = 0; y < MaxY; y++)
                {
                    for (int x = 0; x < MaxX; x++)
                    {
                        int Neighbor = countNeighbors(GameBoard, x, y);

                        if (GameBoard[x, y] == true)
                        {
                            if (Neighbor < 2)
                            {
                                TempBoard[x, y] = false;
                            }

                            else if (Neighbor > 3)
                            {
                                TempBoard[x, y] = false;
                            }


                        }

                        else
                        {
                            if (Neighbor == 3)
                            {
                                TempBoard[x, y] = true;
                            }
                        }
                    }

                }
                StringBuilder gameString = new StringBuilder();
                for (int y = 0; y < MaxY; y++)
                {
                    for (int x = 0; x < MaxX; x++)
                    {
                        gameString.Append(TempBoard[x, y] ? "█" : "▒");
                    }

                    gameString.AppendLine();
                }
                copyBoard(TempBoard, GameBoard);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(gameString.ToString());
                Thread.Sleep(500);
            }
        }

         void copyBoard(bool[,] source, bool[,] destination)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    for (int x = 0; x < MaxX; x++)
                    {
                        destination[x, y] = source[x, y];
                    }
                }
            }



             int countNeighbors(bool[,] board, int x, int y)
            {
                int count = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue; // Skip the current cell
                        int neighborX = x + i;
                        int neighborY = y + j;
                        if (neighborX >= 0 && neighborX < MaxX && neighborY >= 0 && neighborY < MaxY)
                        {
                            if (board[neighborX, neighborY]) count++;
                        }
                    }
                }
                return count;
            }
    }
}
