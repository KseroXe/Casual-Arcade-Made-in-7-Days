using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Переменная move
        if (playerMovement.moveDirection != Vector3.zero) animator.SetBool("move", true);
        else animator.SetBool("move", false);

        //Переменная lookBack
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Vector3.Angle(playerMovement.moveDirection, new Vector3(mousePosition.x - transform.position.x, transform.position.y, mousePosition.z - transform.position.z));
        bool lookBack = angle > 90 ? true : false;
        animator.SetBool("lookBack", lookBack);
    }
}
