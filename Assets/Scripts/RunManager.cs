using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : ServicesReferences
{
    RoomManagerService roomManagerService;
    void Start()
    {
        roomManagerService = GameObject.Find("/RoomManagerService").GetComponent<RoomManagerService>();
        roomManagerService.GenerateDungeon();
    }
}
