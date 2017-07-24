using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vec2 {

    public Vec2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Vec2()
    {
        this.x = 0;
        this.y = 0;
    }

    public int x;
    public int y;

    public static float PythagorasDistance(Vec2 current, Vec2 End)
    {
        float distance = 0;

        int x = current.x - End.x;
        x *= x;

        int y = current.y - End.y;
        y *= y;

        distance = (float)Math.Sqrt(x + y);

        return distance;
    }

    public static float ManhattanDistance(Vec2 current, Vec2 End)
    {
        float distance = 0;

        int x = current.x - End.x; if (x < 0) { x = -x; }
        int y = current.y - End.y; if (y < 0) { y = -y; }

        distance = x + y;

        return distance;
    }

}