using System;
using System.IO;

class Hangman
{
    static void Main()
    {
        // Read the words from an external file
        string[] words = File.ReadAllLines("words.txt");

        // Pick a random word from the list
        Random random = new Random();
        string wordToGuess = words[random.Next(words.Length)];

        // Initialize the game state
        char[] lettersGuessed = new char[wordToGuess.Length];
        int numIncorrectGuesses = 0;

        // Load the high score from the file, or default to 0
        int highScore = 0;

        if (File.Exists("highscore.txt"))
        {
            string highScoreString = File.ReadAllText("highscore.txt");
            int.TryParse(highScoreString, out highScore);
        }

        // Start the game loop
        while (true)
        {
            // Print the current game state
            Console.Clear();
            Console.WriteLine("Hangman");
            Console.WriteLine();
            Console.WriteLine("Word to guess:");

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (lettersGuessed[i] == 0)
                {
                    Console.Write("_ ");
                }
                else
                {
                    Console.Write(lettersGuessed[i] + " ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Incorrect guesses: " + numIncorrectGuesses);

            // Check if the game is over
            if (numIncorrectGuesses >= 6)
            {
                Console.WriteLine("You lose! The word was: " + wordToGuess);

                // Check if the score is higher than the current high score
                if (numIncorrectGuesses < highScore || highScore == 0)
                {
                    highScore = numIncorrectGuesses;
                    Console.WriteLine("New high score: " + highScore);

                    // Save the new high score to the file
                    File.WriteAllText("highscore.txt", highScore.ToString());
                }

                break;
            }

            if (Array.IndexOf(lettersGuessed, (char)0) == -1)
            {
                Console.WriteLine("You win!");

                // Check if the score is higher than the current high score
                if (numIncorrectGuesses < highScore || highScore == 0)
                {
                    highScore = numIncorrectGuesses;
                    Console.WriteLine("New high score: " + highScore);

                    // Save the new high score to the file
                    File.WriteAllText("highscore.txt", highScore.ToString());
                }

                break;
            }

            // Prompt the player for a guess
            Console.Write("Guess a letter: ");
            char guess = Console.ReadLine().ToLower()[0];

            // Update the game state based on the guess
            bool guessCorrect = false;

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guess)
                {
                    lettersGuessed[i] = guess;
                    guessCorrect = true;
                }
            }

            if (!guessCorrect)
            {
                numIncorrectGuesses++;
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
