using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : AItem
{
    public override void Use()
    {
        Vector2Int topOffset = new Vector2Int(0, 1);
        Vector2Int bottomOffset = new Vector2Int(0, -1);
        Vector2Int rightOffset = new Vector2Int(1, 0);
        Vector2Int leftOffset = new Vector2Int(-1, 0);

        RoomHandler currentRoomHandle = roomManagerService.GetCurrentRoomObject().GetComponent<RoomHandler>();
        currentRoomHandle.isRoomCompleted = true;
        currentRoomHandle.OpenAllDoors();

        roomManagerService.GetRoomFromCurrentOffset(topOffset)?.GetComponent<RoomHandler>().OpenBottomDoor();
        roomManagerService.GetRoomFromCurrentOffset(bottomOffset)?.GetComponent<RoomHandler>().OpenTopDoor();
        roomManagerService.GetRoomFromCurrentOffset(rightOffset)?.GetComponent<RoomHandler>().OpenLeftDoor();
        roomManagerService.GetRoomFromCurrentOffset(leftOffset)?.GetComponent<RoomHandler>().OpenRightDoor();

        Destroy(gameObject);
    }

    public override void Equip()
    {
    }

    public override void Passive()
    {
    }
}
