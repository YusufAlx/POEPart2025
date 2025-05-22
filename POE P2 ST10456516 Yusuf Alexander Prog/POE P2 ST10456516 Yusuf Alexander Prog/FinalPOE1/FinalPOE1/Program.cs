using System;
using System.Collections.Generic;
using System.Media;

namespace POE
{
    class Program
    {
        // Variables to remember user details and interests
        static string userName = "";
        static string userInterest = "";
        static Random random = new Random();

        // Dictionary to store keyword-based responses, including randomized ones
        static Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Make sure to use strong, unique passwords for each account.",
                "Avoid using personal details in your passwords.",
                "Consider using a password manager to keep your credentials secure."
            }},
            { "phishing", new List<string> {
                "Be cautious of emails asking for personal information.",
                "Don't click on suspicious links in emails or messages.",
                "Always verify the sender's email address."
            }},
            { "privacy", new List<string> {
                "Review privacy settings on all your online accounts regularly.",
                "Limit the amount of personal information you share online.",
                "Use a VPN when accessing public Wi-Fi."
            }}
        };

        static void Main()
        {
            // Calling the PlaySound method
            PlaySound();

            // Changing the colour to Red for the ASCII art
            Console.ForegroundColor = ConsoleColor.Red;

            // Logo for the bot
            Console.WriteLine("  ____      _           _     _   _            ____                           \r\n / ___|   _| |__   ___ | |_  | |_| |__   ___  / ___|  ___  ___ _   _ _ __ ___ \r\n| |  | | | | '_ \\ / _ \\| __| | __| '_ \\ / _ \\ \\___ \\ / _ \\/ __| | | | '__/ _ \\\r\n| |__| |_| | |_) | (_) | |_  | |_| | | |  __/  ___) |  __/ (__| |_| | | |  __/\r\n \\____\\__, |_.__/ \\___/ \\__|  \\__|_| |_|\\___| |____/ \\___|\\___|\\__,_|_|  \\___|\r\n      |___/  ");

            Console.ResetColor();

            // Calling the welcomeMessageDisplayed method
            welcomeMessageDisplayed();

            // Calling the ResponseSystem method
            ResponseSystem();
        }

        // Method to play the sound file
        static void PlaySound()
        {
            // Path to the sound file
            string filePath = @"Greeting.wav";
            try
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: Could not locate the sound");
            }
        }

        // Method to display a welcome banner
        static void welcomeMessageDisplayed()
        {
            string welcomeMessage = @"
            |-------------------------------------------------------------|
            |                                                             |
            |       Welcome to your personal Cybersecurity expert!        |
            |-------------------------------------------------------------|                                                                                                                            
        ";
            // Changing the colour to Blue for the Welcome message
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(welcomeMessage);

            // Resetting the colour so that the stars pop out to show the separation
            Console.ResetColor();
            Console.WriteLine("*****************************************************************************");
        }

        // Method for handling user input and chatbot responses
        static void ResponseSystem()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            // Prompts the user to enter their name
            Console.Write("Please enter your name: ");
            userName = Console.ReadLine();
            Console.WriteLine($"Yo, {userName}! I'm here to help you with your Cybersecurity problems!");
            Console.ResetColor();
            Console.WriteLine();

            bool running = true;

            // Main chatbot loop
            while (running)
            {
                Console.WriteLine("*****************************************************************************");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nFeel free to ask me about the following cybersecurity problems:");
                Console.WriteLine("- Password safety");
                Console.WriteLine("- Phishing attacks");
                Console.WriteLine("- Safe browsing");
                Console.WriteLine("Type 'exit' to quit.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("*****************************************************************************");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.Write("Enter your question: ");
                string userInput = Console.ReadLine().ToLower();

                // Handles exit command
                if (userInput == "exit")
                {
                    running = false;
                    Console.WriteLine("Goodbye, stay safe online!");
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine("*****************************************************************************");
                    break;
                }

                // Sentiment detection and empathetic responses
                if (userInput.Contains("worried") || userInput.Contains("scared"))
                {
                    Console.WriteLine("It's okay to be worried. Cybersecurity can feel overwhelming, but I'm here to help.");
                    continue;
                }
                if (userInput.Contains("frustrated") || userInput.Contains("angry"))
                {
                    Console.WriteLine("Take a breath—you're not alone. Let's tackle your security questions together.");
                    continue;
                }
                if (userInput.Contains("curious") || userInput.Contains("interested"))
                {
                    Console.WriteLine("Curiosity is great! Let’s dig into some cybersecurity knowledge.");
                }

                // Memory feature — storing user's interest
                if (userInput.Contains("interested in"))
                {
                    int index = userInput.IndexOf("interested in") + "interested in".Length;
                    userInterest = userInput.Substring(index).Trim();
                    Console.WriteLine($"Great! I'll remember that you're interested in {userInterest}.");
                    continue;
                }

                // Recalling user interest and giving personalized advice
                if (!string.IsNullOrEmpty(userInterest))
                {
                    Console.WriteLine($"Since you're interested in {userInterest}, here's a related tip:");
                    GiveResponse(userInterest);
                }

                // Keyword recognition and random response selection
                bool matched = false;
                foreach (var key in responses.Keys)
                {
                    if (userInput.Contains(key))
                    {
                        GiveResponse(key);
                        matched = true;
                        break;
                    }
                }

                // Error handling for unknown input
                if (!matched)
                {
                    Console.WriteLine("I didn't quite understand that. Could you rephrase?\nType 'exit' to quit!");
                }
            }
        }

        // Method to randomly pick a response from the predefined list
        static void GiveResponse(string keyword)
        {
            if (responses.ContainsKey(keyword))
            {
                List<string> options = responses[keyword];
                string response = options[random.Next(options.Count)];
                Console.WriteLine(response);
            }
        }
    }