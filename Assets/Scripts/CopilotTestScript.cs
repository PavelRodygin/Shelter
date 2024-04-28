using System;

public class CopilotTestScript
{
    private static void Main(string[] args)
    {
        //Write me a guess number game, using Console input and output.
        //The computer should generate a random number between 1 and 100.
        //The user should be prompted to guess the number.
        //If the user guesses too high or too low then the computer should output "too high" or "too low" accordingly.
        //The user should continue to make guesses until they guess the correct number.
        //Once the user guesses the correct number, the computer should output how many guesses were taken.

        Console.WriteLine("Guess a number between 1 and 100!");
        int guess = Convert.ToInt32(Console.ReadLine());
        Random random = new Random();
        int randomNumber = random.Next(1, 100);
        int numberOfGuesses = 0;

        while (guess != randomNumber)
        {
            if (guess > randomNumber)
            {
                Console.WriteLine("Too high!");
                guess = Convert.ToInt32(Console.ReadLine());
                numberOfGuesses++;
            }
            else if (guess < randomNumber)
            {
                Console.WriteLine("Too low!");
                guess = Convert.ToInt32(Console.ReadLine());
                numberOfGuesses++;
            }
        }
        Console.WriteLine("You guessed correctly! It took you " + numberOfGuesses + " guesses.");
        Console.ReadLine();
    }
}