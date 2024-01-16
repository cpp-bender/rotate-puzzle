using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelBrickData
{
    public BaseBrickController parentBrick = null;
    public BrickRotation initialRotation = BrickRotation.Up;
    public BrickRotation nextRotation = BrickRotation.Right;
    public Material material = default(Material);
    public int gridIndex = 0;

    public List<LevelBrickData> children = null;
}
