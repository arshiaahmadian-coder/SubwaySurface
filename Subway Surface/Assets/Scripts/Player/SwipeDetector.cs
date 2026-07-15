using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetector : MonoBehaviour
{
    Vector2 startPosition;
    bool isTouching;
    bool isHolding;
    private float holdTimer;
    public PlayerController player;

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            startPosition = touch.position.ReadValue();
            isTouching = true;
        }

        if (isTouching && touch.press.isPressed)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= SettingManager.singleton.holdThreshold)
            {
                if (isHolding == false) player.TryToSprint();
                isHolding = true;
            }

            Vector2 currentPosition = touch.position.ReadValue();

            Vector2 direction = currentPosition - startPosition;

            if (direction.magnitude >= SettingManager.singleton.swipeThreshold)
            {
                if (isHolding == true) player.ResetSprint();
                holdTimer = 0;
                isTouching = false;
                isHolding = false;
                DetectDirection(direction);
            }
        }

        if (touch.press.wasReleasedThisFrame)
        {
            if (isHolding == true) player.ResetSprint();
            holdTimer = 0;
            isTouching = false;
            isHolding = false;
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
