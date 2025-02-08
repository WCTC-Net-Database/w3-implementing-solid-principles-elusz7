using System.Text;
using Spectre.Console;
namespace CharacterConsole;

public class CharacterManager
{
    private readonly IInput _input;
    private readonly IOutput _output;
    private readonly string _filePath = "input.csv";

    private string[] lines;
    private List<Character> characters;
    private CharacterReader reader;
    private CharacterWriter writer;

    public CharacterManager(IInput input, IOutput output)
    {
        _input = input;
        _output = output;
    }

    public void Run()
    {
        lines = File.ReadAllLines(_filePath);

        reader = new CharacterReader();
        characters = reader.ReadCharacters(lines);

        writer = new CharacterWriter(_filePath);

        var menuOptions = new StringBuilder()
                .AppendLine("1. Display Characters")
                .AppendLine("2. Find Character")
                .AppendLine("3. Add Character")
                .AppendLine("4. Level Up Character")
                .AppendLine("5. Exit")
                .ToString();

        var panel = new Panel(menuOptions)
            .Header("[mediumpurple3_1]Character Management Menu[/]")
            .Border(BoxBorder.Rounded)
            .Padding(2, 2, 2, 2);

        while (true)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(panel);
            
            var choice = AnsiConsole.Prompt(
                new TextPrompt<string>("Which [darkmagenta_1]option[/] would you like to choose?")
                    .AddChoice("1")
                    .AddChoice("2")
                    .AddChoice("3")
                    .AddChoice("4")
                    .AddChoice("5")
                    .WithConverter(choice => choice));
            AnsiConsole.WriteLine();

            switch (choice)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    FindCharacter();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    return;
            }
        }
    }

    public void DisplayCharacters()
    {
        var table = new Table();

        table.AddColumn("[red]Name[/]");
        table.AddColumn("[green]Class[/]");
        table.AddColumn("[yellow]Level[/]");
        table.AddColumn("[blue]Health[/]");
        table.AddColumn("[orange3]Equipment[/]");

        foreach (var character in characters)
        {
            table.AddRow(
                character.Name,
                character.Class,
                character.Level.ToString(),
                character.Health.ToString(),
                string.Join(", ", character.Equipment)
            );
        }

        AnsiConsole.Write(table);
    }

    public void FindCharacter()
    {
        var name = AnsiConsole.Prompt(
           new TextPrompt<string>("Search for [deepskyblue1]character[/]:"));

        var character = characters.FirstOrDefault(c => c.Name.Contains(name));
        if (character != null)
        {
            var table = new Table();

            table.AddColumn("[red]Name[/]");
            table.AddColumn("[green]Class[/]");
            table.AddColumn("[yellow]Level[/]");
            table.AddColumn("[blue]Health[/]");
            table.AddColumn("[orange3]Equipment[/]");

            table.AddRow(
                character.Name,
                character.Class,
                character.Level.ToString(),
                character.Health.ToString(),
                string.Join(", ", character.Equipment)
            );

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[deepskyblue1]Character[/] not found.");
        }
    }

    public void AddCharacter()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your character's [red]name[/]:"));

        var cclass = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your character's [green]class[/]:"));

        var level = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter your character's [yellow]level[/]:")
                .Validate(level => level >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Level must be a non-negative integer[/]")));

        var health = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter your character's [blue]health[/]:")
                .Validate(health => health >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Health must be a non-negative integer[/]")));

        var equipment = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your character's [orange3]equipment[/] (separate items with a '|'):"));

        var table = new Table();
        table.AddColumn("[red]Name[/]");
        table.AddColumn("[green]Class[/]");
        table.AddColumn("[yellow]Level[/]");
        table.AddColumn("[blue]Health[/]");
        table.AddColumn("[orange3]Equipment[/]");

        table.AddRow(
            name,
            cclass,
            level.ToString(),
            health.ToString(),
            string.Join(", ", equipment.ToString())
        );

        AnsiConsole.Write(table);

        var newCharacter = new Character(name, cclass, level, health, equipment.Split('|'));

        characters.Add(newCharacter);
        writer.WriteCharacters(characters);
    }

    public void LevelUpCharacter()
    {
        var name = AnsiConsole.Prompt(
           new TextPrompt<string>("Which [deepskyblue1]character[/] would you like to [deeppink3_1]level up[/]?"));
        var index = characters.FindIndex(c => c.Name.Contains(name));
        if (index > -1)
        {
            characters[index].LevelUp();
            AnsiConsole.MarkupLine($":sparkles: [red]{name}[/] has [deeppink3_1]leveled up[/]! :sparkles:");
            writer.WriteCharacters(characters);
        }
        else
        {
            AnsiConsole.MarkupLine($"[deepskyblue1]Character[/]: [red]{name}[/] not found.");
        }
    }
}