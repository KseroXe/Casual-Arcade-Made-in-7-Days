using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : MonoBehaviour
{
    public float speed;

    private Transform playerTransform;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector3 (moveDirection.x, 0, moveDirection.z) * speed;

        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
    }
}
