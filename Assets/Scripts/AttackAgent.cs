using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AttackAgent : MonoBehaviour
{
    public int startingHealth = 100;
    
    public float randomRangeX_Pos = 0f;
    public float randomRangeX_Neg = 0f;
    public float randomRangeZ_Pos = 0f;
    public float randomRangeZ_Neg = 0f;

    private int CurrentHealth;
    private Vector3 StartPosition;

    public NavMeshAgent playerAgent;
    public Transform Agent;


    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        StartPosition = transform.position;
        CurrentHealth = startingHealth;
    }

    void Update()
    {
        playerAgent.SetDestination(Agent.position);
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
        playerAgent.SetDestination(Agent.position);
    }



       
} 






