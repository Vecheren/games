using System.Drawing;

namespace ConsoleGames;

public class GameMap
{
    public bool[,] Map { get;}
    public const int size = 10;

    public GameMap()
    {
        Map = new bool[size, size];
    }

    public bool TryShoot(int x, int y)
    {
        if (Map[x, y])
        {
            Map[x, y] = false;
            return true;
        }
        return false;
    }

    public void FillPlayerMap()
    {
        AddPlayerShips(1);
        AddPlayerShips(2);
        AddPlayerShips(3);
        AddPlayerShips(4);
    }

    private void AddPlayerShips(int decks)
    {
        Console.WriteLine($"Разместим {decks}-палубные корабли");
        for (int i = 0; i < 4 - decks + 1; i++)
        {
            AddShip(decks);
        }
    }

    private void AddShip(int decks, bool horizontal = true)
    {
        if (decks > 1)
        {
            Console.WriteLine("Корабль горизонтальный? Да / Нет");
            horizontal = Console.ReadLine().Trim().ToLower() == "да";
        }

        Console.WriteLine("Координаты:");
        var coords = Console.ReadLine().Split().Select(str => int.Parse(str)).ToArray();
        var x = coords[0];
        var y = coords[1];
        for (int i = 0; i < decks; i++)
        {
            var lastDesk = new Point(horizontal ? x : x - 1, horizontal ? y - 1 : y);
            if (ShipsAreAround(x, y, i > 0 ? lastDesk : new Point(-100, -100)))
            {
                Console.WriteLine("Рядом есть корабль. Попробуйте другие координаты");
                AddShip(decks);
                return;
            }

            Map[x, y] = true;
            if (horizontal) y++;
            else x++;
        }
    }

    private bool ShipsAreAround(int x, int y, Point LastDesk)
    {
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (!InBounds(i, j)) continue;
                if (i == LastDesk.X && j == LastDesk.Y) continue;
                if (Map[i, j]) return true;
            }
        }
        return false;
    }
    
    public void FillPCMap(string mapFileName)
    {
        using (var stream = new StreamReader(mapFileName))
        {
            for (int i = 0; i < size; i++)
            {
                var raw = stream.ReadLine();
                for (int j = 0; j < size; j++)
                {
                    Map[i, j] = raw[j] == 'y';
                }
            }
        }
    }
    
    public bool isLoose()
    {
        foreach (var el in Map)
        {
            if (el) return false;
        }
        return true;
    }
    
    private bool InBounds(int x, int y) => x >= 0 && x < 10 && y >= 0 && y < 10;
}