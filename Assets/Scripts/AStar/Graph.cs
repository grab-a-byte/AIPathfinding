using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Graph {

    static int count = 0;

    Node[,] graph;

    public Graph()
    {
        
    }
    public void SetupNodes(int gridSize)
    {

        graph = new Node[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y<gridSize; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].SetPlace(x, y);
            }
        }

        SetNeighbours();
    }

    public int GetWidth() { return graph.GetLength(0); }
    public Node GetNodeAt(int x, int y) { return graph[x, y]; }

    public void RandomiseNodes(int percent)
    {
        System.Random myRand = new System.Random();

        foreach (Node n in graph)
        {
            if (myRand.Next(0, 100) < percent)
            {
                n.Type = NodeType.WALL;
            }
        }
    }
    private void ResetNodes()
    {

        foreach(Node n in graph)
        {
            n.Reset();
        }

        SetNeighbours();
    }
    private void SetNeighbours()
    {
        foreach (Node n in graph)
        {
            int size = graph.GetLength(0);

            //Add neighbour right
            if (n.posX < size - 1)
            {
                n.AddNeighbours(ref graph[n.posX + 1, n.posY]);
            }

            //add neighbout left
            if (n.posX > 0)
            {
                n.AddNeighbours(ref graph[n.posX - 1, n.posY]);
            }

            //add neighbour up
            if (n.posY > 0)
            {
                n.AddNeighbours(ref graph[n.posX, n.posY - 1]);
            }

            //add neighbour down
            if (n.posY < size - 1)
            {
                n.AddNeighbours(ref graph[n.posX, n.posY + 1]);
            }
            //add neighbout top left
            if (n.posX > 0 && n.posY < GetWidth()-1)
            {
                n.AddNeighbours(ref graph[n.posX - 1, n.posY + 1]);
            }
            //add neihbour top right
            if (n.posX < GetWidth()-1 && n.posY < GetWidth()-1)
            {
                n.AddNeighbours(ref graph[n.posX + 1, n.posY + 1]);
            }
            //add neihbour bottom right
            if (n.posX < GetWidth()-1 && n.posY > 0)
            {
                n.AddNeighbours(ref graph[n.posX + 1, n.posY -1]);
            }
            //add neihbour bottom left
            if (n.posX > 0 && n.posY > 0)
            {
                n.AddNeighbours(ref graph[n.posX -1, n.posY - 1]);
            }


        }
    }
}
