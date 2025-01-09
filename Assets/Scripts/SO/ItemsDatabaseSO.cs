using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database", order = 2)]
public class ItemsDatabaseSO : ScriptableObject
{
    public List<ItemSO> currentActiveItems = new List<ItemSO>();
}
