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
    [SerializeField] private GameObject maxAmmo;
    [SerializeField] private int enemyAmount = 6;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private float waveCoolDown;
    [SerializeField] private float difficultyScaler;
    [SerializeField] private int maxZombiesAtOnce = 40;

    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool canSpawn;
    private bool waveActive = false;
    [SerializeField] private int[] zombieTypeWeights = {100, 0, 0}; // Regular, Sprint, Brute
    private int sprintZombieWeight = 20; // Weight for sprint zombies
    private int bruteZombieWeight = 10; // Weight for brute zombies

    private void Awake()
    {
        onZombieKilled.AddListener(OnDestroy); // add listener to call for enemy deaths
        ResetHealthAmount();
    }
    
    private void Start()
    {
        waveText.text = currentWave.ToString();
        StartCoroutine(StartGame());
    }

    private void Update() // Maybe put in coroutine so it doesn't check every frame?
    {
        if (!waveActive) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnSpeed && enemiesLeftToSpawn > 0 && enemiesAlive < maxZombiesAtOnce) 
        {
            SpawnEnemy();
            //SpawnEnemyGroup(4);
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void OnDestroy()
    {
        enemiesAlive--;
        if (currentWave % 5 == 0 && enemiesLeftToSpawn == 0 && enemiesAlive == 0) // Every 5 waves
        {
            GameObject zombie = GameObject.FindWithTag("Zombie");
            if (zombie != null)
            {
                Instantiate(maxAmmo, zombie.transform.position, Quaternion.identity);
            }
        }
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

        if (currentWave == 5) // Wave 5: (80, 20, 0)
        {
            zombieTypeWeights[1] = sprintZombieWeight;
            zombieTypeWeights[0] -= sprintZombieWeight;
        }
        if (currentWave > 5 && currentWave <= 10)
        {
            zombieTypeWeights[1]++;
            zombieTypeWeights[0]--;
        }
        if (currentWave == 10) // Wave 10: (65, 25, 10)
        {
            zombieTypeWeights[2] = bruteZombieWeight;
            zombieTypeWeights[0] -= bruteZombieWeight;
        }
        if (currentWave > 10 && zombieTypeWeights[0] > 10) // Maxed out Wave 37: (11, 52, 37)
        {
            zombieTypeWeights[1]++;
            zombieTypeWeights[2]++;
            zombieTypeWeights[0] -= 2;  
        }
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
        if (currentWave == 15)
        {
            UpgradeEnemyHealth();
        }
        //Debug.Log("New enemy amount: " + Mathf.RoundToInt(enemyAmount * Mathf.Pow(currentWave, difficultyScaler)));
        return Mathf.RoundToInt(enemyAmount * Mathf.Pow(currentWave, difficultyScaler));
    }

    private void SpawnEnemy() // Maybe change to bulk spawn enemies!
    {
        int totalWeight = 100; // Total sum of weights
        int randomNumber = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;
        int selectedZombieType = 0;

        ZombiePoolManager poolManager = ZombiePoolManager.Instance;

        // Iterate through the weights to find the selected zombie type
        for (int i = 0; i < zombieTypeWeights.Length; i++)
        {
            cumulativeWeight += zombieTypeWeights[i];
            if (randomNumber < cumulativeWeight)
            {
                selectedZombieType = i;
                break;
            }
        }

        /*
        int randomIndex = Random.Range(0, spawnLocations.Count - 1);
        GameObject zombiePrefab = zombies[selectedZombieType];
        Instantiate(zombiePrefab,spawnLocations[randomIndex].position, Quaternion.identity);
        */

        ZombieVitals zombie = poolManager.GetZombie(selectedZombieType);
        if (zombie != null)
        {
            int randomIndex = Random.Range(0, spawnLocations.Count);
            zombie.transform.position = spawnLocations[randomIndex].position;
            zombie.gameObject.SetActive(true);
        }

    }

    private void SpawnEnemyGroup(int groupSize) // TEST
    {
        int remainingZombies = enemiesLeftToSpawn;

        // Spawn complete groups
        while (remainingZombies >= groupSize)
        {
            SpawnGroup(groupSize);
            remainingZombies -= groupSize;
        }

        // Spawn remaining zombies
        if (remainingZombies > 0)
        {
            SpawnGroup(remainingZombies);
        }
    }

    private void SpawnGroup(int size)
    {
        // Spawn enemies in the group
        for (int i = 0; i < size; i++)
        {
            SpawnEnemy();
        }
    }

    public void UpgradeEnemyHealth()
    {
        zombies[0].GetComponent<ZombieVitals>().IncreaseHealth(5);
    }

    private void ResetHealthAmount()
    {
        foreach (GameObject zombie in zombies)
        {
            zombie.GetComponent<ZombieVitals>().ResetHealth();
        }
    }

}
