using System.IO;
using UnityEngine;


public class PictureMakerComponent : MonoBehaviour
{
#if UNITY_EDITOR
    public Vector2Int resolution = new Vector2Int(1920, 1080);
    public bool showFrame = false;
    public Vector2Int horizontalOffset = Vector2Int.zero;
    public Vector2Int verticalOffset = Vector2Int.zero;

    private Texture2D TakeScreenshotTexture()
    {
        // Создаем рендер-текстуру для сохранения снимка экрана
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        renderTexture.useDynamicScale = true;
        Camera mainCamera = GetComponent<Camera>();
        mainCamera.targetTexture = renderTexture;
        mainCamera.Render();

        // Создаем текстуру для сохранения снимка экрана
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height);
        RenderTexture.active = renderTexture;
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Вырезаем из текстуры только пиксели, находящиеся в рамке
        Texture2D croppedTexture = CropTexture(screenshotTexture);

        // Освобождаем ресурсы
        RenderTexture.active = null;
        mainCamera.targetTexture = null;
        Destroy(renderTexture);
        Destroy(screenshotTexture);

        return croppedTexture;
    }

    private Texture2D CropTexture(Texture2D sourceTexture)
    {
        float scaleX = 1f;// Screen.width / (float)1080;
        float scaleY = Screen.height / (float)1920;

        int width = (int) (resolution.x * scaleX);
        int height = (int)(resolution.y * scaleX);

        // Создаем текстуру для сохранения обрезанного снимка
        Texture2D croppedTexture = new Texture2D(width, height);

        float posX = (Screen.width - width) / 2f + horizontalOffset.x * scaleX;
        float posY = (Screen.height - height) / 2f - verticalOffset.y * scaleX;
        // Определяем координаты и размеры обрезаемой области
        Rect cropRect = new Rect(posX, posY, width, height);

        // Копируем пиксели из исходной текстуры в обрезанную
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                croppedTexture.SetPixel(x, y, sourceTexture.GetPixel((int)cropRect.x + x, (int)cropRect.y + y));
            }
        }
        croppedTexture.Apply();

        return croppedTexture;
    }

    // Метод, вызываемый из контекстного меню для сохранения снимка экрана
    [ContextMenu("TakePicture")]
    void TakePicture()
    {
        Texture2D screenshotTexture = TakeScreenshotTexture();

        // Преобразуем текстуру в массив байтов PNG
        byte[] bytes = screenshotTexture.EncodeToPNG();

        // Сохраняем изображение в файл в папке Assets/Screenshots с уникальным именем
        string filename = $"Assets/Screenshots/Screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        System.IO.File.WriteAllBytes(filename, bytes);

        // Освобождаем ресурсы
        Destroy(screenshotTexture);

        Debug.Log($"Screenshot saved: {filename}");
    }

    private void OnGUI()
    {
        if (showFrame)
        {
            Rect frameRect = new Rect((Screen.width - resolution.x) / 2f + horizontalOffset.x,
                                      (Screen.height - resolution.y) / 2f + verticalOffset.y,
                                      resolution.x, resolution.y);
            DrawFrame(frameRect);
        }
    }

    private void DrawFrame(Rect frameRect)
    {
        float frameWidth = 2f;
        float halfFrameWidth = frameWidth / 2f;
        float xMin = frameRect.x - halfFrameWidth;
        float xMax = frameRect.xMax + halfFrameWidth;
        float yMin = frameRect.y - halfFrameWidth;
        float yMax = frameRect.yMax + halfFrameWidth;

        // Верхняя горизонтальная линия
        GUI.DrawTexture(new Rect(xMin, yMin, frameRect.width + frameWidth, frameWidth), Texture2D.whiteTexture);
        // Нижняя горизонтальная линия
        GUI.DrawTexture(new Rect(xMin, yMax - frameWidth, frameRect.width + frameWidth, frameWidth), Texture2D.whiteTexture);
        // Левая вертикальная линия
        GUI.DrawTexture(new Rect(xMin, yMin, frameWidth, frameRect.height + frameWidth), Texture2D.whiteTexture);
        // Правая вертикальная линия
        GUI.DrawTexture(new Rect(xMax - frameWidth, yMin, frameWidth, frameRect.height + frameWidth), Texture2D.whiteTexture);
    }
#endif
}

