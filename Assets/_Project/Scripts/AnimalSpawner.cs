using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
    public List<GameObject> Animals; //몬스터 프리팹 리스트
    private int spawnCount = 10;
    private Vector3 monsterPos;
    private void Start()
    {
        StartCoroutine(MonsterSpawnRoop());
    }

    private IEnumerator MonsterSpawnRoop()
    {
        
        while (true)
        {
            float x = Random.Range(-5, 5);
            float y = Random.Range(-5, 5);
            float z = Random.Range(-5, 5);
            
            monsterPos = new Vector3(x,y,z);
            Instantiate(Animals[Random.Range(0, Animals.Count)], monsterPos, Quaternion.identity);
            
            yield return new WaitForSeconds(5f);
        }
    }
}
