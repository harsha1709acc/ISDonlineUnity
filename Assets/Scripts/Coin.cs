using UnityEngine;

namespace CoinCatcher
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Coin : MonoBehaviour
    {
        public float FallSpeed = 3.5f;

        Rigidbody2D body;
        bool claimed;

        public void Launch(float fallSpeed)
        {
            FallSpeed = fallSpeed;
            claimed = false;
            body = GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.gravityScale = 0f;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        void FixedUpdate()
        {
            if (claimed || GameSession.Instance == null || GameSession.Instance.IsGameOver)
                return;

            body.MovePosition(body.position + Vector2.down * FallSpeed * Time.fixedDeltaTime);

            if (body.position.y < GameSession.Instance.MissY)
                Claim(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (claimed || !other.GetComponent<Catcher>())
                return;

            Claim(true);
        }

        void Claim(bool caught)
        {
            claimed = true;
            GameSession.Instance.ResolveCoin(caught);
            Destroy(gameObject);
        }
    }
}
