using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    Vector3 target;
    public List<GameObject> Factories;

    public Transform player;
    public float waitBeforeSpawn = 10f; 

    public float factoryHealthGen = 7f;

    float MinScale = 0.0025f;
    float MaxScale;

    float minX = -1.5f;
    float maxX = 1.9f;
    float maxZ = 1.8f;
    float minZ = -1.8f;
    float timer;
    float lastInstantiatedTimer;
    int sec;

    bool isNearPlayer = false;
    NavMeshAgent agent;
    Health health;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        lastInstantiatedTimer = 0f;
    }
    void Start()
    {
        target = ResetTarget();
        sec = ResetSec();
        MaxScale = transform.localScale.x;
    }

    void FixedUpdate()
    {
		if(PauseMenu.IsPaused) {
			return;
		}
        //RandomMove();
        FollowPlayer();
        SpawnFactory();
        FactoryHeal();
        ResizeOnHealth();
        lastInstantiatedTimer += Time.deltaTime;
    }


    void FactoryHeal() {
        health.TakeDamage(-factoryHealthGen*GameObject.FindGameObjectsWithTag("Enemy").Length*Time.deltaTime);
    }

    void ResizeOnHealth() {
        float scale = Mathf.Clamp(health.PercentHealth()*MaxScale,MinScale,MaxScale);
        transform.localScale = new Vector3(scale,scale,scale);
    }
    void FollowPlayer() {
        if (isNearPlayer) {
            return;
        }
        agent.SetDestination(player.position);
    }

    public void FoundPlayer() {
        isNearPlayer = true;
    }

    public void LostPlayer() {
        isNearPlayer = false;
    }

    void SpawnFactory() {
        if (isNearPlayer) {
            return;
        }
        if(lastInstantiatedTimer > waitBeforeSpawn && Random.Range(0f, 1f) > 0.75f) {
            int i = Random.Range(0, Factories.Count);
            GameObject factory = Instantiate(Factories[i],new Vector3(transform.position.x, 0.00f, transform.position.z),Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            lastInstantiatedTimer = 0;
        }
    }

    void RandomMove() {
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
            DirectedRandom(transform.position.x, minX,maxX),
            transform.position.y,
            DirectedRandom(transform.position.z,minZ,maxZ)
            );
    }

    float DirectedRandom(float current, float min, float max) {
        float allowedMin = -0.8f;
        float allowedMax = 0.8f;

        if (current + allowedMax > max) {
           allowedMax = max-current;
        }

        if(current + allowedMin < min) {
            allowedMin = min-current;
        }

        return current + Random.Range(allowedMin, allowedMax);
    }



    int ResetSec()
    {
        timer = 0;
        return Random.Range(1, 3);
    }
}
