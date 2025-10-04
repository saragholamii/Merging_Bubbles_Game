using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "MergingFruit/FruitData")]
public class FruitDataList : ScriptableObject
{
    public List<FruitData> fruitsDataList = new List<FruitData>();
}

[Serializable]
public class FruitData
{
    public string fruitName;
    public Sprite sprite;
    public Vector2 size = Vector2.one;
    public int score;
    public int level; // 0 = lowest, increasing upward
}