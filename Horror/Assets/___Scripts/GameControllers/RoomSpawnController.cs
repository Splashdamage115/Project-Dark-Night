using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSpawnController : MonoBehaviour
{
    public GameObject[] rooms;
    private List<bool> spawned;
    void Start()
    {
        GameObject[] spawnPositions = GameObject.FindGameObjectsWithTag("DoorSpawnLocation");

        spawned = new List<bool>();

        foreach (var _ in rooms)
        {
            spawned.Add(false);
        }
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            // keep randomising until a unique room is spawned
            int spawnNum = Random.Range(0, rooms.Length);
            while (spawned[spawnNum]) 
            { 
                spawnNum = Random.Range(0, rooms.Length);
            }

            Instantiate(rooms[spawnNum], spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            spawned[spawnNum] = true;
        }
    }
}
