using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monsterPrefab;
    private int spawnCount = 10;
    private void Start()
    {
        StartCoroutine(MonsterSpawnRoop());
    }

    private IEnumerator MonsterSpawnRoop()
    {
        
        while (true)
        {
            
        }
    }
}
