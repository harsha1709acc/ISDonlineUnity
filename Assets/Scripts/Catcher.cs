using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinCatcher
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Catcher : MonoBehaviour
    {
        public float MoveSpeed = 9f;

        Rigidbody2D body;
        float halfWidth;
        float limitX;

        public void Setup(float worldHalfWidth)
        {
            body = GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.gravityScale = 0f;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            halfWidth = GetComponent<BoxCollider2D>().bounds.extents.x;
            limitX = worldHalfWidth - halfWidth;
        }

        void FixedUpdate()
        {
            if (GameSession.Instance == null || GameSession.Instance.IsGameOver)
                return;

            float input = ReadMove();
            Vector2 next = body.position + Vector2.right * input * MoveSpeed * Time.fixedDeltaTime;
            next.x = Mathf.Clamp(next.x, -limitX, limitX);
            body.MovePosition(next);
        }

        static float ReadMove()
        {
            float value = 0f;
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                    value -= 1f;
                if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                    value += 1f;
            }

            Gamepad gamepad = Gamepad.current;
            if (gamepad != null)
                value += gamepad.leftStick.x.ReadValue();

            return Mathf.Clamp(value, -1f, 1f);
        }
    }
}
