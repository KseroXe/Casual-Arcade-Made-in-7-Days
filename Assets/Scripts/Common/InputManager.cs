
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool wPressed;
    public bool aPressed;
    public bool sPressed;
    public bool dPressed;

    public bool m0Pressed;

    public bool onePressed;
    public bool twoPressed;
    public bool threePressed;
    public bool fourPressed;
    public bool fivePressed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) wPressed = true;
        else wPressed = false;

        if (Input.GetKey(KeyCode.A)) aPressed = true;
        else aPressed = false;

        if (Input.GetKey(KeyCode.S)) sPressed = true;
        else sPressed = false;

        if (Input.GetKey(KeyCode.D)) dPressed = true;
        else dPressed = false;

        if (Input.GetMouseButton(0)) m0Pressed = true;
        else m0Pressed = false;

        if (Input.GetKeyDown(KeyCode.Alpha1)) onePressed = true;
        else onePressed = false;

        if (Input.GetKeyDown(KeyCode.Alpha2)) twoPressed = true;
        else twoPressed = false;

        if (Input.GetKeyDown(KeyCode.Alpha3)) threePressed = true;
        else threePressed = false;

        if (Input.GetKeyDown(KeyCode.Alpha4)) fourPressed = true;
        else fourPressed = false;

        if (Input.GetKeyDown(KeyCode.Alpha5)) fivePressed = true;
        else fivePressed = false;

    }
}
