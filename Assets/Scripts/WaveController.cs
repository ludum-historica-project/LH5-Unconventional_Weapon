using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public WaveArea boundaries;

    public List<Wave> waves;

    public EnemySpawner spawnerPrefab;

    public float timeBeforeStart = 5;
    public float timeBetweenWaves = 3;
    public float timeBetweenEnemies = .1f;

    public ScriptableEvent OnNextWaveReady;
    public ScriptableEvent OnLastWaveClear;

    public CharacterController2D player;

    public int startFromWave = 0;

    public Wave nextWave { get; private set; }
    // Start is called before the first frame update

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Queue<Wave> waveQueue = new Queue<Wave>();
        for (int i = startFromWave; i < waves.Count; i++)
        {
            waveQueue.Enqueue(waves[i]);
        }
        List<EnemySpawner> activeSpawners = new List<EnemySpawner>();

        List<EnemyDuster> activeEnemies = new List<EnemyDuster>();

        if (waveQueue.Count > 0)
        {
            nextWave = waveQueue.Peek();
            OnNextWaveReady.Raise();
        }

        float startTimer = timeBeforeStart;
        while (startTimer > 0)
        {
            startTimer -= Director.GetManager<TimeManager>().deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (waveQueue.Count > 0)
        {
            //take first wave
            Wave currentWave = waveQueue.Dequeue();

            //run through all the wave parts
            foreach (var part in currentWave.parts)
            {
                Queue<Vector2> positionQueue = new Queue<Vector2>();
                if (part.ordered)
                {
                    positionQueue = new Queue<Vector2>(part.area.GetOrderedPoints(part.enemyCount));
                }
                else
                {
                    for (int i = 0; i < part.enemyCount; i++)
                    {
                        positionQueue.Enqueue(part.area.GetRandomPoint(part.edge));
                    }
                }
                //spawn all enemies as intended
                for (int i = 0; i < part.enemyCount; i++)
                {
                    Vector2 position = boundaries.ClampToBoundary(positionQueue.Dequeue());
                    if (part.followPlayer) position += (Vector2)player.transform.position;
                    //add all enemies to a list of enemies
                    var spawner = Instantiate(spawnerPrefab, position, Quaternion.identity);
                    spawner.StartCount(2);
                    activeSpawners.Add(spawner);
                    spawner.OnEnemySpawn += (enemy) =>
                      {
                          activeSpawners.Remove(spawner);
                          activeEnemies.Add(enemy);
                          enemy.OnKill = () =>
                          {
                              activeEnemies.Remove(enemy);
                          };
                      };
                    if (!part.instant)
                    {
                        float enemyTimer = timeBetweenEnemies;
                        while (enemyTimer > 0)
                        {
                            enemyTimer -= Director.GetManager<TimeManager>().deltaTime;
                            yield return new WaitForEndOfFrame();
                        }
                    }
                }
                float partTimer = timeBetweenEnemies;
                while (partTimer > 0)
                {
                    partTimer -= Director.GetManager<TimeManager>().deltaTime;
                    yield return new WaitForEndOfFrame();
                }
            }
            while (activeEnemies.Count > 0 || activeSpawners.Count > 0)
            {
                yield return new WaitForEndOfFrame();
            }
            if (waveQueue.Count > 0)
            {
                nextWave = waveQueue.Peek();
                OnNextWaveReady.Raise();
            }
            float waveTimer = timeBetweenWaves;
            while (waveTimer > 0)
            {
                waveTimer -= Director.GetManager<TimeManager>().deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        OnLastWaveClear.Raise();
    }


}
