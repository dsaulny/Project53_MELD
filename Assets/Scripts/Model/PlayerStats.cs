using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStats 
{
    public long totalTimeTaken = 1000;
    public string Name = "Lab_User_1";
    public float BaseAttackSpeed = 1;
    public int currentSection = 1;
    public bool HasStartedTutorial;
    public bool HasCompletedSafetyTraining;

    public Dictionary<Slot, Equipment> Equipment = new Dictionary<Slot, Equipment>()
    {
        { Slot.Head, new Armor() {
            Name = "Tiara",
            EquippedSlot = Slot.Head,
            Rarity = Rarity.Common,
            Value = 5,
            Defense = 1
        }},
        { Slot.Chest, new Armor() {
            Name = "Rags",
            EquippedSlot = Slot.Chest,
            Rarity = Rarity.Common,
            Value = 5,
            Defense = 1
        } },
        { Slot.Legs, new Armor() {
            Name = "Rags",
            EquippedSlot = Slot.Legs,
            Rarity = Rarity.Common,
            Value = 5,
            Defense = 1
        } },
        { Slot.Feet, null },
        { Slot.Hands, null },
        { Slot.Neck, null },
        { Slot.Ammo, null },
        { Slot.WeaponLeft, null },
        { Slot.Shield, null },
        { Slot.TwoHandedWeapon, null },
        { Slot.Wrist, null }
    };

    public List<Item> Inventory = new List<Item>() { 
        new Item() { 
            Value = 1, 
            IsConsumable = false, 
            Name = "Gold Pieces", 
            Rarity = Rarity.Common, 
            Quantity = 10 
        },
        new Item() { 
            Value = 1, 
            IsConsumable = false, 
            Name = "Other Gold Pieces", 
            Rarity = Rarity.Common, 
            Quantity = 10 
        } 
    };
}
