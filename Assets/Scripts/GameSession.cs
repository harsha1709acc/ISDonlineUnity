using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinCatcher
{
    public class GameSession : MonoBehaviour
    {
        public static GameSession Instance { get; private set; }

        public bool IsGameOver { get; private set; }
        public float MissY { get; private set; } = -5.4f;

        const int StartingLives = 3;

        Catcher catcher;
        Hud hud;
        Sprite coinSprite;
        Camera mainCamera;
        float worldHalfWidth;
        float spawnTimer;
        int score;
        int lives;

        public void Begin(Camera camera, Catcher player, Hud ui, Sprite goldCoin)
        {
            Instance = this;
            mainCamera = camera;
            catcher = player;
            hud = ui;
            coinSprite = goldCoin;
            worldHalfWidth = camera.orthographicSize * camera.aspect;
            MissY = -camera.orthographicSize - 0.6f;
            Restart();
        }

        void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        void Update()
        {
            if (IsGameOver)
            {
                if (RestartPressed())
                    Restart();
                return;
            }

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnCoin();
                spawnTimer = Mathf.Max(0.35f, 1.05f - score * 0.012f);
            }
        }

        public void ResolveCoin(bool caught)
        {
            if (IsGameOver)
                return;

            if (caught)
            {
                score += 10;
                hud.SetScore(score);
                return;
            }

            lives -= 1;
            hud.SetLives(lives);
            if (lives <= 0)
                EndGame();
        }

        void Restart()
        {
            foreach (var coin in FindObjectsByType<Coin>(FindObjectsSortMode.None))
                Destroy(coin.gameObject);

            IsGameOver = false;
            score = 0;
            lives = StartingLives;
            spawnTimer = 0.6f;
            catcher.transform.position = new Vector3(0f, -mainCamera.orthographicSize + 0.9f, 0f);
            hud.HideOverlay();
            hud.SetScore(score);
            hud.SetLives(lives);
        }

        void EndGame()
        {
            IsGameOver = true;
            hud.ShowGameOver(score);
        }

        void SpawnCoin()
        {
            float x = Random.Range(-worldHalfWidth + 0.4f, worldHalfWidth - 0.4f);
            float y = mainCamera.orthographicSize + 0.4f;
            var go = new GameObject("Coin");
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(x, y, 0f);

            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = coinSprite;
            renderer.sortingOrder = 2;

            var collider = go.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = 0.45f;

            go.AddComponent<Rigidbody2D>();
            var coin = go.AddComponent<Coin>();
            float fallSpeed = 3.2f + score * 0.04f;
            coin.Launch(fallSpeed);
        }

        static bool RestartPressed()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
                return true;

            Mouse mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.wasPressedThisFrame)
                return true;

            Gamepad gamepad = Gamepad.current;
            return gamepad != null && gamepad.buttonSouth.wasPressedThisFrame;
        }
    }
}
