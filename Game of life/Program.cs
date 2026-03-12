using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game_of_life
{

    public static class Program
    {
        public static void Main()
        {
            GameCode game1 = new GameCode();
            GameCode game2 = new GameCode();
            GameCode game3 = new GameCode();


            game1.AskGen = true;
            game1.run();


            game2.GenTime = 100;
            game2.Gens = 40;
            game2.StartAlive = 800;
            game2.AliveChar = "X";
            game2.DeadChar = " ";
            game2.run();


            game3.AutoRun = false;
            game3.AskGen = true;
            game3.StartAlive = 400;
            game3.AliveChar = "o";
            game3.DeadChar = ".";
            game3.run();
        }
    }
}
