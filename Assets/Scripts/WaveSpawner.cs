using System.Collections; // for coroutine
using UnityEngine;
using UnityEngine.UI;

//in version 1.0 its always spawned a wave, no matter is before-wave was kill-out
public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public static int EnemiesAlive = 0; 

    [SerializeField]
    private Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBtwWave = 5f;

    [SerializeField]
    private Text waveCounter;

    private float countDown = 2f;
    private int waveNumber = 0;

    private void Update()
    {
        //fix for count down is to make it not 5 when its 5 but 5.5f
        //waveCounter.text = (Mathf.Round(countDown).ToString()); //((int)countDown).ToString();

        if (EnemiesAlive > 0)
            return;

        if (waveNumber == waves.Length)
        {
            gameManager.LevelWon();
            this.enabled = false; // dissable the script
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBtwWave;
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCounter.text = string.Format("{0:00.00}", countDown); //read about string.Format()  
    }

    private IEnumerator SpawnWave()
    {
        //numOfEnemies = waves[waveNumber].count; watch short course from Brackeys
        //polynominal increasment

        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];

        EnemiesAlive = wave.count; // solution for bug
        // End of the video. Use gameManager as prefab can get you pretty far.
        //Or you can use scene which shared all of the scene 
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f/wave.spawnRate); // create variable for this off-timer
        }
        //for (int i = 0; i < waveNumber; i++)
        //{
        //    SpawnEnemy();
        //    yield return new WaitForSeconds(0.4f); // create variable for this off-timer
        //}
        waveNumber++;

    }

    private void SpawnEnemy(GameObject enemy) // before it didnt accept any params
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
      //  EnemiesAlive++; this line may cause bugs
    }
}
