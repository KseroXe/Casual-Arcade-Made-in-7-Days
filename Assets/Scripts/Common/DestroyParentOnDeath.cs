using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnDeath : MonoBehaviour
{

    public void DestroyParent()
    {
        StartCoroutine(DestroyParentDelayed());
    }

    public IEnumerator DestroyParentDelayed()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
