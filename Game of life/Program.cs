using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game_of_life
{
    // store program-wide settings here so other classes can reference them
    public static class ProgramState
    {   


        // if true, the program will ask the user how many generations to run
        public static bool askGen = true;


        // if askGen is false, the program will use this value for the number of generations to run
        public static int Gens = 25;


        // the character used to represent alive cells
        public static string aliveChar = "█";


        // the character used to represent dead cells
        public static string deadChar = "▒";


        // the number of randomly selected alive cells to start with
        //doesnt work well with too high or too low values
        public static int startAlive = 1500;


        // the size of the game board
        //setting x too high will cause the console to wrap around
        public const int MaxX = 100;
        public const int MaxY = 20;

    }




    public static class Program
    {
        public static void Main()
        {
            GameCode game = new GameCode();
            game.run();
        }
    }
}
