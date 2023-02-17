using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public int durability;
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") == true)
        {
            durability--;
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);

            if (durability <= 0) gameObject.SetActive(false);
        }
    }
}
