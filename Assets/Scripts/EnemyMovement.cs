/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
} 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent Enemy;

    void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        if (Enemy.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        //20 -->  walkRadius
        Vector3 randomDirection = Random.insideUnitSphere * 20;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 20, 1);
        Vector3 finalPosition = hit.position;
        Enemy.SetDestination(finalPosition);
    }
}
