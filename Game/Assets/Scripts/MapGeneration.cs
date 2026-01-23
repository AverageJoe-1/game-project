using PurrNet;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MapGeneration : NetworkBehaviour
{
    public int seed = 0;
    public int width = 5;
    public int height = 5;
    public int holes = 4;
    public int shortcuts = 10;
    
    public MapConstructor constructor;

    private void Start()
    {
        if (isHost)
        {

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateArgs();
        }
    }

    [ContextMenu("Generate Map")]
    private void generateArgs()
    {
        seed = (int)(Random.value*int.MaxValue);
        deleteMap();
        generateMap(seed, width, height, holes, shortcuts);
    }

    [ContextMenu("Delete")]
    private void deleteMap()
    {
        if(gameObject.transform.childCount > 0){
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        constructor.rooms = new();
    }

    [ObserversRpc]
    private void generateMap(int seed, int width, int height, int holes, int shortcuts)
    {
        System.Random random = new System.Random(seed);

        int test = random.Next();
        Debug.Log(test);

        Graph graph = new Graph((width, height));
        Graph fullGraph = new Graph((width, height));
        fullGraph.connectGraph();

        for(int i = 0; i < holes; i++)
        {   
            Node node = graph.randomNode(random.Next());
            graph.removeNode(node);
            fullGraph.removeNode(node);
        }

        List<(Node, Node)> edges = fullGraph.edges();
        // Random order list of edges
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

        for(int i = 0; i < shortcuts; i++)
        {   
            Node node = graph.randomNode(random.Next());
            Node[] neighbours = node.getNeighbours(graph).OrderBy(x => random.Next()).ToArray();
            foreach(Node neighbour in neighbours)
            {
                if (!node.edges.Contains(neighbour))
                {
                    node.connect(neighbour);
                    Debug.Log("shortcut made: "+ node.position.ToString() + neighbour.position.ToString());
                    break;
                }
            }
        }
        constructor.Construct(graph, random.Next());
    }

}

