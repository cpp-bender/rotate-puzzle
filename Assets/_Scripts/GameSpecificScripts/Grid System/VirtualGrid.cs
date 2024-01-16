using UnityEngine;
using System;

[Serializable]
public class VirtualGrid
{
    public int x;
    public int z;
    public bool isEmpty;
    public Vector3 worldPos;

    public VirtualGrid(int x, int z, Vector3 worldPos, bool isEmpty)
    {
        this.x = x;
        this.z = z;
        this.worldPos = worldPos;
        this.isEmpty = isEmpty;
    }
}
