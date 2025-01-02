using System;
using System.Collections;
using System.Collections.Generic;
using CurlNoiseParticleSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Content.Walkthrough;

public class Monster : MonoBehaviour
{
    private int monsterHp = 10;
    public ParticleSystem dieParticlePrefab;
    public Animator anim;
    private NavMeshAgent agent;
    public Camera mainCamera;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        anim.SetFloat("Walk", 0.1f);
    }

    private void Update()
    {
        agent.SetDestination(mainCamera.transform.position);
        if (Vector3.Distance(transform.position, mainCamera.transform.position) < 1.5f)
        {
            agent.isStopped = true;
            anim.SetFloat("Walk", 0f);
        }
    }

    private void HitDamage()
    {
        anim.SetTrigger("Hit");
        monsterHp -= 5;
        if (monsterHp <= 0)
        {
            MonsterDie();
        }
    }

    private void MonsterDie()
    { 
        anim.SetTrigger("ChangeEyes");
        anim.SetTrigger("Death");
        DieParticlePlay();
    }

    private void DieParticlePlay()
    {
        Destroy(this.gameObject, 2f);
        ParticleSystem die = Instantiate(dieParticlePrefab, transform.position, Quaternion.identity); 
        die.Play(
            
            ); 
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
