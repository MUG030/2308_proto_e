using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Foodのデータベース
/// </summary>
[CreateAssetMenu]
public class TrapDataBase : ScriptableObject
{
    public List<TrapData> trapDatas = new List<TrapData>();
}
