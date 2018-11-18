using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    Vector3 target;
    Vector3 initialPosition;

    public float walkSpeed = 2f;

    float minX = -1.5f;
    float maxX = 1.9f;
    float maxZ = 1.8f;
    float minZ = -1.8f;
    float timer;
    int sec;
    NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        target = ResetTarget();
        sec = ResetSec();

        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > sec)
        {
            target = ResetTarget();
            sec = ResetSec();
            agent.SetDestination(target);
        }
        
    }
    Vector3 ResetTarget()
    {
        return new Vector3(
            Mathf.Clamp(transform.position.x + Random.Range(-0.4f, 0.4f) *2,minX,maxX),
            transform.position.y,
            Mathf.Clamp(transform.position.x + Random.Range(-0.4f, -0.4f)*2,minZ,maxZ)
            );
    }



    int ResetSec()
    {
        timer = 0;
        return Random.Range(1, 3);
    }
}
