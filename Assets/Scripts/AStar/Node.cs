using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { NEUTRAL, WALL, START, END  }

public class Node {

    public int posX, posY;
    public float heuristicValue;
    public float distanceFromStart;
    public float fValue;

    public  List<Node> neighbours;
    public Node previousNode;
    private NodeType type;

    public Action onNodeTypeChanged;

    public NodeType Type
    {
        get
        {
            return type;
        }

        set
        {
            if (type == NodeType.START)
            { Debug.Log(value); }
            type = value;
        }
    }

    public Node()
    {
        posX = 0;
        posY = 0;
        heuristicValue = 0;
        distanceFromStart = 0;
        fValue = 0;
        type = NodeType.NEUTRAL;
        neighbours = new List<Node>();
    }

    public void SetPlace(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public void AddNeighbours(ref Node newNeighbour)
    {
        if(!neighbours.Contains(newNeighbour))
        {
            neighbours.Add(newNeighbour);
        }
    }

    public void Reset()
    {

        neighbours.Clear();
        neighbours.TrimExcess();

        heuristicValue = 0;
        distanceFromStart = 0;
        fValue = 0;
        Type = NodeType.NEUTRAL;
    }
}
