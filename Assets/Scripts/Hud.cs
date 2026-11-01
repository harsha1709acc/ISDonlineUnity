using UnityEngine;
using UnityEngine.UI;

namespace CoinCatcher
{
    public class Hud : MonoBehaviour
    {
        Text scoreText;
        Text livesText;
        Text overlayText;

        public void Build()
        {
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            gameObject.AddComponent<GraphicRaycaster>();

            scoreText = CreateLabel("Score", new Vector2(40, -30), new Vector2(0, 1), TextAnchor.UpperLeft, 48);
            livesText = CreateLabel("Lives", new Vector2(-40, -30), new Vector2(1, 1), TextAnchor.UpperRight, 48);
            overlayText = CreateLabel("Overlay", Vector2.zero, new Vector2(0.5f, 0.5f), TextAnchor.MiddleCenter, 56);
            overlayText.rectTransform.sizeDelta = new Vector2(1200, 400);
            overlayText.gameObject.SetActive(false);

            CreateLabel("Move with A/D or arrows", new Vector2(0, 36), new Vector2(0.5f, 0), TextAnchor.LowerCenter, 28);
        }

        public void SetScore(int score)
        {
            scoreText.text = $"Score  {score}";
        }

        public void SetLives(int lives)
        {
            livesText.text = $"Lives  {lives}";
        }

        public void ShowGameOver(int score)
        {
            overlayText.gameObject.SetActive(true);
            overlayText.text = $"Game Over\nScore {score}\nPress Space or click to play again";
        }

        public void HideOverlay()
        {
            overlayText.gameObject.SetActive(false);
        }

        Text CreateLabel(string name, Vector2 offset, Vector2 anchor, TextAnchor alignment, int fontSize)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(transform, false);

            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.pivot = anchor;
            rect.anchoredPosition = offset;
            rect.sizeDelta = new Vector2(700, 80);

            var text = go.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = Color.white;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            return text;
        }
    }
}
