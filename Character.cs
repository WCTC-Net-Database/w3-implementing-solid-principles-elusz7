using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CharacterConsole;

public class Character
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public string Class { get; set; }
    public string[] Equipment { get; set; }

    public Character() { }
    public Character(string name, string cclass, int level, int health, string[] equipment)
    {
        Name = name;
        Class = cclass;
        Level = level;
        Health = health;
        Equipment = equipment;
    }

    public void LevelUp()
    {
        Level++;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Level: {Level}, Health: {Health}, Class: {Class}, Equipment: {string.Join(", ", Equipment)}";
    }
}



