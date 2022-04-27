namespace ConsoleGames;

public class Game
{
    public GameMap playerMap;
    public GameMap PcMap;

    public Game(GameMap playerMap, GameMap pcMap)
    {
        this.playerMap = playerMap;
        this.PcMap = pcMap;
    }

    public void Start()
    {
        Console.WriteLine("Игра началась");
        while (true)
        {
            PlayerTurn();
            if (PcMap.isLoose())
            {
                Console.WriteLine("Вы выиграли!");
                break;
            }

            PcTurn();
            if (playerMap.isLoose())
            {
                Console.WriteLine("Вы проиграли!");
                break;
            }
        }
    }

    public void PlayerTurn()
    {
        Console.WriteLine("Куда стреляем?");
        var coords = Console.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        if (PcMap.TryShoot(coords[0], coords[1]))
        {
            Console.WriteLine("Попали!");
            PlayerTurn();
        }
    }

    public void PcTurn()
    {
        Console.WriteLine("Компьютер: пропускаю ход!");
    }
}