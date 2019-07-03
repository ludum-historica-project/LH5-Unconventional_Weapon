using System.Collections;
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

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Queue<Wave> waveQueue = new Queue<Wave>(waves);

        List<EnemyDuster> activeEnemies = new List<EnemyDuster>();

        yield return new WaitForSeconds(timeBeforeStart);
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
                        yield return new WaitForSeconds(timeBetweenEnemies);
                    }
                }
                yield return new WaitForSeconds(timeBetweenParts);
            }
            while (activeEnemies.Count > 0)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }


}
