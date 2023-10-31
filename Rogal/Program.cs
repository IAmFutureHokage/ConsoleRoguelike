namespace ConsoleApp
{
    sealed class Program
    {
        static void Main()
        {
            int mapWidth = 100;
            int mapHeight = 25;
            int frameRate = 100;

            var gameInit = new GameInit(mapWidth, mapHeight, frameRate);
         
            gameInit.Run();
        }
    }
}