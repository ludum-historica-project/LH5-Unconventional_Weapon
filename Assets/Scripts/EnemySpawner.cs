using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyDuster dusterPrefab;
    public float timeRemaining = -1;

    public System.Action<EnemyDuster> OnEnemySpawn = (e) => { };


    public void StartCount(float time)
    {
        timeRemaining = time;
    }


    // Update is called once per frame
    void Update()
    {
        if (!Director.GetManager<TimeManager>().Paused) transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, .1f);
        if (timeRemaining > 0)
        {
            timeRemaining -= Director.GetManager<TimeManager>().deltaTime;
            if (timeRemaining <= 0)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        OnEnemySpawn(Instantiate(dusterPrefab, transform.position, Quaternion.identity));
        Destroy(gameObject);
    }
}
