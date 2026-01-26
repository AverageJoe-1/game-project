using System;
using System.Collections.Generic;
using System.Linq;

public class Node
{
    public (int, int) position;
    public List<Node> edges = new();
    public List<string> tags = new();

    public Node((int, int) nodePosition)
    {
        position = nodePosition;
    }

    public void connect(Node node, bool reverse = true)
    {
        if(node is not null){
            if (!edges.Contains(node))
            {
                edges.Add(node);
            }

            if (reverse)
            {
                node.connect(this, false);
            }
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

    public Node[] getNeighbours(Graph graph)
    {
        (int, int)[] positions = { (position.Item1+1, position.Item2), 
                                    (position.Item1, position.Item2+1), 
                                    (position.Item1-1, position.Item2), 
                                    (position.Item1, position.Item2-1)};

        List<Node> neighbours = new() {};

        for(int i = 0; i < 4; i++)
        {
            if (graph.GetNode(positions[i]) is not null)
            {
                neighbours.Add(graph.GetNode(positions[i]));
            }
            
        }

        return neighbours.ToArray();
    }

    public void connectNeighbours(Graph graph)
    {
        Node[] neighbours = getNeighbours(graph);
        
        foreach(Node neighbour in neighbours)
        {

            connect(neighbour); 

        }
    }

    public Node step(Graph graph, Random random)
    {
        (int, int)[] positions = { (position.Item1+1, position.Item2), 
                                    (position.Item1, position.Item2+1), 
                                    (position.Item1-1, position.Item2), 
                                    (position.Item1, position.Item2-1)};
        positions = positions.OrderBy(x => random.Next()).ToArray();

        foreach((int, int) neighbour in positions)
        {
            if(graph.GetNode(neighbour) is null)
            {
                Node newNode = graph.addNode(neighbour);
                connect(newNode);
                return newNode;
                
            }
        }
        return null;
    }
}