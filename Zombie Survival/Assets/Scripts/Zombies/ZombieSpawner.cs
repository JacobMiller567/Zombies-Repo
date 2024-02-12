using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{
    public static UnityEvent onZombieKilled = new UnityEvent();
    public TMP_Text waveText;
    public int currentWave = 1;

    [SerializeField] private GameObject[] zombies;
    [SerializeField] private List<Transform> spawnLocations;

    [SerializeField] private int enemyAmount = 6;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private float waveCoolDown;
    [SerializeField] private float difficultyScaler;

    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool canSpawn;
    private bool waveActive = false;

    private void Awake()
    {
        onZombieKilled.AddListener(OnDestroy); // add listener to call for enemy deaths
    }
    
    private void Start()
    {
        waveText.text = currentWave.ToString();
        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (!waveActive) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnSpeed && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
           // Debug.Log("All Enemies Dead: " + currentWave);
            EndWave();
        }
    }

    private void OnDestroy()
    {
        enemiesAlive--;
       // Debug.Log("Enemies Left: " + enemiesAlive);
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(waveCoolDown);
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(waveCoolDown);
        waveActive = true;
        waveText.text = currentWave.ToString();
        enemiesLeftToSpawn = IncreaseEnemies();
    }

    private void EndWave()
    {
        waveActive = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        if (currentWave != 1)
        {
            PlayerVitals.instance?.IncreaseMoney(Mathf.RoundToInt((currentWave * 1.5f) + 5));
        }
        StartCoroutine(StartWave());
    }

    private int IncreaseEnemies()
    {
        //UpgradeEnemyHealth();
        Debug.Log("New enemy amount: " + Mathf.RoundToInt(enemyAmount * Mathf.Pow(currentWave, difficultyScaler)));
        return Mathf.RoundToInt(enemyAmount * Mathf.Pow(currentWave, difficultyScaler));
    }

    private void SpawnEnemy()
    {
        int zombieType = 0;
        if (currentWave > 9 && currentWave <= 19)
        {
            float chooseEnemy = Random.Range(0, 5);
            if (chooseEnemy == 0) // 20%
            {
                zombieType = 1;
            }
            else // 80%
            {
                zombieType = 0;
            }
        }
        if (currentWave >= 20)
        {
            float chooseEnemy = Random.Range(0, 5);
            if (chooseEnemy == 0) // 20%
            {
                zombieType = 2;
            }
            if (chooseEnemy > 0 && chooseEnemy < 3) // 40%
            {
                zombieType = 1;
            }
            else // 40%
            {
                zombieType = 0;
            }
        }
        int randomIndex = Random.Range(0, spawnLocations.Count - 1);
        GameObject zombiePrefab = zombies[zombieType];
        Instantiate(zombiePrefab,spawnLocations[randomIndex].position, Quaternion.identity);
    }

}
