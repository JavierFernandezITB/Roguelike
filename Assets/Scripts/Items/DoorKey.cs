using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : AItem
{
    public override void Use()
    {
        RoomManagerService roomManagerService = GameObject.Find("RoomManagerService").GetComponent<RoomManagerService>();
        InventoryManagerService inventoryManagerService = GameObject.Find("InventoryManagerService").GetComponent<InventoryManagerService>();
        Vector2Int topOffset = new Vector2Int(0, 1);
        Vector2Int bottomOffset = new Vector2Int(0, -1);
        Vector2Int rightOffset = new Vector2Int(1, 0);
        Vector2Int leftOffset = new Vector2Int(-1, 0);

        RoomHandler currentRoomHandle = roomManagerService.GetCurrentRoomObject().GetComponent<RoomHandler>();
        currentRoomHandle.isRoomCompleted = true;
        currentRoomHandle.onOpenAllDoors.Invoke();

        roomManagerService.GetRoomFromCurrentOffset(topOffset)?.GetComponent<RoomHandler>().onOpenBottomDoor.Invoke();
        roomManagerService.GetRoomFromCurrentOffset(bottomOffset)?.GetComponent<RoomHandler>().onOpenTopDoor.Invoke();
        roomManagerService.GetRoomFromCurrentOffset(rightOffset)?.GetComponent<RoomHandler>().onOpenLeftDoor.Invoke();
        roomManagerService.GetRoomFromCurrentOffset(leftOffset)?.GetComponent<RoomHandler>().onOpenRightDoor.Invoke();

        inventoryManagerService.DeleteItemFromInventory(this);
    }

    public override void Attack()
    {
    }

    public override void Passive()
    {
    }
}
