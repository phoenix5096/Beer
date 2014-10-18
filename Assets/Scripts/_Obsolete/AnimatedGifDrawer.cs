using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;
 
public class AnimatedGifDrawer : MonoBehaviour
{
    public string loadingGifPath;
    public float speed = 0.075f;
    public Vector2 drawPosition;
	public Vector2 drawScale;
	public bool autoCenterAndScale = true;

    List<Texture2D> gifFrames = new List<Texture2D>();
    void Awake()
    {
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
					frameTexture.SetPixel(x,frame.Height - 1 - y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, y is flipped
                }
            frameTexture.Apply();
            gifFrames.Add(frameTexture);
   		}
	}
 
    void OnGUI()
    {
		//TODO: this is only required if the screen is resizable, we can calculate this only once on Load if we don't care about resizing
		if (autoCenterAndScale) 
		{
			float calculatedXratio = Screen.width/gifFrames[0].width;
			float calculatedYratio = Screen.height/gifFrames[0].width;
			float minRatio = Mathf.Min (calculatedXratio,calculatedYratio);
			
			drawScale.x = minRatio;
			drawScale.y = minRatio;
			drawPosition.x = (Screen.width/2) - ((gifFrames[0].width/2) * drawScale.x);
			drawPosition.y = (Screen.height/2) - ((gifFrames[0].height/2) * drawScale.y);
		}

		GUI.DrawTexture(new Rect(drawPosition.x, drawPosition.y, (int)(gifFrames[0].width * drawScale.x), (int)(gifFrames[0].height * drawScale.y)), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
	}
}