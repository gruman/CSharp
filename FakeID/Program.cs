using System;
using System.IO;

class Hangman
{
    static void Main()
    {
        // Read the words from an external file
        string[] firstNames = File.ReadAllLines("firstNames.txt");
        string[] lastNames = File.ReadAllLines("lastNames.txt");
        string[] countries = File.ReadAllLines("countries.txt");
        while (true)
        {
        // Pick a random word from the list
        Random random = new Random();
        string first = firstNames[random.Next(firstNames.Length)];
        string last = lastNames[random.Next(lastNames.Length)];
        string country = countries[random.Next(countries.Length)];
        int age = random.Next(8, 109);

        Console.Clear();
        Console.WriteLine($"{first} {last}");
        Console.WriteLine($"Age: {age}");
        Console.WriteLine($"Location: {country}");
        Console.WriteLine();
        Console.WriteLine("Press any key to regereate...");
        Console.ReadKey();
        }
    }
}
