using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CharacterConsole;

public class CharacterReader
{
    public List<Character> ReadCharacters(string[] lines)
    {
        var characters = new List<Character>();
        for (int i = 1; i < lines.Length; i++)
        {

            var values = ParseLine(lines[i]);
            var character = new Character()
            {
                Name = values[0],
                Class = values[1],
                Level = int.Parse(values[2]),
                Health = int.Parse(values[3]),
                Equipment = values[4].Split(';')
            };
            characters.Add(character);
        }
        return characters;
    }

    private string[] ParseLine(string line)
    {
        string name;
        string[] splitLine;

        // Check if the name is quoted
        if (line.StartsWith("\""))
        {
            // TODO: Find the closing quote and the comma right after it
            splitLine = line.Split("\",");
            name = splitLine[0];
            splitLine = splitLine[1].Split(",");
            // TODO: Remove quotes from the name if present and parse the name
            name = (name.Split(",")[1] + name.Split(",")[0]).Replace("\"", " ").Trim(); //switch name, change " to a space, and trim initial white space
            var list = new List<string> { name };
            list.AddRange(splitLine);
            splitLine = list.ToArray();
        }
        else
        {
            // TODO: Name is not quoted, so store the name up to the first comma
            splitLine = line.Split(",");
        }
        return splitLine;
    }
}