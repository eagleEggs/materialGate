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

    //mode7 (from : https://github.com/KuboS0S/Mode7Shader)
    public AnimationCurve animationCurve;
    public float animationTime = 0.5f;
    public float timeStep = 0.05f;
    public float animationStartTime;
    private Material _material;

    void Awake()
    {


    }

    void Start()
    {




    }


    public Material Material
    {
        get
        {
            if (!_material)
            {
                _material = GetComponent<Renderer>().material;
            }
            return _material;
        }
    }
    public List<Mode7Config> configs = new List<Mode7Config>();
    public Mode7Config InterpolateFromTo(Mode7Config config1, Mode7Config config2, float time)
    {
        float value = animationCurve.Evaluate(time);

        return (config2 - config1) * value + config1;
    }

    public Mode7Config GetConfig()
    {
        return new Mode7Config(
            Material.GetFloat("_H"),
            Material.GetFloat("_V"),
            Material.GetFloat("_X0"),
            Material.GetFloat("_Y0"),
            Material.GetFloat("_A"),
            Material.GetFloat("_B"),
            Material.GetFloat("_C"),
            Material.GetFloat("_D")
            );
    }
    public void SetConfig(Mode7Config config)
    {
        Material.SetFloat("_H", config.h);
        Material.SetFloat("_V", config.v);
        Material.SetFloat("_X0", config.x0);
        Material.SetFloat("_Y0", config.y0);
        Material.SetFloat("_A", config.a);
        Material.SetFloat("_B", config.b);
        Material.SetFloat("_C", config.c);
        Material.SetFloat("_D", config.d);
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
    //mode7:
    [System.Serializable]
    public struct Mode7Config
    {
        public float h;
        public float v;
        public float x0;
        public float y0;

        public float a;
        public float b;
        public float c;
        public float d;

        public Mode7Config(float h, float v, float x0, float y0, float a, float b, float c, float d)
        {
            this.h = h;
            this.v = v;
            this.x0 = x0;
            this.y0 = y0;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public static Mode7Config operator *(Mode7Config config, float mult)
        {
            return new Mode7Config(
                config.h * mult,
                config.v * mult,
                config.x0 * mult,
                config.y0 * mult,
                config.a * mult,
                config.b * mult,
                config.c * mult,
                config.d * mult
                );
        }

        public static Mode7Config operator +(Mode7Config config1, Mode7Config config2)
        {
            return new Mode7Config(
                config1.h + config2.h,
                config1.v + config2.v,
                config1.x0 + config2.x0,
                config1.y0 + config2.y0,
                config1.a + config2.a,
                config1.b + config2.b,
                config1.c + config2.c,
                config1.d + config2.d
                );
        }

        public static Mode7Config operator -(Mode7Config config1, Mode7Config config2)
        {
            return new Mode7Config(
                config1.h - config2.h,
                config1.v - config2.v,
                config1.x0 - config2.x0,
                config1.y0 - config2.y0,
                config1.a - config2.a,
                config1.b - config2.b,
                config1.c - config2.c,
                config1.d - config2.d
                );
        }

        public static bool operator ==(Mode7Config c1, Mode7Config c2)
        {
            return
                c1.h == c2.h &&
                c1.v == c2.v &&
                c1.x0 == c2.x0 &&
                c1.y0 == c2.y0 &&
                c1.a == c2.a &&
                c1.b == c2.b &&
                c1.c == c2.c &&
                c1.d == c2.d;
        }

        public static bool operator !=(Mode7Config c1, Mode7Config c2)
        {
            return !(c1 == c2);
        }

        public override string ToString()
        {
            return string.Format("Offset: ({0:00.00}, {1:00.00}), Pivot: ({2:00.00}, {3:00.00}), ABCD: ({4:00.00}, {5:00.00}, {6:00.00}, {7:00.00})", h, v, x0, y0, a, b, c, d);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    

    }

