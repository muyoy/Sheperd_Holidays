using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderT : MonoBehaviour
{
    public Camera renderCamera;
    private SpriteRenderer slotImage;
    public Sprite sprite;
    void Start()
    {
        //RenderTexture renderTexture = new RenderTexture(512, 512, 0);
        //renderCamera.targetTexture = renderTexture;
        slotImage = gameObject.GetComponent<SpriteRenderer>();
        Texture2D modelView = RTImage(renderCamera);
        sprite = Sprite.Create(modelView, new Rect(0, 0, modelView.width, modelView.height), new Vector2(0.5f, 0.5f));
        sprite.name = modelView.name;
        slotImage.sprite = sprite;
    }
    //private void Update()
    //{
    //    Texture2D modelView = RTImage(renderCamera);
    //    sprite = Sprite.Create(modelView, new Rect(0, 0, modelView.width, modelView.height), new Vector2(0.5f, 0.5f));
    //    sprite.name = modelView.name;
    //    slotImage.sprite = sprite;
    //}
    private Texture2D RTImage(Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
}
