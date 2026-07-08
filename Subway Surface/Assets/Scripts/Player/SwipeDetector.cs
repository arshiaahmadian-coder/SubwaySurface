using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetector : MonoBehaviour
{
    Vector2 startPosition;
    bool isSwiping;
    public PlayerController player;

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            startPosition = touch.position.ReadValue();
            isSwiping = true;
        }

        if (isSwiping && touch.press.isPressed)
        {
            Vector2 currentPosition = touch.position.ReadValue();

            Vector2 direction = currentPosition - startPosition;

            if (direction.magnitude >= SettingManager.singleton.swipeThreshold)
            {
                DetectDirection(direction);

                isSwiping = false;
            }
        }

        if (touch.press.wasReleasedThisFrame)
        {
            isSwiping = false;
        }
    }

    void DetectDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                player.TryToMoveRight();
            else
                player.TryToMoveLeft();
        }
        else
        {
            if (direction.y > 0)
                player.TryToJump();
            else
                player.TryToRoll();
        }
    }
}
