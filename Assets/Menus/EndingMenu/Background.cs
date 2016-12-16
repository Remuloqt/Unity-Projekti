using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;

public class Background : MonoBehaviour {
    
    public string loadingGifPath;
    public double speed = 0.1;
    public Vector2 drawPosition;
    List<Texture2D> gifFrames = new List<Texture2D>();

    void Awake()
    {
        //loadingGifPath = "C:/Users/t4luha00/Documents/Unity-Projekti-master/Assets/Canvases/gameover.gif";
        var gifImage = Image.FromFile(loadingGifPath);
        var dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);

        int frameCount = gifImage.GetFrameCount(dimension);
        for (int i = 0; i < frameCount; i++)
        {
            gifImage.SelectActiveFrame(dimension, i);
            var frame = new Bitmap(gifImage.Width, gifImage.Height);
            System.Drawing.Graphics.FromImage(frame).DrawImage(gifImage, Point.Empty);
            var frameTexture = new Texture2D(frame.Width, frame.Height);
            for (int x = 0; x < frame.Width; x++)
                for (int y = 0; y < frame.Height; y++)
                {
                    System.Drawing.Color sourceColor = frame.GetPixel(x, y);
                    frameTexture.SetPixel(x, 1-y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
                }
            frameTexture.Apply();
            gifFrames.Add(frameTexture);
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - gifFrames[0].width / 2, Screen.height / 4, 378, gifFrames[0].height), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
    }//gifFrames[0].width, gifFrames[0].height

}
