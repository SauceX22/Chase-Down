using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    private int waveNumber = 0;
    [Header("===== Waves ===== ")]
    public Text waveCountDownTimer;
    public GameObject startingNextWavePanel;
    public GameObject waveCompletedPanel;
    public Text waveCompletedText;
    public Text waveEnemiesCountText;
    public List<Wave> waves = new List<Wave>();
    public float timeBetweenWaves = 5f;
    public bool loopWaves = false;
    private int currentWaveIndex = -1;
    private float waveCountdown;
    private bool spawningWave;
    [Header("===== Spawn points ===== ")]
    public Transform[] spawnPoints;

    /// <summary>
    /// Gets the current wave
    /// </summary>
    private Wave CurrentWave
    {
        get
        {
            return currentWaveIndex >= 0 && currentWaveIndex < waves.Count
                ? waves[currentWaveIndex]
                : null;
        }
    }


    /// <summary>
    /// Indicates whether the last wave has been cleared (if waves don't loop)
    /// </summary>
    private bool ClearedLastWave
    {
        get { return !loopWaves && currentWaveIndex == waves.Count - 1 && CurrentWave.IsCleared; }
    }


    void Start()
    {
        RandomizeWaves();

        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points referenced.");
        waveCountdown = timeBetweenWaves;
        
    }


    void Update()
    {
        waveCountDownTimer.text = waveCountdown.ToString("0");
        waveCountdown -= Time.deltaTime;
        if (spawningWave)
        {
            if (waveCountdown <= 0 && CurrentWave != null && CurrentWave.RemainingEnemiesToInstantiate > 0)
            {
                Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                CurrentWave.InstantiateEnemy(spawnPoint.position, spawnPoint.rotation);
                waveCountdown = 1 / CurrentWave.Rate;
            }
        }
        else if (waveCountdown <= 0)
        {
            StartNextWave();
        }


    }

   

    /// <summary>
    /// Starts the next wave
    /// </summary>
    private void StartNextWave()
    {
        Debug.Log("Starting next wave!");
        waveCountDownTimer.gameObject.SetActive(false);
        startingNextWavePanel.SetActive(true);
        StartCoroutine(HideNextWavePanel(2));
        spawningWave = true;
        if (CurrentWave != null)
        {
            CurrentWave.OnCleared -= OnWaveCleared;
            CurrentWave.OnEnemySpawned -= OnEnemySpawned;
            CurrentWave.OnEnemyKilled -= OnEnemyKilled;
        }
        if (!ClearedLastWave)
        {
            currentWaveIndex = (currentWaveIndex + 1) % waves.Count;
            CurrentWave.Reset();
            CurrentWave.OnCleared += OnWaveCleared;
            CurrentWave.OnEnemySpawned += OnEnemySpawned;
            CurrentWave.OnEnemyKilled += OnEnemyKilled;
        }
    }


    /// <summary>
    /// Called when the given wave has been cleared (all the instantiated enemies have been destroyed)
    /// </summary>
    private void OnWaveCleared(Wave wave)
    {
        if (ClearedLastWave)
        {
            Debug.Log("You have cleared the last wave, congratulations!");
        }
        else
        {
            Debug.Log("Wave Completed! Next one starting in " + timeBetweenWaves + " seconds");
            waveCountDownTimer.gameObject.SetActive(true);
        }
        waveCountdown = timeBetweenWaves;
        spawningWave = false;
        waveCompletedPanel.SetActive(true);
        waveNumber++;
        waveCompletedText.text = wave.Name + waveNumber + " Completed";
        StartCoroutine(HideWaveComPanel(2));
    }


    private void OnEnemySpawned(Wave wave, Transform spawnedEnemy)
    {
        waveEnemiesCountText.text = string.Format("{0} / {1}", wave.RemainingEnemiesAlive, wave.Count);
    }


    private void OnEnemyKilled(Wave wave, Transform killedEnemy)
    {
        waveEnemiesCountText.text = string.Format("{0} / {1}", wave.RemainingEnemiesAlive, wave.Count);
    }


    /// <summary>
    /// Hides the wave panel after the given delay
    /// </summary>
    private IEnumerator HideWaveComPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        waveCompletedPanel.SetActive(false);
    }
    private IEnumerator HideNextWavePanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        startingNextWavePanel.SetActive(false);
    }

    void RandomizeWaves()
    {
        foreach (Wave wave in waves.GetRange(0, 5))
        {
            wave.count = Random.Range(1, 6);
            wave.rate = Random.Range(0.6f, 1);
        }

        foreach (Wave wave in waves.GetRange(5, 6))
        {
            wave.count = Random.Range(10, 15);
            wave.rate = Random.Range(0.1f, 0.6f);
        }

        foreach (Wave wave in waves.GetRange(11, 10))
        {
            wave.count = Random.Range(15, 25);
            wave.rate = Random.Range(0.1f, 0.4f);
        }

        foreach (Wave wave in waves.GetRange(21, 10))
        {
            wave.count = Random.Range(13, 20);
            wave.rate = Random.Range(0.8f, 1);
        }

        foreach (Wave wave in waves.GetRange(31, 10))
        {
            wave.count = Random.Range(20, 30);
            wave.rate = Random.Range(0.3f, 0.7f);
        }

        foreach (Wave wave in waves.GetRange(41, 10))
        {
            wave.count = Random.Range(30, 40);
            wave.rate = Random.Range(0.3f, 0.8f);
        }

        foreach (Wave wave in waves.GetRange(51, 10))
        {
            wave.count = Random.Range(35, 45);
            wave.rate = Random.Range(0.7f, 1f);
        }

        foreach (Wave wave in waves.GetRange(61, 10))
        {
            wave.count = Random.Range(40, 50);
            wave.rate = Random.Range(0.8f, 1f);
        }

        foreach (Wave wave in waves.GetRange(71, 10))
        {
            wave.count = Random.Range(60, 65);
            wave.rate = Random.Range(0.5f, 0.8f);
        }

        foreach (Wave wave in waves.GetRange(81, 10))
        {
            wave.count = Random.Range(70, 75);
            wave.rate = Random.Range(0.8f, 1f);
        }

        foreach (Wave wave in waves.GetRange(91, 10))
        {
            wave.count = Random.Range(80, 90);
            wave.rate = Random.Range(0.7f, 1f);
        }
    }
}