using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerEnemy : MonoBehaviour
{
    public float speed;
    public float slamCooldown;
    public bool isSlamOnCooldown;

    public GameObject damageArea;
    public GameObject damageAreaFiller;
    public ParticleSystem slamParticles;

    private Transform playerTransform;
    private Rigidbody rb;
    private Animation areaAnimation;

    private void Start()
    {
        areaAnimation = damageAreaFiller.GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);

        damageArea.transform.position = new Vector3(transform.position.x, -1, transform.position.z);
        damageAreaFiller.transform.position = new Vector3(transform.position.x, -1, transform.position.z);

        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * speed;

        if (!isSlamOnCooldown) StartCoroutine(Slam());
    }


    private IEnumerator Slam()
    {
        isSlamOnCooldown = true;
        areaAnimation.Play();
        yield return new WaitForSeconds(5.5f);
        slamParticles.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        slamParticles.Play();
        damageArea.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damageArea.SetActive(false);
        yield return new WaitForSeconds(slamCooldown);
        isSlamOnCooldown = false;
    }
}
