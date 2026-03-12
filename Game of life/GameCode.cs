using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Game_of_life
{
    public class GameCode
    {

        // if true, the program will ask the user how many generations to run
        public bool AskGen { get; set; } = false;


        // if askGen is false, the program will use this value for the number of generations to run
        public int Gens { get; set; } = 25;


        // the character used to represent alive cells
        public string AliveChar { get; set; } = "█";


        // the character used to represent dead cells
        public string DeadChar { get; set; } = "▒";


        // the number of randomly selected alive cells to start with
        //doesnt work well with too high or too low values
        public int StartAlive { get; set; } = 1500;


        //sets time between generations in milliseconds
        public int GenTime { get; set; } = 500;


        //sets if generations should run automatically or wait for user input between each generation
        public bool AutoRun { get; set; } = true;

        // the size of the game board
        //setting x too high will cause the console to wrap around
        const int MaxX = 100;
        const int MaxY = 20;


        bool[,] GameBoard = new bool[MaxX, MaxY];
        bool[,] TempBoard = new bool[MaxX, MaxY];
        Random rnd = new Random();
        bool boolvalue;
        int ForT = 1;

        int Generations = 0;
        

        public void run()
        {
            TrueClear();
            Console.CursorVisible = true;
            // initialize the game board with random values
            /*
            for (int y = 0; y < ProgramState.MaxY; y++)
            {
                for (int x = 0; x < ProgramState.MaxX; x++)
                {

                    ForT = rnd.Next(2);
                    boolvalue = ForT == 1;
                    GameBoard[x, y] = boolvalue;
                }
            }
            */


            // initialize the game board with a set number of random alive cells
            int placed = initializeRandomAlive(StartAlive);
            Console.SetCursorPosition(0, MaxY);
            Console.WriteLine($"Placed {placed} alive cells");
            Console.SetCursorPosition(0, 0);


            //flying machine
            /*
            GameBoard[2, 1] = true;
            GameBoard[3, 2] = true;
            GameBoard[1, 3] = true;
            GameBoard[2, 3] = true;
            GameBoard[3, 3] = true;
            */


            // print the game board
            printBoard(GameBoard);



            // check if program should ask for generations or use the default value
            if (AskGen == true)
            {
                Console.WriteLine("Enter number of Generations");
                Generations = getNumber();
            }
            else
            {
                Console.WriteLine($"Press any key to run for {Gens} generations");
                Console.ReadKey();
                Generations = Gens;
            }

            TrueClear();
            printBoard(GameBoard);
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            runGenerations(Generations);
            TrueClear();
            printBoard(GameBoard);
            Console.SetCursorPosition(2, MaxY + 2);
            Console.Write("Simulation complete. Press any key to exit or continue to next.");
            Console.ReadKey();
        }

        int getNumber()
        {
            bool loop = true;
            while (loop == true)
            {
                string Input = Console.ReadLine();
                int Value;

                if (int.TryParse(Input, out Value))
                {
                    
                    loop = false;
                    return Value;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    
                }

            }
            return 0;
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
                        if (GameBoard[x, y])
                        {
                            TempBoard[x, y] = (Neighbor == 2 || Neighbor == 3);
                        }
                        else
                        {
                            TempBoard[x, y] = (Neighbor == 3);
                        }
                    }

                }
                printBoard(TempBoard);
                Console.SetCursorPosition(2, MaxY + 2);
                Console.WriteLine($"Generation {gen + 1}/{generations}");
                copyBoard(TempBoard, GameBoard);
                
                if (!AutoRun)
                {
                    Console.WriteLine("Press any key to continue to the next generation...");
                    Console.ReadKey();
                }
                else
                {
                    int time = GenTime;
                    Thread.Sleep(time);
                }
                Console.SetCursorPosition(0, 0);

            }
        }
        //generate random game board and print it to the console
        void printBoard(bool[,] board)
        {
            StringBuilder gameString = new StringBuilder();
            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    gameString.Append(board[x, y] ? AliveChar : DeadChar);
                }

                gameString.AppendLine();
            }
            Console.WriteLine(gameString.ToString());
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


        void TrueClear()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
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


        // place exactly `count` alive cells at random distinct positions on the board
        // Uses a Fisher-Yates shuffle to pick `count` distinct indices efficiently and deterministically.
        int initializeRandomAlive(int count)
        {
            // clear board first
            for (int y = 0; y < MaxY; y++)
                for (int x = 0; x < MaxX; x++)
                    GameBoard[x, y] = false;

            int maxCells = MaxX * MaxY;
            if (count <= 0) return 0;
            if (count >= maxCells)
            {
                // fill all
                for (int y = 0; y < MaxY; y++)
                    for (int x = 0; x < MaxX; x++)
                        GameBoard[x, y] = true;
                return maxCells;
            }

            // build index array
            int[] indices = new int[maxCells];
            for (int i = 0; i < maxCells; i++) indices[i] = i;

            // Fisher-Yates partial shuffle: shuffle first `count` positions
            for (int i = 0; i < count; i++)
            {
                int j = rnd.Next(i, maxCells);
                int tmp = indices[i];
                indices[i] = indices[j];
                indices[j] = tmp;
            }

            // set the first `count` indices to alive
            for (int k = 0; k < count; k++)
            {
                int idx = indices[k];
                int x = idx % MaxX;
                int y = idx / MaxX;
                GameBoard[x, y] = true;
            }

            return count;
        }
    }
}
