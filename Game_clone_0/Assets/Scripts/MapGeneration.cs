using PurrNet;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

public class MapGeneration : NetworkBehaviour
{
    int seed;

    private void Start()
    {
        if (isHost)
        {
            seed = UnityEngine.Random.Range(0, 99999);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            generateMap(seed);
        }
    }

    [ContextMenu("Generate Map")]
    private void manualGenerate()
    {
        generateMap(seed);
    }

    [ObserversRpc]
    private void generateMap(int seed, int width = 5, int height = 5, int holes = 3)
    {
        Debug.Log("Generate Map");
        System.Random random = new System.Random(seed);
        int test = random.Next();
        Debug.Log(test);

        Debug.Log("Blank Graph");
        Graph graph = new Graph((width, height));

        Debug.Log("Connect Graph");
        Graph fullGraph = graph;
        fullGraph.connectGraph();

        Debug.Log("Remove Nodes");
        for(int i = 0; i < holes; i++)
        {   
            int index = random.Next(graph.nodes.Count);
            graph.removeNode(index);
            fullGraph.removeNode(index);
        }

        Debug.Log("Final");
        List<(Node, Node)> edges = fullGraph.edges();
        edges = edges.OrderBy(x => random.Next()).ToList();
        
        // Randomised Kruskal's Algorithm for maze generation        
        foreach ((Node, Node) connection in edges)
        {
            Node start = graph.GetNode(connection.Item1.position);
            Node end = graph.GetNode(connection.Item2.position);

            if (!graph.findPath(start, end))
            {
                start.connect(end);
            }
        }
        
    }

}

