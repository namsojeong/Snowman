using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObjectPoolData
{
    public List<GameObject> prefabs = new List<GameObject>();

    public List<int> prefabsCounts = new List<int>();
}