using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PlayerAgent : MonoBehaviour
{
    public int startingHealth = 100;
    
    public float randomRangeX_Pos = 0f;
    public float randomRangeX_Neg = 0f;
    public float randomRangeZ_Pos = 0f;
    public float randomRangeZ_Neg = 0f;

    private int CurrentHealth;
    private Vector3 StartPosition;

    NavMeshAgent playerAgent;


    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        StartPosition = transform.position;
        CurrentHealth = startingHealth;
    }

    void Update()
    {
        if (playerAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    public void GetShot(int damage, ShootingAgent shooter)
    {
        ApplyDamage(damage, shooter);
    }

    private void ApplyDamage(int damage, ShootingAgent shooter)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            Die(shooter);
        }
    }

    private void Die(ShootingAgent shooter)
    {
        Debug.Log("I died!");
        shooter.RegisterKill();
        Respawn();
    }

    public void Respawn()
    {
        CurrentHealth = startingHealth;
        transform.position = new Vector3(StartPosition.x + Random.Range(randomRangeX_Neg, randomRangeX_Pos), StartPosition.y, StartPosition.z + Random.Range(randomRangeZ_Neg, randomRangeZ_Pos));
        SetRandomDestination();
    }

    void SetRandomDestination()
    {
        //20 -->  walkRadius
        Vector3 randomDirection = Random.insideUnitSphere * 20;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 20, 1);
        Vector3 finalPosition = hit.position;
        playerAgent.SetDestination(finalPosition);
    }

       
} 






