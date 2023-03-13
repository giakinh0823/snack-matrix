using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMatrix : MonoBehaviour
{
    public static int w = 10;
    public static int h = 10;
    public static int k = 1;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int x = -w; x <= w; x += k)
        {
            Point.Spawn(gameObject.transform, new Point(x, -h), new Point(x, h), Color.grey);
        }

        for (int y = -h; y <= h; y += k)
        {
            Point.Spawn(gameObject.transform, new Point(-w, y), new Point(w, y), Color.grey);
        }
    }
}
