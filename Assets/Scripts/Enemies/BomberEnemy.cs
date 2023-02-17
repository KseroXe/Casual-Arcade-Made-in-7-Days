using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : MonoBehaviour
{
    public float speed;
    public float stayawayDistance;
    public float stayawaySpace;

    public GameObject explosionSphere;

    private Transform playerTransform;
    private Rigidbody rb;

    private bool destroying;
    void Start()
    {
        explosionSphere.SetActive(false);
        destroying = false;
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
    }


    void Update()
    {
        transform.position = new Vector3 (transform.position.x, 1, transform.position.z);
        Vector3 moveDirection;
        if (Vector3.Distance(transform.position, playerTransform.position) > stayawayDistance + stayawaySpace && !destroying)
        {
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
            moveDirection = (playerTransform.position - transform.position).normalized;
            rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) < stayawayDistance - stayawaySpace && !destroying)
        {
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
            moveDirection = (transform.position - playerTransform.position).normalized;
            rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;
        }
        else
        {
            destroying = true;
            StartCoroutine(KamikazeAttack());
        }
    }

    private IEnumerator KamikazeAttack()
    {
        Vector3 playerPos = playerTransform.position;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
        rb.velocity = (new Vector3(playerPos.x - transform.position.x, 0, playerPos.z - transform.position.z)).normalized * speed * 3;
        yield return new WaitForSeconds(0.5f);
        explosionSphere.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GetComponent<Entity>().TakeDamage(100000);
    }
}
