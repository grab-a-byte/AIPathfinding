using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{

    Node startNode;
    Node endNode;

    List<Node> openSet;
    List<Node> closedSet;

    Node lastNodeEval;
    string pathway;

    Graph graphThisBelongTo;


    public AStar()
    {
        graphThisBelongTo = null;
        startNode = null;
        endNode = null;
        openSet = new List<Node>();
        closedSet = new List<Node>();
    }
    public AStar(Graph thisGraph, Vec2 start, Vec2 End)
    {
        graphThisBelongTo = thisGraph;
        startNode = graphThisBelongTo.GetNodeAt(start.x, start.y);
        startNode.Type = NodeType.START;
        endNode = graphThisBelongTo.GetNodeAt(End.x, End.y);
        endNode.Type = NodeType.END;

        lastNodeEval = startNode;
        openSet = new List<Node>();
        closedSet = new List<Node>();
        pathway = "";

        openSet.Add(startNode);
    }

    public void RunAstar()
    {
        while (openSet.Count > 0)
        {
            if (StepForward())
            {
                break;
            }
        }

        Debug.Log(pathway);
    }
    public bool StepForward()
    {
        if(openSet.Count <= 0)
        {
            return true;
        }

        //find lowest f value
        int lowestIndex = 0;

        for (int i = 0; i < openSet.Count; i++)
        {
            if (openSet[i].fValue < openSet[lowestIndex].fValue || openSet[i].fValue == openSet[lowestIndex].fValue)
            {
                if (openSet[i].heuristicValue < openSet[lowestIndex].heuristicValue)
                {
                    lowestIndex = i;
                }
            }
        }
        Node current = openSet[lowestIndex];

        if (current == endNode)
        {
            lastNodeEval = current;
            return true;
        }

        //Move node between sets as it has been checked. 

        //Debug.Log(current.posX + "," + current.posY);
        lastNodeEval = current;
        openSet.Remove(current);
        closedSet.Add(current);

            for (int x = 0; x < current.neighbours.Count; x++)
            {

                //Check to see if it is in closed list or if it is a wall.
                if (!closedSet.Contains(current.neighbours[x]) && current.neighbours[x].Type != NodeType.WALL)
                {
                    Vec2 currentPos = new Vec2(current.posX, current.posY);
                    Vec2 neighbourPos = new Vec2(current.neighbours[x].posX, current.neighbours[x].posY);
                    var tempDistance = current.distanceFromStart + Vec2.ManhattanDistance(currentPos, neighbourPos);

                    //Check if in open set and add / check to see if shorter
                    if (openSet.Contains(current.neighbours[x]))
                    {
                        if (tempDistance < current.neighbours[x].distanceFromStart)
                        {
                            current.neighbours[x].distanceFromStart = tempDistance;
                            current.neighbours[x].previousNode = current;
                        }
                    }

                    else
                    {
                        current.neighbours[x].distanceFromStart = tempDistance;
                        current.neighbours[x].previousNode = current;
                        openSet.Add(current.neighbours[x]);
                    }

                    //calculate heuristic anf f value
                    Vec2 end = new Vec2(endNode.posX, endNode.posY);
                    current.neighbours[x].heuristicValue = Vec2.ManhattanDistance(neighbourPos, end);
                    current.neighbours[x].fValue = current.neighbours[x].heuristicValue + current.neighbours[x].distanceFromStart;

                }
            
        }

        return false;
    }
    public Node GetLastEvaluatedNode() { return lastNodeEval; }
    public bool OpenSetContains(Node contains) { return openSet.Contains(contains); }
    public bool ClosedSetContains(Node contains) { return closedSet.Contains(contains); }
}
