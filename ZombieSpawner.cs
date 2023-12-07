using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombies;
    public Transform[] position;
    private float timePassed;
    private float pauseSpawn;
    private float spawnSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        pauseSpawn = spawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = Time.realtimeSinceStartup;
        if(Mathf.Round(timePassed) %10 == 0 && pauseSpawn < 0)
        {
            SpawnZombies();
            pauseSpawn = spawnSpeed;
        }
        pauseSpawn -= 0.0143f;
        
    }
    void SpawnZombies()
    {
        new WaitForSeconds(UnityEngine.Random.Range(0, 5));
        GameObject newZombie = Instantiate(zombies, position[UnityEngine.Random.Range(0, 13)]);
        newZombie.transform.localPosition = Vector3.zero;
    }
}
