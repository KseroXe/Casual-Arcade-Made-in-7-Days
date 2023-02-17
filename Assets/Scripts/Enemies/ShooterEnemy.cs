using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletDamage;
    public float speed;
    public float stayawayDistance;
    public float stayawaySpace;

    private bool onCooldown;
    public float cooldownBetweenShots;
    public float shootForce;

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

        Vector3 moveDirection;
        if (Vector3.Distance(transform.position, playerTransform.position) > stayawayDistance + stayawaySpace)
        {
            moveDirection = (playerTransform.position - transform.position).normalized;
            rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) < stayawayDistance - stayawaySpace)
        {
            moveDirection = (transform.position - playerTransform.position).normalized;
            rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (!onCooldown) StartCoroutine(Shoot());
        }

        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
       
    }


    private IEnumerator Shoot()
    {
        onCooldown = true;

        Vector3 shootDirection = (new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z) - transform.position).normalized;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce, ForceMode.Impulse);
        BulletBehaviour newBulletBehaviour = newBullet.GetComponent<BulletBehaviour>();
        newBulletBehaviour.enemyBullet = true;
        newBulletBehaviour.damage = bulletDamage;
        

        yield return new WaitForSeconds(cooldownBetweenShots);
        onCooldown = false;
    }
}
