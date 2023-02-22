using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame
{
    public static class Program
    {
        [STAThread];
        static void Main()
        {

            string filePath = "./../../../errorLog.txt";
            try
            {
                using (var game = new Game1())
                    game.Run();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.IO.StreamWriter objWriter;
                objWriter = new System.IO.StreamWriter(filePath, true);
                objWriter.WriteLine("----------------------");
                objWriter.WriteLine(ex.Message);
                objWriter.Close();
            }
        }
    }
}