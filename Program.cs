using System.Diagnostics;

string[] messages =
{
    "Your spine has filed a complaint. Stand up!",
    "You've been glued to that chair. Go get some water.",
    "Checkpoint reached! Rest your eyes and stretch.",
    "Damn man get up go beat some ass"
};


Console.Write("Remind you every how many minutes? ");
string? input = Console.ReadLine();

if (!int.TryParse(input, out int minutes) || minutes <= 0)
{
    Console.WriteLine("Enter a whole number greater than zero.");
    return;
}


string saveFile = "rounds.txt";
int completedRounds = 0;

if (File.Exists(saveFile))
{
    string savedText = File.ReadAllText(saveFile);
    int.TryParse(savedText, out completedRounds);
}

Console.WriteLine($"Previously completed rounds: {completedRounds}");


Console.Write("Reset your round count? Type yes, or press Enter to keep it: ");
string? answer = Console.ReadLine();

if (answer?.Trim().ToLowerInvariant() == "yes")
{
    completedRounds = 0;
    File.WriteAllText(saveFile, completedRounds.ToString());

    Console.WriteLine("Fresh start. Round count reset 👾");
}

Console.WriteLine("Guardian activated 👾 Press Ctrl+C to stop.");


while (true)
{
    int remainingSeconds = minutes * 60;


    while (remainingSeconds > 0)
    {
        TimeSpan remaining = TimeSpan.FromSeconds(remainingSeconds);

        Console.Write(
            $"\rTime until efficiency drop-off {remaining:mm\\:ss}!   "
        );

        await Task.Delay(1000);
        remainingSeconds--;
    }

    Console.WriteLine();


    completedRounds++;
    File.WriteAllText(saveFile, completedRounds.ToString());

    Console.WriteLine($"Focus rounds completed: {completedRounds} 🎯");


    int index = Random.Shared.Next(messages.Length);
    Console.WriteLine(messages[index]);


    var notification = new ProcessStartInfo("notify-send");
    notification.ArgumentList.Add("AFK Guardian 👾");
    notification.ArgumentList.Add(messages[index]);

    using var process = Process.Start(notification);


    Console.WriteLine("Step away. Press Enter when you're back 👾");
    Console.ReadLine();

    Console.WriteLine("Welcome back. Ready for the next round?");
}
