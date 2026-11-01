using UnityEngine;

namespace CoinCatcher
{
    public static class GameBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void StartGame()
        {
            foreach (var session in Object.FindObjectsByType<GameSession>(FindObjectsSortMode.None))
                Object.Destroy(session.gameObject);

            Camera camera = Camera.main;
            if (camera == null)
            {
                var cameraObject = new GameObject("Main Camera");
                camera = cameraObject.AddComponent<Camera>();
                cameraObject.tag = "MainCamera";
            }

            camera.orthographic = true;
            camera.orthographicSize = 5f;
            camera.backgroundColor = new Color(0.07f, 0.12f, 0.22f);
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.transform.position = new Vector3(0f, 0f, -10f);

            var root = new GameObject("Coin Catcher");
            var sessionHost = root.AddComponent<GameSession>();

            Sprite basketSprite = SpriteFactory.RoundedRect(160, 48, 16, new Color(0.25f, 0.78f, 0.86f));
            Sprite coinSprite = SpriteFactory.Circle(64, new Color(1f, 0.84f, 0.18f));

            var catcherObject = new GameObject("Catcher");
            catcherObject.transform.SetParent(root.transform);
            catcherObject.transform.position = new Vector3(0f, -4.1f, 0f);

            var catcherRenderer = catcherObject.AddComponent<SpriteRenderer>();
            catcherRenderer.sprite = basketSprite;
            catcherRenderer.sortingOrder = 3;

            var catcherCollider = catcherObject.AddComponent<BoxCollider2D>();
            catcherCollider.isTrigger = true;
            catcherObject.AddComponent<Rigidbody2D>();
            var catcher = catcherObject.AddComponent<Catcher>();
            catcher.Setup(camera.orthographicSize * camera.aspect);

            var hudObject = new GameObject("HUD");
            hudObject.transform.SetParent(root.transform);
            var hud = hudObject.AddComponent<Hud>();
            hud.Build();

            CreateGround(root.transform, camera);
            sessionHost.Begin(camera, catcher, hud, coinSprite);
        }

        static void CreateGround(Transform parent, Camera camera)
        {
            var ground = new GameObject("Ground");
            ground.transform.SetParent(parent);
            ground.transform.position = new Vector3(0f, -camera.orthographicSize + 0.18f, 0f);

            var renderer = ground.AddComponent<SpriteRenderer>();
            renderer.sprite = SpriteFactory.RoundedRect(512, 24, 4, new Color(0.16f, 0.28f, 0.42f));
            renderer.sortingOrder = 1;
            ground.transform.localScale = new Vector3((camera.orthographicSize * camera.aspect * 2f) / (512f / 64f), 1f, 1f);
        }
    }
}
