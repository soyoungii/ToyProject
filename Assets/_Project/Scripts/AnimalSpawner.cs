using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
    public List<GameObject> Animals; //몬스터 프리팹 리스트
    private float spawnRadius = 40f;
    public Transform spawnAreaCenter;

    private void Start()
    {
        StartCoroutine(MonsterSpawnRoop());
    }

    private IEnumerator MonsterSpawnRoop()
    {
        
        while (true)
        {
            Vector3 spawnPos = GetRandomPostion(spawnAreaCenter.position, spawnRadius);
            if (spawnPos != Vector3.zero)
            {
                print("랜덤 위치에 몬스터 생성");
                Instantiate(Animals[Random.Range(0, Animals.Count)], spawnPos, Quaternion.identity);
                
            }
            
            yield return new WaitForSeconds(20f);
        }
    }

    private Vector3 GetRandomPostion(Vector3 center, float radius)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            print("유효한 위치 찾음");
            return hit.position;
        }
        print("유효한 위치를 찾을 수 없음");
        return Vector3.zero;
    }
    
}
