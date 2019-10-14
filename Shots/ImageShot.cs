using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class ImageShot : MonoBehaviour
{
    private Object[] images;
    private Text currentImageText;
    private InputField DivideByInput;
    private int imageDivided = 6;
    public BallShooter ballShooter;

    // Use this for initialization
    private void Awake()
    {
        images = Resources.LoadAll("Textures", typeof(Texture2D));
    }

    void Start()
    {
        ballShooter = GameObject.Find("BallShooter").GetComponent<BallShooter>();
//        DivideByInput = GameObject.Find("ImageDivideBy").GetComponent<InputField>();

        //currentImageText = GameObject.Find("CurrentImageText").GetComponent<Text>();

        foreach (Object t in images)
        {
            //Debug.Log(t.name);
        }

        //DivideByInput.text = Static.imageDivideBy.ToString();
        //Static.currentImage = (Texture2D)images[(Static.imgIndex % images.Length)];
        //currentImageText.text = images[Static.imgIndex].name;
    }

    // Update is called once per frame

    public void switchImage()
    {
        //Static.imgIndex++;
        //Debug.Log(Static.imgIndex % images.Length + "asdf");
        //currentImageText.text = images[Static.imgIndex % images.Length].name + "  =>";
        //Static.currentImage = (Texture2D)images[Static.imgIndex % images.Length];

    }

    public void dividePixelsBy()
    {
        int.TryParse(DivideByInput.text, out imageDivided);
    }

    public void fire()
    {
        if (images.Length > 0) {
            fireImage((Texture2D)images[0], imageDivided);
        }
    }


    public void fireImage(Texture2D image, int divideSizeBy)
    {

        List<ColoredBallVect> newBalls = new List<ColoredBallVect>();
        Color[] pixels = image.GetPixels(0, 0, image.width, image.height);

        //Debug.Log(String.Format("image w {0}, image h {1}", image.width, image.height));

        for (int i = 0; i < image.width; i += divideSizeBy)
        {
            for (int j = 0; j < image.height; j += divideSizeBy)
            {

                //Debug.Log("i = " + i + "j = " + j);
                Color ballColor = pixels[(i * image.height) + j];
                if (ballColor != Color.clear)
                {
                    float3 addedVect = new float3(j / divideSizeBy - (image.width / divideSizeBy) / 2,
                        (i / divideSizeBy) - (image.height / divideSizeBy) / 2, 0);
                        
                    newBalls.Add(new ColoredBallVect { vect = addedVect, color = ballColor });
                }
            }
        }
        ballShooter.fireBallGroup(newBalls);
    }
}



