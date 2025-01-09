using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectorPosition { 
    Top,
    Bottom,
    Right,
    Left
}

public class RoomDetector : ServicesReferences
{
    public DetectorPosition detectorPosition;
    public GameObject parentRoomObject;
    public GameObject roomObject;
    public Vector2Int direction;

    private void Awake()
    {
        base.GetServices();
        parentRoomObject = gameObject.transform.parent.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && roomManagerService.GetCurrentRoomObject() != parentRoomObject)
        {
            switch (detectorPosition)
            { 
                case DetectorPosition.Top:
                    Debug.Log("Touched top detector.");
                    direction = new Vector2Int(0, -1);
                    break;
                case DetectorPosition.Bottom:
                    Debug.Log("Touched bottom detector.");
                    direction = new Vector2Int(0, 1);
                    break;
                case DetectorPosition.Right:
                    Debug.Log("Touched right detector.");
                    direction = new Vector2Int(-1, 0);
                    break;
                case DetectorPosition.Left:
                    Debug.Log("Touched left detector.");
                    direction = new Vector2Int(1, 0);
                    break;
            }

            roomManagerService.currentPlayerRoom = new Vector2Int(roomManagerService.currentPlayerRoom.x + direction.x, roomManagerService.currentPlayerRoom.y + direction.y);

            roomObject = roomManagerService.GetCurrentRoomObject();
            GameObject.Find("/Main Camera").transform.position = new Vector3(roomObject.transform.position.x, roomObject.transform.position.y, -10);
            if (!parentRoomObject.GetComponent<RoomHandler>().isRoomCompleted) {
                other.transform.position = new Vector3(other.transform.position.x + direction.x, other.transform.position.y + direction.y, other.transform.position.z);
                parentRoomObject.GetComponent<RoomHandler>().CloseAllDoors();
            }
        }
    }
}
