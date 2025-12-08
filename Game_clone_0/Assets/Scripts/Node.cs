using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Node
{
    public (int, int) position;
    public List<Node> edges = new List<Node>();

    public Node((int, int) nodePosition)
    {
        position = nodePosition;
    }

    public void connect(Node node, bool reverse = true)
    {
        Debug.Log(position.ToString() + " Connect " + node.position.ToString());
        

        if (!edges.Contains(node))
        {
            edges.Add(node);
        }

        if (reverse)
        {
            node.connect(this, false);
        }
        
    }

    public void disconnect(Node node, bool reverse = true)
    {
        edges.Remove(node);
        if (reverse)
        {
            node.disconnect(this, false);
        }
    }

    public void connectNeighbours(Graph graph)
    {
        (int, int)[] neighbours = {(position.Item1+1, position.Item2), 
                            (position.Item1, position.Item2+1), 
                            (position.Item1-1, position.Item2), 
                            (position.Item1, position.Item2-1)};
        

        foreach((int, int) neighbour in neighbours)
        {
            if (graph.GetNode(neighbour) != null)
            {
                connect(graph.GetNode(neighbour)); 
            }

        }
    }
}