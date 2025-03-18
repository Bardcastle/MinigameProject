using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    private PlayerMove PlayerScript;
    private Vector3 PlayerLoc;
    private Vector3 SpawnPoint;
    bool ValidSpawn;
    public int SpawnDelay;
    // Update is called once per frame
    void Update()
    {
        if (!PlayerScript.Pause)
        {
            if (SpawnDelay >= 400)
            {
                while (ValidSpawn == false)
                {
                    SpawnPoint = new Vector3(Random.Range(-24.45f, 24.45f), 0.9f, Random.Range(-24.45f, 24.45f));
                    PlayerLoc = Player.transform.position;
                    float DistToPlayer = Vector3.Distance(PlayerLoc, SpawnPoint);
                    if (DistToPlayer >= 25)
                    {
                        ValidSpawn = true;
                    }
                }
                Instantiate(Enemy, SpawnPoint, transform.rotation);
                ValidSpawn = false;
                SpawnDelay = 0;
            }
            else
            {
                SpawnDelay += 1;
            }
        }
    }
    private void Start()
    {
        PlayerScript = Player.GetComponent<PlayerMove>();
        SpawnDelay = 0;
        ValidSpawn = false;
    }
}
