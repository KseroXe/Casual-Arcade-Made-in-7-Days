using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject followerPrefab;
    public GameObject shooterPrefab;
    public GameObject bomberPrefab;
    public GameObject destroyerPrefab;
    public GameObject mothershipPrefab;

    public float followerSpawnRate;
    public float shooterSpawnRate;
    public float bomberSpawnRate;
    public float destroyerSpawnRate;
    public float mothershipSpawnRate;

    private bool spawningShooters = false;
    private bool spawningBomber = false;
    private bool spawningDestroyer = false;
    private bool spawningMotherships = false;

    public float timePassed;

    public GameObject[] spawnPoints;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        timePassed = 0;

        followerSpawnRate = 2.25f;
        shooterSpawnRate = 4f;
        bomberSpawnRate = 5f;
        destroyerSpawnRate = 12f;
        mothershipSpawnRate = 30f;

        StartCoroutine(StartFollowerSpawn());
    }
    private void Update()
    {
        timePassed += Time.deltaTime;
    }
    private IEnumerator StartFollowerSpawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length-1);
        Instantiate(followerPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        if (!spawningShooters && timePassed >= 90)
        {
            spawningShooters = true;
            StartCoroutine(StartShooterSpawn());
        }
        yield return new WaitForSeconds(followerSpawnRate);
        StartCoroutine(StartFollowerSpawn());
    }
    private IEnumerator StartShooterSpawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length - 1);
        Instantiate(shooterPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        if (!spawningBomber && timePassed >= 210)
        {
            spawningBomber = true;
            StartCoroutine(StartBomberSpawn());
        }
        yield return new WaitForSeconds(shooterSpawnRate);
        StartCoroutine(StartShooterSpawn());
    }
    private IEnumerator StartBomberSpawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length - 1);
        Instantiate(bomberPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        if (!spawningDestroyer && timePassed >= 350)
        {
            spawningDestroyer = true;
            StartCoroutine(StartDestroyerSpawn());
        }
        yield return new WaitForSeconds(bomberSpawnRate);
        StartCoroutine(StartBomberSpawn());
    }
    private IEnumerator StartDestroyerSpawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length - 1);
        Instantiate(destroyerPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        if (!spawningMotherships && timePassed >= 470)
        {
            spawningMotherships = true;
            StartCoroutine(StartMothershipSpawn());
        }
        yield return new WaitForSeconds(destroyerSpawnRate);
        StartCoroutine(StartDestroyerSpawn());
    }
    private IEnumerator StartMothershipSpawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length - 1);
        Instantiate(mothershipPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(mothershipSpawnRate);
        StartCoroutine(StartMothershipSpawn());
    }
}
