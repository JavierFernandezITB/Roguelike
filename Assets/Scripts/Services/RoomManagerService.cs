using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomManagerService : ServicesReferences
{
    public GameObject startingRoomPrefab;
    public GameObject[] roomPrefabs;

    public int difficultyMultiplier = 1;

    public Vector2Int currentPlayerRoom;

    public int maxRoomCount = 5;
    public int minRoomCount = 2;
    private int roomCount;
    private int pathCount;

    private int xGridSize = 40;
    private int yGridSize = 40;
    private GameObject[,] roomGrid;

    private Vector2Int gridCenter;
    private Vector2Int currentRoom;
    private Vector2Int lastRoomPosition;
    private int lastDirection;
    private int roomDirection;

    void Awake()
    {
        base.Persist<RoomManagerService>();
    }

    void Start()
    {
        //GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        // Lo ponemos todo por defecto. (Un poco xd, pero se buggea la generación si no lo hago. No se cual es el problema, pero no quiero reescribir esto.)

        maxRoomCount = 5 * difficultyMultiplier;
        
        minRoomCount = 2 * difficultyMultiplier;

        roomCount = 0;

        pathCount = 0;

        roomGrid = null;

        gridCenter = new Vector2Int(0,0);

        currentRoom = new Vector2Int(0, 0);

        lastRoomPosition = new Vector2Int(0, 0);

        lastDirection = 0;

        roomDirection = 0;

        // Preparamos todo.
        InitializeRoomGeneration();

        // Las veces que queremos generar a partir del centro.
        pathCount = Random.Range(1, 5);
        for (int i = 0; i < pathCount; i++)
            GenerateRooms();

        CloseDoors();
    }

    private void InitializeRoomGeneration()
    {
        // Según el nivel de dificultad, generamos más salas.

        gridCenter = new Vector2Int(xGridSize / 2, yGridSize / 2);
        roomGrid = new GameObject[xGridSize, yGridSize];

        currentPlayerRoom = gridCenter;

        roomGrid[gridCenter.x, gridCenter.y] = SpawnRoom(startingRoomPrefab, lastRoomPosition.x, lastRoomPosition.y);
    }

    private void GenerateRooms()
    {
        currentRoom = gridCenter;
        roomCount = Random.Range(minRoomCount, maxRoomCount);
        lastRoomPosition = Vector2Int.zero;
        Debug.Log($"Generating {roomCount} rooms.");

        for (int i = 0; i < roomCount; i++)
        {
            bool isGenerated = false;
            GameObject selectedRoom = GetRandomRoom();
            List<int> directionCache = new List<int> { 0, 1, 2, 3 };

            while (!isGenerated && directionCache.Count != 0)
            {
                roomDirection = directionCache[Random.Range(0, directionCache.Count)];

                if (lastDirection != roomDirection)
                {
                    isGenerated = TryGenerateRoomInDirection();

                    if (!isGenerated)
                        directionCache.Remove(roomDirection);
                }
                else
                { 
                    directionCache.Remove(roomDirection);
                }
            }
            if (directionCache.Count == 0)
                break;

            lastDirection = roomDirection;
        }
    }

    private bool TryGenerateRoomInDirection()
    {
        Vector2Int direction = GetDirectionVector(roomDirection);
        Vector2Int newRoomPosition = currentRoom + direction;

        if (IsValidPosition(newRoomPosition))
        {
            currentRoom = newRoomPosition;
            lastRoomPosition += new Vector2Int(direction.x * 18, direction.y * 10);
            roomGrid[currentRoom.x, currentRoom.y] = SpawnRoom(GetRandomRoom(), lastRoomPosition.x, lastRoomPosition.y);
            return true;
        }

        return false;
    }

    private Vector2Int GetDirectionVector(int direction)
    {
        switch (direction)
        {
            case 0: return Vector2Int.up; // 1
            case 1: return Vector2Int.down; // -1
            case 2: return Vector2Int.left; // -1
            case 3: return Vector2Int.right; // 1
            default: return Vector2Int.zero; // no debería salir.
        }
    }

    private bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < xGridSize && position.y >= 0 && position.y < yGridSize && roomGrid[position.x, position.y] == null;
    }

    private GameObject GetRandomRoom()
    {
        return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    }

    private GameObject SpawnRoom(GameObject roomPrefab, int positionX, int positionY)
    {
        return Instantiate(roomPrefab, new Vector2(positionX, positionY), Quaternion.identity);
    }

    public GameObject GetCurrentRoomObject()
    {
        return roomGrid[currentPlayerRoom.x, currentPlayerRoom.y];
    }

    public GameObject GetRoomFromCurrentOffset(Vector2Int offset)
    {
        try {
            return roomGrid[currentPlayerRoom.x + offset.x, currentPlayerRoom.y + offset.y];
        } catch (System.Exception e)
        {
            return null;
        }
    }

    private void CloseDoors()
    {
        for (int yAxis = 0; yAxis < yGridSize; yAxis++)
        {
            for (int xAxis = 0; xAxis < xGridSize; xAxis++)
            {
                if (roomGrid[xAxis, yAxis] != null)
                {
                    // Cierra las puertas que no llevan a ningun sitio.
                    if (roomGrid[xAxis, yAxis + 1] == null)
                    {
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
                    }
                    if (roomGrid[xAxis, yAxis - 1] == null)
                    {
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true);
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(5).gameObject.SetActive(true);
                    }
                    if (roomGrid[xAxis + 1, yAxis] == null)
                    {
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(6).gameObject.SetActive(true);
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(7).gameObject.SetActive(true);
                    }
                    if (roomGrid[xAxis - 1, yAxis] == null)
                    {
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(8).gameObject.SetActive(true);
                        roomGrid[xAxis, yAxis].transform.GetChild(1).transform.GetChild(9).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
