using UnityEngine;

namespace CoinCatcher
{
    public static class SpriteFactory
    {
        public static Sprite Circle(int size, Color color)
        {
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.filterMode = FilterMode.Bilinear;
            var pixels = new Color[size * size];
            float radius = (size - 1) * 0.5f;
            Vector2 center = new Vector2(radius, radius);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), center);
                    float alpha = Mathf.Clamp01(radius - distance + 0.5f);
                    Color pixel = color;
                    pixel.a *= alpha;
                    pixels[y * size + x] = pixel;
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        public static Sprite RoundedRect(int width, int height, int radius, Color color)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.filterMode = FilterMode.Bilinear;
            var pixels = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float alpha = CornerAlpha(x, y, width, height, radius);
                    Color pixel = color;
                    pixel.a *= alpha;
                    pixels[y * width + x] = pixel;
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 64f);
        }

        static float CornerAlpha(int x, int y, int width, int height, int radius)
        {
            bool left = x < radius;
            bool right = x >= width - radius;
            bool bottom = y < radius;
            bool top = y >= height - radius;

            if (!((left || right) && (bottom || top)))
                return 1f;

            float cx = left ? radius : width - radius - 1;
            float cy = bottom ? radius : height - radius - 1;
            float distance = Vector2.Distance(new Vector2(x, y), new Vector2(cx, cy));
            return Mathf.Clamp01(radius - distance + 0.5f);
        }
    }
}
