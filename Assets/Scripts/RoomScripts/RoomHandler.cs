using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomHandler : ServicesReferences
{
    public UnityEvent onOpenAllDoors;
    public UnityEvent onOpenTopDoor;
    public UnityEvent onOpenBottomDoor;
    public UnityEvent onOpenRightDoor;
    public UnityEvent onOpenLeftDoor;

    public bool isRoomCompleted = false;
    public int enemiesToSpawn = 5;
    public int maxEnemiesInRoom = 4;
    public int enemiesRemaining = 0;
    public bool isKeyDropped = false;
    public GameObject keyPrefab;
    public List<GameObject> enemiesPool;
    public List<GameObject> spawnedEnemiesPool;
    public List<GameObject> spawnPoints;
    public RunManager runManager;
    public RoomManagerService roomManagerService;

    private void Start()
    {
        roomManagerService = GameObject.Find("/RoomManagerService").GetComponent<RoomManagerService>();
        runManager = GameObject.Find("/RunManager").GetComponent<RunManager>();
        runManager.roomsInCurrentRun.Add(this);
        enemiesPool = new List<GameObject>();
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject newEnemy = Instantiate(runManager.availableEnemyList[Random.Range(0, runManager.availableEnemyList.Count)]);
            newEnemy.GetComponent<EnemyStateManager>().currentRoomHandler = this;
            newEnemy.SetActive(false);
            enemiesPool.Add(newEnemy);
            enemiesRemaining += 1;
        }
    }

    private void Update()
    {
        if (enemiesRemaining <= 0 && !isKeyDropped && !runManager.isRunCompleted)
        {
            isKeyDropped = true;
            GameObject key = Instantiate(keyPrefab);
            key.transform.position = new Vector2(transform.position.x, transform.position.y);
        }

        if (spawnedEnemiesPool.Count < maxEnemiesInRoom && gameObject == roomManagerService.GetCurrentRoomObject() && !isRoomCompleted)
        {
            foreach (GameObject enemy in enemiesPool)
            {
                EnemyStateManager esm = enemy.GetComponent<EnemyStateManager>();
                enemiesPool.Remove(enemy);
                spawnedEnemiesPool.Add(enemy);
                enemy.SetActive(true);
                enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
            }
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
