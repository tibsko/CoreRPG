using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public struct ItemAmount {
    public Item Item;
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;
}
