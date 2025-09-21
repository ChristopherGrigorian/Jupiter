using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DB/GameDB")]
public class GameDB : ScriptableObject
{
    public List<WeaponData> weapons;
    public List<ItemData> items;
    public List<CharacterData> characters;

    public WeaponData WeaponById(string id) => weapons.Find(w => w && w.id == id);
    public ItemData ItemById(string id) => items.Find(i => i && i.id == id);
    public CharacterData CharById(string id) => characters.Find(c => c && c.id == id);
}
