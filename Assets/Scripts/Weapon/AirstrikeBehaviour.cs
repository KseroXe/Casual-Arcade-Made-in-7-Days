using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeBehaviour : MonoBehaviour
{
    public float damage;
    public ParticleSystem airstrikeParticleSystem;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
        airstrikeParticleSystem.transform.position = transform.position;
        airstrikeParticleSystem.Play();
        StartCoroutine(Deactivate());

    }
    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
