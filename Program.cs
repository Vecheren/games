namespace ConsoleGames;

public class Program
{
    public static void Main()
    {
        var enemyMap = new GameMap();
        enemyMap.FillPCMap(@"C:\Users\veche\RiderProjects\ConsoleGames\ConsoleGames\map1.txt");
        var playerMap = new GameMap();
        playerMap.FillPlayerMap();
        var game = new Game(playerMap, enemyMap);
        game.Start();
        Console.ReadLine();
    }
}