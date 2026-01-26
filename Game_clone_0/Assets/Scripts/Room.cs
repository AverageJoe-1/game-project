using UnityEngine;
using System.Collections.Generic;

public class Room
{
    public byte connections;
    public byte type;
    public int rotation;
    public (int, int) position;
    public List<string> tags = new();
    public Vector3 location;
    public GameObject prefab;
    private (byte, int)[] roomTypes =
        {
        (0, 0), (1, 0), (1, 270), (2, 0),
        (1, 180), (3, 0), (2, 270), (4, 0),
        (1, 90), (2, 90), (3, 90), (4, 90),
        (2, 180), (4, 180), (4, 270), (5, 0),
        };
    
    public Room(Node node)
    {
        position = node.position;
        location = new Vector3(node.position.Item1, node.position.Item2);
        tags = node.tags;

        foreach (Node connection in node.edges)
        {
            // Top connection
            if (connection.position == (position.Item1, position.Item2+1))
            {
                connections += 1;
            }
            
            // Right connections    
            else if (connection.position == (position.Item1+1, position.Item2))
            {
                connections += 2;
            }

            // Bottom connection
            else if (connection.position == (position.Item1, position.Item2-1))
            {
                connections += 4;
            }

            // Left connection
            else if (connection.position == (position.Item1-1, position.Item2))
            {
                connections += 8;
            }
        }
        
        type = roomTypes[connections].Item1;
        rotation = roomTypes[connections].Item2;
        
        // debug construct per room
        // Debug.Log(position.ToString()+ " " + type.ToString()+ " " + rotation.ToString());
        
    }

    public void Spawn(float distance, Transform map)
    {
        Object.Instantiate(prefab, location*distance, Quaternion.AngleAxis(rotation, Vector3.forward), map);
    }
}
