using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Point
{
    private float _x;
    private float _y;

    public Point(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public static void Spawn(Transform parent, Point originPosition, Point endPosition, Color color)
    {
        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.transform.parent = parent.transform;
        lineRenderer.material.color = color;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.SetPosition(0, new Vector3(originPosition._x, originPosition._y, 0));
        lineRenderer.SetPosition(1, new Vector3(endPosition._x, endPosition._y, 0));
    }
}
