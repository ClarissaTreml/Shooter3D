using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;
using UnityEngine.AI;


public class ShootingAgent : Agent
{
    public Transform bulletSpawnPoint;
    public Projectile projectile;
    public int minStepsBetweenShots = 50;
    public int damage = 100;
    public float moveSpeed = 12;

    private bool shotAvailable = true;
    private int stepsUntilShotIsAvaliable = 0;
    private Vector3 startingPosition;
    NavMeshAgent enemyAgent;

    public CharacterController controller;
    public float speed = 6f;
    
    
    private void Shoot()
    {
        if (!shotAvailable) return;

        var layerMask = 1 << LayerMask.NameToLayer("Agent");
        var direction = transform.forward;
        
        var spawnedProjectile = Instantiate(projectile, bulletSpawnPoint.position, Quaternion.Euler(0f, -90f, 0f));
        spawnedProjectile.SetDirection(direction);

        Debug.Log("Shot");
        Debug.DrawRay(bulletSpawnPoint.position, direction *200f, Color.green, 1f);

        if(Physics.Raycast(bulletSpawnPoint.position, direction, out var hit, 200f, layerMask))
        {
            hit.transform.GetComponent<PlayerAgent>().GetShot(damage, this);
        }
        else
        {
            AddReward(-0.033f);
        } 

        shotAvailable = false;
        stepsUntilShotIsAvaliable = minStepsBetweenShots;
    }

    private void FixedUpdate()
    {
        if(!shotAvailable)
        {
            stepsUntilShotIsAvaliable--;

            if(stepsUntilShotIsAvaliable <= 0)
            {
                shotAvailable = true;
            }
        } 
    }

   public override void OnActionReceived(ActionBuffers actions)
   {
        if(actions.ContinuousActions[0] >= 1)
        {
            Shoot();
        }
        enemyAgent.velocity = new Vector3(actions.ContinuousActions[2] * speed, 0f, actions.ContinuousActions[3] * speed);
   }

   public override void CollectObservations(VectorSensor sensor)
   {
        base.CollectObservations(sensor);
   }

   public override void Initialize()
   {
        enemyAgent = GetComponent<NavMeshAgent>(); 
        startingPosition = transform.position;
   }

   public override void Heuristic(in ActionBuffers actionsOut)
   {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetKey(KeyCode.P) ? 1f : 0f;
        continuousActions[2] = Input.GetAxis("Horizontal");
        continuousActions[3] = Input.GetAxis("Vertical");
   }

   public override void OnEpisodeBegin()
   {
        Debug.Log("Episode Begin");
        transform.position = startingPosition;
        enemyAgent.velocity = Vector3.zero;
        shotAvailable = true;
   }

   private void Reset()
   {
    Debug.Log("Reset");
   }

   public void RegisterKill()
   {
        AddReward(1.0f);
        EndEpisode();
   }

   private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("agent"))
        {
            AddReward(-1f);
            EndEpisode();
        }
    }

}

