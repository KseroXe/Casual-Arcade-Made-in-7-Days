using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamageDealer : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) collision.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }
}
