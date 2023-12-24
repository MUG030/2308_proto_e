using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TrapData
{
    public string name;
    public GameObject trap;
    public int level;//障害物の難易度
    public int size;//罠の大きさ(タイル一マスの大きさが1)
    public float RebuildDistance;//障害物を再配置することができる最低限の距離
}
