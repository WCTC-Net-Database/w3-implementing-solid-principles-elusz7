using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterConsole;
public class CharacterWriter
{
    private readonly string FilePath;

    public CharacterWriter(string filePath)
    {
        this.FilePath = filePath;
    }

    public void WriteCharacters(List<Character> characters)
    {
        List<string> lines = new List<string>();
        lines.Add("Name,Class,Level,Health,Equipment");
        foreach (var character in characters)
        {
            lines.Add(CsvOutput(character));
        }
        //File.WriteAllLines(filePath, lines);
        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }

    private string CsvOutput(Character character)
    {
        return $"{GetName(character.Name)},{character.Class},{character.Level},{character.Health},{string.Join("|", character.Equipment)}";
    }

    private string GetName(string name)
    {
        if (name.Contains(" "))
        {
            int index = name.IndexOf(" ");
            name = $"\"{name.Substring(index + 1)}, {name.Substring(0, index)}\"";
        }
        return name;
    }
}