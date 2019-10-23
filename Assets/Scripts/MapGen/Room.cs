using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public enum Type
    {
        kStart,
        kCombat,
        kLoot,
        kShop,
        kCampfire,
        kTavern,
        kBlackSmith,
        kChurch,
        kBoss,
        kMystery,
        kCount

    }

    [SerializeField]
    private List<Sprite> m_sprites;

    private Dictionary <Direction, Room> m_doors = new Dictionary<Direction, Room>();


    [SerializeField]
    private Type m_type;
    private Vector2 m_position;


    [SerializeField]
    private GameObject m_doorPrefab;

    [SerializeField]
    private SpriteRenderer m_inside;

    [SerializeField]
    private float doorPosition = 0.468f;

    [SerializeField]
    private List<Direction> m_availableConnections;

    [SerializeField]
    private int m_distanceFromStart = 0;

    public Type type { get => m_type; set => m_type = value; }
    public Vector2 Position { get => m_position; set => m_position = value; }

    public List<Direction> AvailableConnections { get => m_availableConnections; set => m_availableConnections = value; }
    public int DistanceFromStart { get => m_distanceFromStart; set => m_distanceFromStart = value; }
    public Dictionary<Direction, Room> Doors { get => m_doors; set => m_doors = value; }

    public float GetWeight()
    {
        return AvailableConnections.Count;
    }

    public void SetPosition(Vector2 target)
    {
        transform.position = target;

        if (transform.position.x > 4)
        {
            AvailableConnections.Remove(Direction.kRight);
        }
        if (transform.position.y > 3)
        {
            AvailableConnections.Remove(Direction.kUp);
        }
        if (transform.position.y < -3)
        {
            AvailableConnections.Remove(Direction.kDown);
        }
        if (transform.position.x < -4)
        {
            AvailableConnections.Remove(Direction.kLeft);
        }

    }


    public void SetRoom(Type type)
    {
        m_type = type;

        switch (type)
        {
            case Type.kStart:
                m_inside.color = Color.black;
                break;
            case Type.kBoss:
 
                m_inside.sprite = m_sprites[0];
                break;
            case Type.kChurch:
                  m_inside.sprite = m_sprites[7];
                break;
            case Type.kBlackSmith:
                m_inside.sprite = m_sprites[4];
                break;
            case Type.kCampfire:
                m_inside.sprite = m_sprites[8];
                break;
            case Type.kCombat:
                m_inside.sprite = m_sprites[1];
                break;
            case Type.kLoot:
                m_inside.sprite = m_sprites[2];
                break;
            case Type.kShop:
                m_inside.sprite = m_sprites[3];
                break;
            case Type.kTavern:
                m_inside.sprite = m_sprites[5];
                break;
            case Type.kMystery:
                m_inside.sprite = m_sprites[6];
                break;
        }
    }

    public void CreateDoor(Direction dir, Room room)
    {
        GameObject door =Instantiate(m_doorPrefab, transform);

        m_doors.Add(dir, room);

        switch (dir)
        {
            case Direction.kUp:
                door.transform.localPosition = Vector2.up * doorPosition;
           
                break;
            case Direction.kDown:
                door.transform.localPosition = -Vector2.up * doorPosition;

                break;
            case Direction.kRight:
                door.transform.localPosition = Vector2.right * doorPosition;
                door.transform.localEulerAngles = new Vector3(0, 0,90);
                break;
            case Direction.kLeft:
                door.transform.localPosition = -Vector2.right * doorPosition;
                door.transform.localEulerAngles = new Vector3(0, 0, 90);

                break;
        }
    }
}
