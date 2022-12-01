using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public GameObject ItemBasePrefab;
    public int DropChance;

    System.Random random = new System.Random();


    public GameObject Drop()
    {
        int rand = random.Next(0, 100);
        if (rand <= DropChance)
        {
            //Debug.Log(rand);
            //GameObject obj;
            //obj = GameObject.Find(item.name + "ObjectPool").GetComponent<ObjectPool>().GetPooledObject();
            //Debug.Log(obj);
            //return obj;
        }
        return null;

    }
}
