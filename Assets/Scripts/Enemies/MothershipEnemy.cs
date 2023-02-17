using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipEnemy : MonoBehaviour
{
    public float speed;

    private Transform playerTransform;
    private Rigidbody rb;

    public GameObject enemyPrefab;
    public float spawnCooldown;
    private bool onCooldown;
    public GameObject[] spawnPoints;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);

        transform.Rotate(Vector3.up, 0.5f);

        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;

        if (!onCooldown) StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        onCooldown = true;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }
}
