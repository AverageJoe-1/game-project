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

    public List<GameObject> type0CentrePrefabs;
    public List<GameObject> type1CentrePrefabs;
    public List<GameObject> type2CentrePrefabs;
    public List<GameObject> type3CentrePrefabs;
    public List<GameObject> type4CentrePrefabs;
    public List<GameObject> type5CentrePrefabs;

    public List<GameObject> type0ExtractPrefabs;
    public List<GameObject> type1ExtractPrefabs;
    public List<GameObject> type2ExtractPrefabs;
    public List<GameObject> type3ExtractPrefabs;
    public List<GameObject> type4ExtractPrefabs;
    public List<GameObject> type5ExtractPrefabs;

    
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
            // Plain Rooms
            if(room.tags.Count == 0){
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
            // Centre rooms
            else if (room.tags.Contains("Centre"))
            {
                switch (room.type)
                {
                    case 0:
                        room.prefab = type0CentrePrefabs[random.Next(type0CentrePrefabs.Count)];
                        break;
                    case 1:
                        room.prefab = type1CentrePrefabs[random.Next(type1CentrePrefabs.Count)];
                        break;
                    case 2:
                        room.prefab = type2CentrePrefabs[random.Next(type2CentrePrefabs.Count)];
                        break;
                    case 3:
                        room.prefab = type3CentrePrefabs[random.Next(type3CentrePrefabs.Count)];
                        break;
                    case 4:
                        room.prefab = type4CentrePrefabs[random.Next(type4CentrePrefabs.Count)];
                        break;
                    case 5:
                        room.prefab = type5CentrePrefabs[random.Next(type5CentrePrefabs.Count)];
                        break;
                }
            }
            // Extract rooms
            else if (room.tags.Contains("Extract"))
            {
                switch (room.type)
                {
                    case 0:
                        room.prefab = type0ExtractPrefabs[random.Next(type0ExtractPrefabs.Count)];
                        break;
                    case 1:
                        room.prefab = type1ExtractPrefabs[random.Next(type1ExtractPrefabs.Count)];
                        break;
                    case 2:
                        room.prefab = type2ExtractPrefabs[random.Next(type2ExtractPrefabs.Count)];
                        break;
                    case 3:
                        room.prefab = type3ExtractPrefabs[random.Next(type3ExtractPrefabs.Count)];
                        break;
                    case 4:
                        room.prefab = type4ExtractPrefabs[random.Next(type4ExtractPrefabs.Count)];
                        break;
                    case 5:
                        room.prefab = type5ExtractPrefabs[random.Next(type5ExtractPrefabs.Count)];
                        break;
                }
            }
        }

        // Instantiate rooms
        foreach (Room room in rooms)
        {
            room.Spawn(25, gameObject.transform.GetChild(0).transform);
        }
    }
}
