
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public Vector3 moveDirection;

    private Rigidbody rb;
    private InputManager inputManager;

    private void Start()
    { 
        rb = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
    }
    private void Update()
    {
        //Перемещение
        int xMoveDirection;
        int zMoveDirection;
        
        //X
        if (inputManager.dPressed) xMoveDirection = 1;
        else if (inputManager.aPressed) xMoveDirection = -1;
        else xMoveDirection = 0;

        //Z
        if (inputManager.wPressed) zMoveDirection = 1;
        else if (inputManager.sPressed) zMoveDirection = -1;
        else zMoveDirection = 0;

        moveDirection = new Vector3 (xMoveDirection, 0, zMoveDirection).normalized;

        rb.velocity = moveDirection * movespeed;

        //Поворот за курсором
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Vector3.Angle(Vector3.forward, new Vector3(mousePosition.x - transform.position.x, transform.position.y, mousePosition.z - transform.position.z));
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.position.x < mousePosition.x ? angle : -angle, transform.rotation.y);
    }
}
