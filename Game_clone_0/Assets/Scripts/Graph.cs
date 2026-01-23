using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph
{
    public List<Node> nodes = new();

    // Grid shaped graph
    public Graph((int, int) dimensions)
    {
        for (int i = 0; i < dimensions.Item1 * dimensions.Item2; i++)
        {
            (int, int) position = (i % dimensions.Item1, i / dimensions.Item1);
            nodes.Add(new Node(position));
        }
    }

    public void connectGraph()
    {
        foreach (Node node in nodes)
        {
            node.connectNeighbours(this);
        }
    }

    public void removeNode(Node node)
    {
        nodes.Remove(node);
        foreach (Node copy in nodes.ToList())
        {
            copy.edges.Remove(node);
        }
    }

    public void removeNode(int node)
    {   
        Node remove = nodes[node];
        removeNode(remove);
    }

    // Get node by position
    public Node GetNode((int, int) position)
    {
        foreach (Node node in nodes)
        {
            if (node.position == position)
            {
                return node;
            }
        }
        return null; 
    
    }

    public void step(Node node, Graph graph)
    {
    }

    // Breadth-first search algorithm, eturns true if there is a path from start to end
    public bool findPath(Node start, Node end)
    {
        List<Node> found = new List<Node>{start};

        int size = 1;
        int prevsize = 0;
        
        // Iterate through list, adding all neighbours not in list
        while (size > prevsize)
        {   
            for(int i = 0; i < size; i++)
            {   
                if (found[i] == null) return true;

                if (found[i].edges == null) UnityEngine.Debug.Log("edges is null for node");
                foreach(Node child in found[i].edges)
                {
                    // If end node is found, return true
                    if (child == end)
                    {
                        return true;
                    }
                    else if(!found.Contains(child))
                    {
                        found.Add(child);
                    }
                }
            }
            // If no new neighbours are found, graph has been fully explored, return false
            prevsize = size;
            size = found.Count;
        }
        return false;
    }

    public Node randomNode(int seed)
    {
        Node random = null;

        while (random is null)
        {
            UnityEngine.Random.InitState(seed);
            random = nodes[UnityEngine.Random.Range(0, nodes.Count)];
        }
        return random;
    }

    public List<(Node, Node)> edges()
    {
        List<(Node, Node)> edges = new List<(Node, Node)>();
   
        foreach(Node node in nodes)
        {
            if (node == null) continue;

            foreach( Node connection in node.edges)
            {   
                if (connection == null) continue;

                // Only add edges if first is greater than second, to prevent reversed duplicates
                if (node.position.Item1 * 10 + node.position.Item2 >
                connection.position.Item1 * 10 + connection.position.Item2)
                {
                    edges.Add((node, connection));
                }
            }
        }

        return edges;
    }
}