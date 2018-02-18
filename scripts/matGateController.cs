using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[CustomEditor(typeof(matGateController))]
public class matGateController : MonoBehaviour
{

    [ExecuteInEditMode]
    [Header("Primary Texture Manipulation Controls")]
    [SerializeField]
    private Texture2D sourceTexture; // source texture to use
    [SerializeField] [Range(0, +100)] private float warpAmount = 100f; // warp
    [SerializeField] [Range(0, +100)] private float warpXAmount = 0.01f; // warp x
    [SerializeField] [Range(0, +100)] private float warpYAmount = 0.01f; // warp y
    [SerializeField] [Range(0, +100)] private float warpWidthXAmount; // warp x width
    [SerializeField] [Range(0, +100)] private float warpHeightYAmount; // warp y width

    private float warpXFrac; // variables to store the settings
    private float warpYFrac;
    private float xFrac;
    private float yFrac;

    [SerializeField] private Texture2D destinationTexture; // destination texture that will be modified with changes from the source tex
    private Color[] destPix; // color array for pixel changes
    private float checkChange; // check if value was changed
    [SerializeField] private bool allowChanges = true; // it was changed
    [SerializeField] private bool lockChanges = false; // lock changes from being made to the texture in edit and runtime
    [SerializeField] private bool rotateTexture; // animate texture
    [SerializeField] private Vector2 rotateUVAnimationRate = new Vector2(1.0f, 0.0f);
    private string textureName = "_MainTex"; // to access the shader texture properties
    private Vector2 uvOffset = Vector2.zero; // offset UV at runtime

    //mode7 (from : https://github.com/KuboS0S/Mode7Shader)
    [HideInInspector] public AnimationCurve animationCurve;
    [HideInInspector] public float animationTime = 0.5f;
    [HideInInspector] public float timeStep = 0.05f;
    [HideInInspector] public float animationStartTime;
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
    [HideInInspector] public List<Mode7Config> configs = new List<Mode7Config>();
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
    [ExecuteInEditMode]
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



            destPix = new Color[destinationTexture.width * destinationTexture.height]; // set the destpix array to be the w and h of the destination texture
            int y = 0; // iterate y and x all accross the texture until the end
            while (y < destinationTexture.height)
            {
                int x = 0;
                while (x < destinationTexture.width) // apply animation to the texture
                {
                    xFrac = x * warpXAmount / (destinationTexture.width - warpWidthXAmount); // warp x
                    yFrac = y * warpYAmount / (destinationTexture.height - warpHeightYAmount); // warp y
                    warpXFrac = Mathf.Sin(xFrac * warpAmount); // apply Sin wave to x
                    warpYFrac = Mathf.Sin(yFrac * warpAmount); // apply sin wave to y

                    destPix[y * destinationTexture.width + x] = sourceTexture.GetPixelBilinear(warpXFrac, warpYFrac); // for the entire texture, apply the warping
                    x++; // iterate x
                    warpXAmount = warpXAmount; // ?
                    warpYAmount = warpYAmount;

                }
                y++; // iterate y
            }
            destinationTexture.SetPixels(destPix); // set the new pixels on the destination texture
            destinationTexture.Apply(); // apply them
            GetComponent<Renderer>().material.mainTexture = destinationTexture; // set the texture to the material

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



            destPix = new Color[destinationTexture.width * destinationTexture.height];
            int y = 0;
            while (y < destinationTexture.height)
            {
                int x = 0;
                while (x < destinationTexture.width)
                {
                    float xFrac = x * warpXAmount / (destinationTexture.width - warpWidthXAmount);
                    float yFrac = y * warpYAmount / (destinationTexture.height - warpHeightYAmount);
                    float warpXFrac = Mathf.Sin(xFrac * warpAmount + Time.deltaTime);
                    float warpYFrac = Mathf.Sin(yFrac * warpAmount + Time.deltaTime);

                    destPix[y * destinationTexture.width + x] = sourceTexture.GetPixelBilinear(warpXFrac, warpYFrac);
                    x++;
                }
                y++;
            }
            destinationTexture.SetPixels(destPix);
            destinationTexture.Apply();
            GetComponent<Renderer>().material.mainTexture = destinationTexture;

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
            float r_warpAmount = Random.Range(0, +100);
            float r_warpWidthXAmount = Random.Range(0, +100);
            float r_warpHeightYAmount = Random.Range(0, +100);
            float r_warpXAmount = Random.Range(0, +100);
            float r_warpYAmount = Random.Range(0, +100);


            destPix = new Color[destinationTexture.width * destinationTexture.height];
            int r_y = 0;
            while (r_y < destinationTexture.height)
            {
                int r_x = 0;
                while (r_x < destinationTexture.width)
                {
                    r_xFrac = r_x * r_warpXAmount / (destinationTexture.width - r_warpWidthXAmount);
                    r_yFrac = r_y * r_warpYAmount / (destinationTexture.height - r_warpHeightYAmount);
                    r_warpXFrac = Mathf.Sin(r_xFrac * r_warpAmount + Time.deltaTime);
                    r_warpYFrac = Mathf.Sin(r_yFrac * r_warpAmount + Time.deltaTime);

                    destPix[r_y * destinationTexture.width + r_x] = sourceTexture.GetPixelBilinear(r_warpXFrac, r_warpYFrac);
                    r_x++;

                }
                r_y++;
            }
            destinationTexture.SetPixels(destPix);
            destinationTexture.Apply();
            GetComponent<Renderer>().material.mainTexture = destinationTexture;



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
            float pr_warpAmount = Random.Range(0, +100);
            float pr_warpWidthXAmount = Random.Range(0, +100);
            float pr_warpHeightYAmount = Random.Range(0, +100);
            float pr_warpXAmount = Random.Range(0, +100);
            float pr_warpYAmount = Random.Range(0, +100);


            destPix = new Color[destinationTexture.width * destinationTexture.height];
            int r_y = 0; // iterate y
            while (r_y < destinationTexture.height)
            {
                int r_x = 0;
                while (r_x < destinationTexture.width)
                {
                    pr_xFrac = Random.Range(0, destinationTexture.width); // randomly place the pixel on width of texture
                    pr_yFrac = Random.Range(0, destinationTexture.height); // randomly place the pixel on height of texture
                    pr_warpXFrac = Mathf.Sin(pr_xFrac * pr_warpAmount * Time.deltaTime); // sin wave
                    pr_warpYFrac = Mathf.Sin(pr_yFrac * pr_warpAmount * Time.deltaTime); // sin wave
                    destPix[r_y * destinationTexture.width + r_x] = sourceTexture.GetPixelBilinear(pr_warpXFrac, pr_warpYFrac);
                    r_x++; // iterate x

                }
                r_y++; // iterate y
            }
            destinationTexture.SetPixels(destPix);
            destinationTexture.Apply();
            GetComponent<Renderer>().material.mainTexture = destinationTexture;




        }
    }

    public void pushGate_randomSmall()
    {

        if (allowChanges && lockChanges == false)
        {

            float r_xFrac = Random.Range(0, +25);
            float r_yFrac = Random.Range(0, +25);

            float r_warpAmount = Random.Range(0, +25);
            float r_warpWidthXAmount = Random.Range(0, +25);
            float r_warpHeightYAmount = Random.Range(0, +25);
            float r_warpXAmount = Random.Range(0, +25);
            float r_warpYAmount = Random.Range(0, +25);


            destPix = new Color[destinationTexture.width * destinationTexture.height];
            int r_y = 0;
            while (r_y < destinationTexture.height)
            {
                int r_x = 0;
                while (r_x < destinationTexture.width)
                {
                    r_xFrac = r_x * r_warpXAmount / (destinationTexture.width - r_warpWidthXAmount);
                    r_yFrac = r_y * r_warpYAmount / (destinationTexture.height - r_warpHeightYAmount);
                    float r_warpXFrac = Mathf.Sin(r_xFrac * r_warpAmount + Time.deltaTime);
                    float r_warpYFrac = Mathf.Sin(r_yFrac * r_warpAmount + Time.deltaTime);

                    destPix[r_y * destinationTexture.width + r_x] = sourceTexture.GetPixelBilinear(r_warpXFrac, r_warpYFrac);
                    r_x++;

                }
                r_y++;
            }
            destinationTexture.SetPixels(destPix);
            destinationTexture.Apply();
            GetComponent<Renderer>().material.mainTexture = destinationTexture;



        }


    }



    public void pushGate_save(string saveName)
    {


        Texture2D destinationTextureCopy = new Texture2D(destinationTexture.width, destinationTexture.height, TextureFormat.RGBA32, false); // prepare a new texture to copy contents that were generated
        Graphics.CopyTexture(destinationTexture, 0, 0, destinationTextureCopy, 0, 0); // copy the texture

        destinationTextureCopy.Apply(); // apply it
        byte[] bytes = destinationTextureCopy.EncodeToPNG(); // encode the bytes to PNG
        File.WriteAllBytes("Assets/!materialGate/" + saveName + ".png", bytes); // save the file
        DestroyImmediate(destinationTextureCopy); // destroy the copy
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

