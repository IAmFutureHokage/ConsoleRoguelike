namespace ConsoleApp
{
    sealed class Program
    {
        static void Main()
        {
            int mapWidth = 100;
            int mapHeight = 25;
            int frameRate = 50;

            var gameInit = new GameInit(mapWidth, mapHeight, frameRate);
         
            gameInit.Run();
            //Пофиксить клик
        }
    }
}