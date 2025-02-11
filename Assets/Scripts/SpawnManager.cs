using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int hard;
    public GameObject[] animalPrefabs;
    private float xSpawnRange = 10;
    private float xSpawnpos = 30;
    private float zSpawnpos = 20;
    private float zSpawnRange = 6;
    private float startDelay = 1;
    private float spawnInterval = 3;
    // Start is called before the first frame update
    private MoveForward MoveBoss;
    void Start()
    {
        LoadPlayerData();
        spawnInterval = spawnInterval - hard;

        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomBoss", 30f, 100f);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            //Instantiate(animalPrefabs[Random.Range(0, animalPrefabs.Length)], new Vector3(Random.Range(-10, 10), 0, 20), animalPrefabs[Random.Range(0, animalPrefabs.Length)].transform.rotation);
            SpawnRandomAnimal();
        }*/
    }
    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        int index = Random.Range(1, 4);
        Vector3 spawnpos;
        Quaternion rotation;
        if (index == 1)
        {
            spawnpos = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawnpos);
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (index == 2)
        {
            spawnpos = new Vector3(-xSpawnpos, 0, Random.Range(zSpawnRange, 2.5f * zSpawnRange));
            rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            spawnpos = new Vector3(xSpawnpos, 0, Random.Range(zSpawnRange, 2.5f * zSpawnRange));
            rotation = Quaternion.Euler(0, 270, 0);
        }
        Instantiate(animalPrefabs[animalIndex], spawnpos, rotation);

    }
    void SpawnRandomBoss()
    {
        int animalIndex = Random.Range(0, 2);
        int index = Random.Range(1, 4);
        Vector3 spawnpos;
        Quaternion rotation;
        if (index == 1)
        {
            spawnpos = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawnpos);
            rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (index == 2)
        {
            spawnpos = new Vector3(-xSpawnpos, 0, Random.Range(zSpawnRange, 2.5f * zSpawnRange));
            rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            spawnpos = new Vector3(xSpawnpos, 0, Random.Range(zSpawnRange, 2.5f * zSpawnRange));
            rotation = Quaternion.Euler(0, 270, 0);
        }
        GameObject boss = Instantiate(animalPrefabs[animalIndex], spawnpos, rotation);

        // Scale up the boss
        boss.transform.localScale *= 5;
        HealthBar bossHealthBar = boss.GetComponentInChildren<HealthBar>();
        if (bossHealthBar != null)
        {
            bossHealthBar.transform.localScale *= 3; // Làm cho thanh máu của Boss to lên gấp 3 lần
        }

        MoveBoss = boss.GetComponent<MoveForward>();
        MoveBoss.speedFood = 2.0f;

        // HP boss
        Health bossHealth = boss.GetComponent<Health>();
        if (bossHealth != null)
        {
            bossHealth.maxHealth *= 10;
        }


        boss.tag = "Boss";
    }
    void LoadPlayerData()
    {
        // Kiểm tra nếu dữ liệu tồn tại trong PlayerPrefs
        if (PlayerPrefs.HasKey("Hard"))
        {
            hard = PlayerPrefs.GetInt("Hard");
        }

    }
}

