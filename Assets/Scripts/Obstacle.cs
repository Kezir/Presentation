using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Obstacle
{
    [Range(1,2)]
    public int size;
    public GameObject prefab;
}
