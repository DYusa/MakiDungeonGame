// just like taylors version this one is makis version, where he always wins!!

using System;

class DungeonCrawler
{
    static Random random = new Random();
    static int playerHealth = 100;
    static int playerGold = 0;
    static int gridSize = 5;
    static string[,] dungeon = new string[gridSize, gridSize];
    static int playerX = 0, playerY = 0;

    static void Main()
    {
        InitializeDungeon();
        Console.WriteLine("Welcome to the Dungeon Crawler!");
        Console.WriteLine("Explore rooms, feed Maki, and collect wet food before he does. Good luck!");
        Console.WriteLine("Spoiler: Maki always wins!");

        while (playerHealth > 0)
        {
            DisplayPlayerStats();
            Console.WriteLine("Enter a direction (north, south, east, west):");
            string direction = Console.ReadLine().ToLower();
            MovePlayer(direction);

            if (playerHealth <= 0)
            {
                Console.WriteLine("You ran out of energy and couldn't continue. Maki wins!");
                break;
            }

            if (playerX == gridSize - 1 && playerY == gridSize - 1)
            {
                Console.WriteLine("You reached the exit... but Maki was faster! Game over.");
                break;
            }
        }
    }

    static void InitializeDungeon()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                dungeon[i, j] = GetRandomRoomContent();
            }
        }
        dungeon[0, 0] = "start";
        dungeon[gridSize - 1, gridSize - 1] = "exit";
    }

    static string GetRandomRoomContent()
    {
        int roll = random.Next(1, 101);
        if (roll <= 30) return "empty";      // 30% chance
        if (roll <= 60) return "wet food";  // 30% chance
        return "maki";                      // 40% chance
    }

    static void DisplayPlayerStats()
    {
        Console.WriteLine($"Health: {playerHealth}, Food Saved: {playerGold}");
        Console.WriteLine($"Current position: ({playerX + 1}, {playerY + 1})");
    }

    static void MovePlayer(string direction)
    {
        int newX = playerX;
        int newY = playerY;

        switch (direction)
        {
            case "north":
                newX = Math.Max(0, playerX - 1);
                break;
            case "south":
                newX = Math.Min(gridSize - 1, playerX + 1);
                break;
            case "east":
                newY = Math.Min(gridSize - 1, playerY + 1);
                break;
            case "west":
                newY = Math.Max(0, playerY - 1);
                break;
            default:
                Console.WriteLine("Invalid direction. Use north, south, east, or west.");
                return;
        }

        if (newX == playerX && newY == playerY)
        {
            Console.WriteLine("You hit a wall. Try a different direction.");
            return;
        }

        playerX = newX;
        playerY = newY;
        ExploreRoom();
    }

    static void ExploreRoom()
    {
        string roomContent = dungeon[playerX, playerY];
        switch (roomContent)
        {
            case "empty":
                Console.WriteLine("The room is empty.");
                break;

            case "wet food":
                int foodFound = random.Next(5, 16); // Reduced food reward
                playerGold += foodFound;
                Console.WriteLine($"You found {foodFound} wet food. Maki is still ahead!");
                dungeon[playerX, playerY] = "empty";
                break;

            case "maki":
                Console.WriteLine("You encountered Maki, the hungry cat!");
                int energyLost = random.Next(20, 41); // Increased energy penalty
                playerHealth -= energyLost;
                Console.WriteLine($"Maki drained {energyLost} energy from you. He's unstoppable!");
                dungeon[playerX, playerY] = "empty";
                break;

            case "start":
                Console.WriteLine("You are at the starting point.");
                break;

            case "exit":
                Console.WriteLine("You found the exit! But Maki is already there. Game over.");
                break;
        }
    }
}
