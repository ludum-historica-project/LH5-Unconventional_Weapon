﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public List<Wave> waves;

    public EnemyDuster dusterPrefab;

    public float timeBeforeStart = 5;
    public float timeBetweenWaves = 3;
    public float timeBetweenParts = 1;
    public float timeBetweenEnemies = .1f;

    public ScriptableEvent OnLastWaveClear;


    // Start is called before the first frame update

    IEnumerator Start()
    {
        Queue<Wave> waveQueue = new Queue<Wave>(waves);

        List<EnemyDuster> activeEnemies = new List<EnemyDuster>();

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
                //spawn all enemies as intended
                for (int i = 0; i < part.enemyCount; i++)
                {
                    //add all enemies to a list of enemies
                    var enemy = Instantiate(dusterPrefab, part.area.GetRandomPoint(part.edge), Quaternion.identity);
                    activeEnemies.Add(enemy);
                    enemy.OnKill = () =>
                    {
                        activeEnemies.Remove(enemy);
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
            while (activeEnemies.Count > 0)
            {
                yield return new WaitForEndOfFrame();
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
