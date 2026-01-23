using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class MapConstructor : MonoBehaviour
{   
    public List<Room> rooms = new();
    
    public List<GameObject> type0Prefabs;
    public List<GameObject> type1Prefabs;
    public List<GameObject> type2Prefabs;
    public List<GameObject> type3Prefabs;
    public List<GameObject> type4Prefabs;
    public List<GameObject> type5Prefabs;
    public GameObject map;

    public void Construct(Graph graph, int seed)
    {
        // Create map as new gameobject
        Instantiate(map, transform);

        // Create new random generator from old seed
        System.Random random = new System.Random(seed);

        // Create rooms
        foreach (Node room in graph.nodes)
        {
            rooms.Add(new Room(room));
        }

        // Assign random prefabs
        foreach (Room room in rooms)
        {
            switch (room.type)
            {
                case 0:
                    room.prefab = type0Prefabs[random.Next(type0Prefabs.Count)];
                    break;
                case 1:
                    room.prefab = type1Prefabs[random.Next(type1Prefabs.Count)];
                    break;
                case 2:
                    room.prefab = type2Prefabs[random.Next(type2Prefabs.Count)];
                    break;
                case 3:
                    room.prefab = type3Prefabs[random.Next(type3Prefabs.Count)];
                    break;
                case 4:
                    room.prefab = type4Prefabs[random.Next(type4Prefabs.Count)];
                    break;
                case 5:
                    room.prefab = type5Prefabs[random.Next(type5Prefabs.Count)];
                    break;
            }
        }

        // Instantiate rooms
        foreach (Room room in rooms)
        {
            room.Spawn(25, gameObject.transform.GetChild(0).transform);
        }
    }
}
