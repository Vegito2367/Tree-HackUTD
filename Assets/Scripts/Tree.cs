using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tree",menuName ="Tree")]
public class Tree : ScriptableObject
{
    public TreeType type;
    public Sprite[] TreeSprite;
    public float SeedDropRate; //Measured in seeds per second, 
    public float SeedQuantity; //Function of Age
    public int SeedCost;

    public float midAge;
    public float LastAge;




}
