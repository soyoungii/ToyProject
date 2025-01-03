using System;
using System.Collections;
using System.Collections.Generic;
using CurlNoiseParticleSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.XR.Content.Walkthrough;

public class Monster : MonoBehaviour
{
    private int monsterHp = 5;
    public ParticleSystem dieParticlePrefab;
    public GameObject steakPrefab; 
    private Animator anim;
    private NavMeshAgent agent;
    private Transform target;
    private Player player;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        anim.SetFloat("Walk", 0.1f);
        target = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        if (monsterHp <= 0)
        {
            agent.isStopped = true;
            anim.SetFloat("Walk", 0f);
        }
        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            agent.isStopped = true;
            anim.SetFloat("Walk", 0f);
        }
        
        else if (Vector3.Distance(transform.position, target.position) > 3f)
        {
            agent.isStopped = false;
            anim.SetFloat("Walk", 0.1f);
        }
    }

    private void HitDamage()
    {
        monsterHp -= 5; 
        anim.SetTrigger("ChangeEyes");
        anim.SetTrigger("Death");
        if (monsterHp <= 0)
        {
            StartCoroutine(MonsterDieAnim());
        }
    }
    
    private void DieParticlePlay()
    {
        ParticleSystem die = Instantiate(dieParticlePrefab, transform.position, Quaternion.identity); 
        die.Play(); 
        Destroy(die.gameObject, 3f);
    }

    private IEnumerator MonsterDieAnim()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
        player.huntCount++;
        ParticleSystem die = Instantiate(dieParticlePrefab, transform.position, Quaternion.identity);
        Instantiate(steakPrefab, transform.position, Quaternion.identity);
        die.Play(); 
        Destroy(die.gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            HitDamage();
            print($"현재 몬스터 체력: {monsterHp}");
        }
    }
}
