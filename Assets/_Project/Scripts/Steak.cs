using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steak : MonoBehaviour
{
    private Animator animator;
    private Player player;
    public GameObject burgerPrefab;
    public ParticleSystem getPrefab;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();    
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BonFire"))
        {
            print("스테이크 구워지는중");
            player.huntCount--;
            animator.SetTrigger("Start");
            Destroy(this.gameObject, 2f);
            StartCoroutine(SetBurger());
        }
    }

    private IEnumerator SetBurger()
    {
        print("코루틴 시작");
        yield return new WaitForSeconds(1.9f);
        player.burgerCount++;
        ParticleSystem getBurger = Instantiate(getPrefab, transform.position, Quaternion.identity);
        getBurger.Play();
        Destroy(getBurger.gameObject, 2f);
        GameObject newBurger = Instantiate(burgerPrefab, transform.position, Quaternion.identity);
        player.burgers.Add(newBurger);
        print("버거 획득");
    }
}
