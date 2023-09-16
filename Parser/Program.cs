try
{
    // File lies in Desktop directory
    string directoryPath = GetDirectoryPath(@"C:\Users\91765\Desktop\");

    string inputFilePath = Path.Combine(directoryPath, "theMachineStops.txt");

    if (!File.Exists(inputFilePath))
    {
        Console.WriteLine("The input file does not exist.");
        return;
    }

    string text = "";
    string outputFilePath = Path.Combine(directoryPath, "TelegramCopy.txt");

    using (StreamReader sr = new StreamReader(inputFilePath))
    {
        text = sr.ReadToEnd();
        string newText = text.Replace(".", " (Stop)");
        using (StreamWriter sw = new StreamWriter(outputFilePath))
        {
            sw.WriteLine(newText);
        }
    }
    Console.WriteLine("Text file processing complete. The modified content has been saved to TelegramCopy.txt.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

static string GetDirectoryPath(string filePath)
{
    string directoryPath = Path.GetDirectoryName(filePath);
    Directory.SetCurrentDirectory(directoryPath);

    return directoryPath;
}
