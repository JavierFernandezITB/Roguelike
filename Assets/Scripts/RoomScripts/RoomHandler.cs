using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : ServicesReferences
{
    public bool isRoomCompleted = false;
    public int enemiesRemaining = 0;
    public bool isKeyDropped = false;
    public GameObject keyPrefab;

    private void Update()
    {
        if (enemiesRemaining <= 0 && !isKeyDropped)
        {
            isKeyDropped = true;
            GameObject key = Instantiate(keyPrefab);
            key.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }

    public void OpenAllDoors()
    {
        if (isRoomCompleted)
        {
            OpenTopDoor();
            OpenBottomDoor();
            OpenRightDoor();
            OpenLeftDoor();
        }
    }

    public void CloseAllDoors()
    { 
        CloseTopDoor();
        CloseBottomDoor();
        CloseRightDoor();
        CloseLeftDoor();
    }

    public void OpenTopDoor()
    {
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
    }

    public void CloseTopDoor()
    {
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OpenBottomDoor()
    {
        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
    }

    public void CloseBottomDoor()
    {
        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
    }

    public void OpenRightDoor()
    {
        transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(false);
    }

    public void CloseRightDoor()
    {
        transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true);
    }

    public void OpenLeftDoor()
    {
        transform.GetChild(0).transform.GetChild(6).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(7).gameObject.SetActive(false);
    }

    public void CloseLeftDoor()
    {
        transform.GetChild(0).transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(7).gameObject.SetActive(true);
    }
}
