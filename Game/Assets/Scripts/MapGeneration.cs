using PurrNet;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MapGeneration : NetworkBehaviour
{
    public int seed;
    public MapConstructor constructor;

    private void Start()
    {
        if (isHost)
        {
            //
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
    private void generateMap(int seed, int width = 5, int height = 5, int holes = 5)
    {
        System.Random random = new System.Random(seed);
        int test = random.Next();
        Debug.Log(test);

        Graph graph = new Graph((width, height));

        

        for(int i = 0; i < holes; i++)
        {   
            int index = random.Next(graph.nodes.Count)-1;
            graph.removeNode(index);
        }

        Graph fullGraph = graph;
        fullGraph.connectGraph();

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
        
        constructor.Construct(graph, random.Next());
    }

}

