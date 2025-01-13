using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Game/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public ERarity rarity;
    public Sprite sprite;
    public GameObject prefab; // Reference the prefab directly
    public float itemScale;
    public int maxQuantity;
    public int price;
    public bool canBeEquipped;
    public bool canBeUsed;
    public bool canBeDropped;
    public bool canBeBought;
}
