using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class matGateController : MonoBehaviour
{

    [ExecuteInEditMode]

    [SerializeField]
    private Texture2D sourceTex; // source texture to use
    [SerializeField] [Range(0, +100)] private float warpFactor = 100f; // warp
    [SerializeField] [Range(0, +100)] private float xFracX = 0.01f; // warp x
    [SerializeField] [Range(0, +100)] private float yFracY = 0.01f; // warp y
    [SerializeField] [Range(0, +100)] private float xFracXw; // warp x width
    [SerializeField] [Range(0, +100)] private float yFracYh; // warp y width

    private float warpXFrac; // variables to store the settings
    private float warpYFrac;
    private float xFrac;
    private float yFrac;


    [SerializeField] private Texture2D destTex; // destination texture that will be modified with changes from the source tex
    private Color[] destPix; // color array for pixel changes
    private float checkChange; // check if value was changed
    [SerializeField] private bool allowChanges = true; // it was changed
    [SerializeField] private bool lockChanges = false; // lock changes from being made to the texture in edit and runtime
    [SerializeField] private bool rotateTexture; // animate texture
    [SerializeField] private Vector2 rotateUVAnimationRate = new Vector2(1.0f, 0.0f);
    private string textureName = "_MainTex"; // to access the shader texture properties
    private Vector2 uvOffset = Vector2.zero; // offset UV at runtime


    void Awake()
    {


    }

    void Start()
    {




    }



    void Update()
    {

        if (allowChanges && lockChanges == false) // if not locked
        {



            destPix = new Color[destTex.width * destTex.height]; // set the destpix array to be the w and h of the destination texture
            int y = 0; // iterate y and x all accross the texture until the end
            while (y < destTex.height)
            {
                int x = 0;
                while (x < destTex.width) // apply animation to the texture
                {
                    xFrac = x * xFracX / (destTex.width - xFracXw); // warp x
                    yFrac = y * yFracY / (destTex.height - yFracYh); // warp y
                    warpXFrac = Mathf.Sin(xFrac * warpFactor); // apply Sin wave to x
                    warpYFrac = Mathf.Sin(yFrac * warpFactor); // apply sin wave to y

                    destPix[y * destTex.width + x] = sourceTex.GetPixelBilinear(warpXFrac, warpYFrac); // for the entire texture, apply the warping
                    x++; // iterate x
                    xFracX = xFracX; // ?
                    yFracY = yFracY;

                }
                y++; // iterate y
            }
            destTex.SetPixels(destPix); // set the new pixels on the destination texture
            destTex.Apply(); // apply them
            GetComponent<Renderer>().material.mainTexture = destTex; // set the texture to the material

        }
        if (rotateTexture) // if rotating is enabled
        {

            uvOffset += (rotateUVAnimationRate * Time.deltaTime); // offet the UV as set in the inspector x, y
            float offset = Time.time * 3;
            if (rotateTexture)
            {
                GetComponent<Renderer>().material.SetTextureOffset("_MainTex", uvOffset); // apply offset to the material

            }



        }




    }



    public void pushGate()
    {

        if (allowChanges && lockChanges == false)
        {



            destPix = new Color[destTex.width * destTex.height];
            int y = 0;
            while (y < destTex.height)
            {
                int x = 0;
                while (x < destTex.width)
                {
                    float xFrac = x * xFracX / (destTex.width - xFracXw);
                    float yFrac = y * yFracY / (destTex.height - yFracYh);
                    float warpXFrac = Mathf.Sin(xFrac * warpFactor + Time.deltaTime);
                    float warpYFrac = Mathf.Sin(yFrac * warpFactor + Time.deltaTime);

                    destPix[y * destTex.width + x] = sourceTex.GetPixelBilinear(warpXFrac, warpYFrac);
                    x++;
                }
                y++;
            }
            destTex.SetPixels(destPix);
            destTex.Apply();
            GetComponent<Renderer>().material.mainTexture = destTex;

        }
    }




    public void pushGate_random()
    {

        if (allowChanges && lockChanges == false)
        {

            float r_xFrac = Random.Range(0, +100); // set random ranges for warping
            float r_yFrac = Random.Range(0, +100);
            float r_warpXFrac = Random.Range(0, +100);
            float r_warpYFrac = Random.Range(0, +100);
            float r_warpFactor = Random.Range(0, +100);
            float r_xFracXw = Random.Range(0, +100);
            float r_yFracYh = Random.Range(0, +100);
            float r_xFracX = Random.Range(0, +100);
            float r_yFracY = Random.Range(0, +100);


            destPix = new Color[destTex.width * destTex.height];
            int r_y = 0;
            while (r_y < destTex.height)
            {
                int r_x = 0;
                while (r_x < destTex.width)
                {
                    r_xFrac = r_x * r_xFracX / (destTex.width - r_xFracXw);
                    r_yFrac = r_y * r_yFracY / (destTex.height - r_yFracYh);
                    r_warpXFrac = Mathf.Sin(r_xFrac * r_warpFactor + Time.deltaTime);
                    r_warpYFrac = Mathf.Sin(r_yFrac * r_warpFactor + Time.deltaTime);

                    destPix[r_y * destTex.width + r_x] = sourceTex.GetPixelBilinear(r_warpXFrac, r_warpYFrac);
                    r_x++;

                }
                r_y++;
            }
            destTex.SetPixels(destPix);
            destTex.Apply();
            GetComponent<Renderer>().material.mainTexture = destTex;



        }
    }

    public void pushGate_randomPixel()
    {

        if (allowChanges && lockChanges == false)
        {

            float pr_xFrac = Random.Range(0, +100); // set random ranges for warping
            float pr_yFrac = Random.Range(0, +100);
            float pr_warpXFrac = Random.Range(0, +100);
            float pr_warpYFrac = Random.Range(0, +100);
            float pr_warpFactor = Random.Range(0, +100);
            float pr_xFracXw = Random.Range(0, +100);
            float pr_yFracYh = Random.Range(0, +100);
            float pr_xFracX = Random.Range(0, +100);
            float pr_yFracY = Random.Range(0, +100);


            destPix = new Color[destTex.width * destTex.height];
            int r_y = 0; // iterate y
            while (r_y < destTex.height)
            {
                int r_x = 0;
                while (r_x < destTex.width)
                {
                    pr_xFrac = Random.Range(0, destTex.width); // randomly place the pixel on width of texture
                    pr_yFrac = Random.Range(0, destTex.height); // randomly place the pixel on height of texture
                    pr_warpXFrac = Mathf.Sin(pr_xFrac * pr_warpFactor * Time.deltaTime); // sin wave
                    pr_warpYFrac = Mathf.Sin(pr_yFrac * pr_warpFactor * Time.deltaTime); // sin wave
                    destPix[r_y * destTex.width + r_x] = sourceTex.GetPixelBilinear(pr_warpXFrac, pr_warpYFrac);
                    r_x++; // iterate x

                }
                r_y++; // iterate y
            }
            destTex.SetPixels(destPix);
            destTex.Apply();
            GetComponent<Renderer>().material.mainTexture = destTex;




        }
    }

    public void pushGate_randomSmall()
    {

        if (allowChanges && lockChanges == false)
        {

            float r_xFrac = Random.Range(0, +25);
            float r_yFrac = Random.Range(0, +25);

            float r_warpFactor = Random.Range(0, +25);
            float r_xFracXw = Random.Range(0, +25);
            float r_yFracYh = Random.Range(0, +25);
            float r_xFracX = Random.Range(0, +25);
            float r_yFracY = Random.Range(0, +25);


            destPix = new Color[destTex.width * destTex.height];
            int r_y = 0;
            while (r_y < destTex.height)
            {
                int r_x = 0;
                while (r_x < destTex.width)
                {
                    r_xFrac = r_x * r_xFracX / (destTex.width - r_xFracXw);
                    r_yFrac = r_y * r_yFracY / (destTex.height - r_yFracYh);
                    float r_warpXFrac = Mathf.Sin(r_xFrac * r_warpFactor + Time.deltaTime);
                    float r_warpYFrac = Mathf.Sin(r_yFrac * r_warpFactor + Time.deltaTime);

                    destPix[r_y * destTex.width + r_x] = sourceTex.GetPixelBilinear(r_warpXFrac, r_warpYFrac);
                    r_x++;

                }
                r_y++;
            }
            destTex.SetPixels(destPix);
            destTex.Apply();
            GetComponent<Renderer>().material.mainTexture = destTex;



        }


    }



    public void pushGate_save(string saveName)
    {


        Texture2D destTexCopy = new Texture2D(destTex.width, destTex.height, TextureFormat.RGBA32, false); // prepare a new texture to copy contents that were generated
        Graphics.CopyTexture(destTex, 0, 0, destTexCopy, 0, 0); // copy the texture

        destTexCopy.Apply(); // apply it
        byte[] bytes = destTexCopy.EncodeToPNG(); // encode the bytes to PNG
        File.WriteAllBytes("Assets/!materialGate/" + saveName + ".png", bytes); // save the file
        DestroyImmediate(destTexCopy); // destroy the copy
        AssetDatabase.Refresh(); // refresh assets



    }



}
