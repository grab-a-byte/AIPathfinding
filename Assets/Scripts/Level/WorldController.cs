using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WorldController : MonoBehaviour {

    float timechange;

    Graph myGraph;
    AStar myAStar;

    Vec2 start;
    Vec2 end;

    int num; //grid size
    int wallNum;

    bool Running = false;
    float timer;

    Dictionary<GameObject, Node> mappings;

    public Sprite PathSprite;
    public Sprite OpenSetSprite;
    public Sprite ClosedSetSprite;
    public Sprite StartSprite;
    public Sprite EndSprite;
    public Sprite WallSprite;
    public Sprite NothingSprite;


    public InputField startX;
    public InputField startY;
    public InputField endX;
    public InputField endY;
    public InputField size;
    public InputField walls;

    // Use this for initialization
    void Start () {
        myGraph = new Graph();
        mappings = new Dictionary<GameObject, Node>();
        myAStar = new AStar();
        start = new Vec2();
        end = new Vec2();
	}
	
    public void Setup()
    {
        start.x = int.Parse(startX.text);
        start.y = int.Parse(startY.text);

        end.x = int.Parse(endX.text);
        end.y = int.Parse(endY.text);

        num = int.Parse(size.text);
        wallNum = int.Parse(walls.text);

        myGraph.SetupNodes(num);
        myGraph.RandomiseNodes(wallNum);


        myAStar = new AStar(myGraph, start, end);

        SetupVisuals();
        Running = true;
    }

    public void UpdateTable()
    {
        int num = int.Parse(size.text);

        myGraph.SetupNodes(num);
        SetupVisuals();
    }

    public void runStepByStep()
    {
        Setup();
        Running = true;

        //rest handles in update due to nature of program
    }

    public void runOutright()
    {
        Setup();
        myAStar.RunAstar();
        UpdateVisuals();
        Running = false;
    }

    public void SetupVisuals()
    {

        //Clean up old Game Object
        var objects =  GameObject.FindGameObjectsWithTag("Node");
        foreach(GameObject go in objects)
        {
            Destroy(go);
        }

        mappings.Clear();

        // Make Visual Aspect
        for (int x = 0; x < myGraph.GetWidth(); x++)
        {
            for (int y = 0; y < myGraph.GetWidth(); y++)
            {
                GameObject node_go = new GameObject();

                node_go.name = "Node_" + x + "_" + y;
                node_go.tag = "Node";

                node_go.transform.position = new Vector3(x, y, 0);
                node_go.transform.parent = gameObject.transform;
                SpriteRenderer renderer = node_go.AddComponent<SpriteRenderer>();

                Node myNode = myGraph.GetNodeAt(x, y);

                mappings.Add(node_go, myNode);
            }
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for (int x = 0; x < mappings.Count; x++)
        {
            var pair = mappings.ElementAt(x);

            SpriteRenderer bob = pair.Key.GetComponent<SpriteRenderer>();

            NodeType type = pair.Value.Type;
            if (type == NodeType.START) { bob.sprite = StartSprite; }
            else if (type == NodeType.END) { bob.sprite = EndSprite; }
            else if (type == NodeType.WALL) { bob.sprite = WallSprite; }
            else if (myAStar.OpenSetContains(pair.Value)) { bob.sprite = OpenSetSprite; }
            else if (myAStar.ClosedSetContains(pair.Value)) { bob.sprite = ClosedSetSprite; }
            else
            {
                bob.sprite = NothingSprite;
            }
        }

        Node lastNodeEval = myAStar.GetLastEvaluatedNode();
        Node current = lastNodeEval;
        while(current != null && current.Type != NodeType.START)
        {
            foreach (KeyValuePair<GameObject, Node> pair in mappings)
            {
                if (pair.Value == current)
                {
                    pair.Key.GetComponent<SpriteRenderer>().sprite = PathSprite;
                    current = pair.Value.previousNode;
                }
            }
        }
    }

    private void Update()
    {
        if (Running) // if not done
        {
            timer += Time.deltaTime;
            if (timer > 0.2)
            {
                if (myAStar.StepForward()) { Running = false; }
                timer = 0.0f;
                UpdateVisuals();
            }
        }

        UpdateVisuals();
    }



}
