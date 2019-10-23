
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    kUp,
    kDown,
    kLeft,
    kRight,
    kCount
}


public class MapGenerator : MonoBehaviour {

    [SerializeField]
    List <Room.Type>  availabeRooms;

 

    [SerializeField]
    private int totalRoom = 13;

    private int roomCount;

    [SerializeField]
    private Room m_roomPrefab;

    private Vector2 startRoomPos = Vector2.zero;

    private int maxLength = 5;

    [SerializeField]
    private Room[,] m_rooms;


    [SerializeField]
    private List<Room> m_availableRoomList = new List<Room>();


    private void Awake()
    {
        m_rooms = new Room[maxLength * 2 + 1, maxLength * 2 + 1];
        GenerateRooms();
    }

    public void GenerateRooms()
    {
        Room startingRoom = Instantiate(m_roomPrefab, transform);

        startingRoom.SetPosition(startRoomPos);

        startingRoom.SetRoom(Room.Type.kStart);

        m_rooms[5, 5] = startingRoom;

        m_availableRoomList.Add(startingRoom);
        CreateRoom(startingRoom);
        

    }
    public void CreateRoom(Room currentRoom)
    {
 
            Vector2 targetPos = Vector2.zero;
           
            Direction dir = GetRandomDirection(currentRoom);


            int length = maxLength;
            while (m_availableRoomList.Count < totalRoom)
            {
                 Room room = GetRandomRoom();

                 dir = GetRandomDirection(room);

                 CreateRoomAtDirection(room, dir, length);
        
            }


        SetRoom();
    }

    private void SetRoom()
    {   
        int biggestDistance = 0;
        Room room = m_availableRoomList[0];

        for (int i = 1; i < m_availableRoomList.Count; i++)
        {
            if (m_availableRoomList[i].DistanceFromStart > biggestDistance)
            {
                biggestDistance = m_availableRoomList[i].DistanceFromStart;
                room = m_availableRoomList[i];
            }

        }

        room.SetRoom(Room.Type.kBoss);
        m_availableRoomList.Remove(room);

        foreach (var key in room.Doors)
        {
            key.Value.SetRoom(Room.Type.kCampfire);
            m_availableRoomList.Remove(key.Value);
        }

        while (availabeRooms.Count > 0)
        {
            int rand = Random.Range(0, availabeRooms.Count);
            int index = Random.Range(2, m_availableRoomList.Count);
            m_availableRoomList[index].SetRoom(availabeRooms[rand]);
            availabeRooms.RemoveAt(rand);

            m_availableRoomList.Remove(m_availableRoomList[index]);
       
            
        }

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(2, m_availableRoomList.Count);
            m_availableRoomList[index].SetRoom((Room.Type)Random.Range(2, 7));

            m_availableRoomList.Remove(m_availableRoomList[index]);
        }

        foreach (var item in m_availableRoomList)
        {
            if(item.type == Room.Type.kStart)
            {
                item.SetRoom(Room.Type.kCombat);
            }

        }


    }

    private Room GetRandomRoom()
    {
        Room room = m_availableRoomList[Random.Range(0, m_availableRoomList.Count)];
        while (room.AvailableConnections.Count == 0)
        {
            room = m_availableRoomList[Random.Range(0, m_availableRoomList.Count)];
        }
      
        return room;
    }

    private void CreateRoomAtDirection(Room currentRoom, Direction dir, int amount)
    {
        Vector2 targetPos = Vector2.zero;

        Vector2 pos = currentRoom.transform.position;

        switch (dir)
        {
            case Direction.kUp:
                targetPos = pos + Vector2.up;          

                break;
            case Direction.kDown:
                targetPos = pos - Vector2.up;

             
                break;
            case Direction.kRight:
                targetPos = pos + Vector2.right;

                break;
            case Direction.kLeft:
                targetPos = pos - Vector2.right;
             
                break;
        }

        if(m_rooms[5 + (int)targetPos.x, 5 + (int)targetPos.y] != null)
        {
            roomCount = 0;
            return;
        }

        currentRoom.AvailableConnections.Remove(dir);


        Room temp = Instantiate(m_roomPrefab, transform);

        currentRoom.CreateDoor(dir, temp);

        temp.AvailableConnections.Remove(GetOppositeDirection(dir));

        temp.CreateDoor(GetOppositeDirection(dir), currentRoom);

        temp.SetPosition(targetPos);

        temp.DistanceFromStart = currentRoom.DistanceFromStart + 1;


        m_rooms[5 + (int)targetPos.x, 5 + (int)targetPos.y] = temp;
        m_availableRoomList.Add(temp);

        currentRoom = temp;

        roomCount++;

        if (roomCount < amount - 1 && m_availableRoomList.Count < totalRoom && currentRoom.AvailableConnections.Contains(dir))
        {
            CreateRoomAtDirection(currentRoom, dir, amount);
        }
        else
        {
            roomCount = 0;
        }
  
    }

  


    private Direction GetRandomDirection(Room currentRoom)
    {
        return currentRoom.AvailableConnections[Random.Range(0, currentRoom.AvailableConnections.Count)];
    }

  

    private Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.kUp:
                return Direction.kDown;

            case Direction.kDown:
                return Direction.kUp;
            
            case Direction.kRight:
                return Direction.kLeft;

            case Direction.kLeft:
                return Direction.kRight;
            default:
                Debug.Log("Direction not found");
                return Direction.kCount;
        }

    }
}
