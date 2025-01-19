using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : ServicesReferences
{
    RoomManagerService roomManagerService;
    public GameObject statuePrefab;
    public List<GameObject> availableEnemyList = new List<GameObject>();
    public bool isRunCompleted = false;
    public List<RoomHandler> roomsInCurrentRun = new List<RoomHandler>();

    void Start()
    {
        GameObject character = GameObject.Find("/Character");
        character.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, -20);

        roomManagerService = GameObject.Find("/RoomManagerService").GetComponent<RoomManagerService>();
        roomManagerService.GenerateDungeon();
    }

    private void Update()
    {
        if (!isRunCompleted && roomsInCurrentRun.Count > 0)
        {
            List<RoomHandler> completedRooms = new List<RoomHandler>();
            foreach (RoomHandler room in roomsInCurrentRun)
            {
                Debug.Log($"roomsIncurrent -> {roomsInCurrentRun.Count}");
                Debug.Log($"completedRooms -> {completedRooms.Count}");
                if (room.isRoomCompleted)
                    completedRooms.Add(room);
            }
            if (completedRooms.Count == roomsInCurrentRun.Count)
            {
                isRunCompleted = true;
                GameObject currentRoom = roomManagerService.GetCurrentRoomObject();
                GameObject exitStatue = Instantiate(statuePrefab, currentRoom.transform.position, Quaternion.identity);
                exitStatue.transform.GetChild(0).GetComponent<StatueScript>().sceneTarget = "Lobby";
            }
        }
    }
}
