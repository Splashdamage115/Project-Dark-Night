using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemSpawn : MonoBehaviour
{
    [System.Serializable]
    public struct KeyItemsPair
    {
        public GameObject Item;
        public string SpawnTagName;
    }
    public KeyItemsPair[] KeyItems;
    void Start()
    {
        foreach (var keyItem in KeyItems)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag(keyItem.SpawnTagName);
            if (obj != null)
            {
                int chosen = Random.Range(0, obj.Length);
                Instantiate(keyItem.Item, obj[chosen].transform.position, obj[chosen].transform.rotation);
            }
        }
    }
}
