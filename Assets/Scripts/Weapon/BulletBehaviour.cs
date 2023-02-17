using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float damage = 1;

    public bool enemyBullet;

    private ParticleSystem deathParticleSystem;

    private void Start()
    {
        deathParticleSystem = GetComponentInChildren<ParticleSystem>();
        if (enemyBullet)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = Color.red;
            transform.localScale = Vector3.one/3;
        }
        StartCoroutine(DestroyOnTime());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") != true && other.gameObject.CompareTag("IgnoreBullets") != true)
        {
            if (other.gameObject.CompareTag("Enemy") == true && !enemyBullet)
            {
                other.gameObject.GetComponent<Entity>().TakeDamage(damage);
                StartCoroutine(DestroyBullet());
            }
            else if (other.gameObject.CompareTag("Player") && enemyBullet)
            {
                other.gameObject.GetComponent<Entity>().TakeDamage(damage);
                StartCoroutine(DestroyBullet());
            }
        }
    }

    private IEnumerator DestroyBullet()
    {
        deathParticleSystem.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private IEnumerator DestroyOnTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
