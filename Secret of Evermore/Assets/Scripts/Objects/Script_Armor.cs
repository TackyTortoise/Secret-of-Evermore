using System;

public class Script_Armor : Script_Item
{
    public enum ArmorType
    {
        Chest,
        Legs,
        Helm
    }

    public ArmorType GearType { get; private set; }

    public Script_Armor(String name, ArmorType type, int defenseBonus)
    {
        Name = name;
        GearType = type;
        Type = ItemType.Armor;
        DefenseBoost = defenseBonus;
    }
}
