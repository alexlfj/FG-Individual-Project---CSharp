using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField] float deadzone = 0.2f;
    

    void Update()
    {
        if (Mouse.current.delta.ReadValue() != Vector2.zero || Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.visible = true;
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftStick.ReadValue().magnitude > deadzone || Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                Cursor.visible = false;
            }
        }
    }
}
