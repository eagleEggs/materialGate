using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class matGateController : MonoBehaviour {

    [ExecuteInEditMode]

    [SerializeField]                        private Texture2D   sourceTex;
    [SerializeField]    [Range(0, +100)]    private float       warpFactor=100f;
    [SerializeField]    [Range(0, +100)]    private float       xFracX = 0.01f;
    [SerializeField]    [Range(0, +100)]    private float       yFracY = 0.01f;
    [SerializeField]    [Range(0, +100)]    private float       xFracXw;
    [SerializeField]    [Range(0, +100)]    private float       yFracYh;
                                            private float       warpXFrac;
                                            private float       warpYFrac;
                                            private float       xFrac;
                                            private float       yFrac;


    [SerializeField]                        private Texture2D   destTex;
                                            public  Texture2D   destTex2;
                                            private Color[]     destPix;
                                            private float       checkChange;
    [SerializeField]                        private bool        changedValue=true;
    [SerializeField]                        private bool        lockChanges=false;

                                            private AnimationCurve curveX;
                                            private AnimationCurve curveY;
                                            private AnimationCurve curveZ;
                                            public  bool useCurves = false;
                                            public  bool enableFlow = false;
                                            public bool rotateTexture;
                                            public bool parallaxTexture;
                                            public float rotateSpeed = 30f;

                                            public int materialIndex = 0;
                                            public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
                                            public string textureName = "_MainTex";
                                            public Vector2 uvOffset = Vector2.zero;
                                            private Object[] textures;
                                            public Rect sourceRect;
                                            private float csColorTotal;
                                            private Color[] finalList;
                                            //public Color[] colorArray;
                                            


    void Awake(){

        
    }

    void Start() {

        // your textures to add as sources must be in Assets/Resources/!materialGate/
        // these may be overridden, be careful!
        textures = Resources.LoadAll("!materialGate", typeof(Texture2D));
        

        
        
    }

    public void palletize(){

        // gather all colors from the texture and remove duplicates to create color map / pallette:
        List<Color> colrs = new List<Color>();


        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        foreach (Color cs in pix){

            if (pix.Contains(cs)){
                Debug.Log("Skipping Color " + cs + " as it already Exists, making White");
                colrs.Remove(cs);
            } else { colrs.Add(cs);}

        }
        Color[] cols = colrs.ToArray();
        Color[] cols2 = sourceTex.GetPixels(x, y, width, height);
        destTex = new Texture2D(width, height);
        destTex.SetPixels(cols2);
        destTex.Apply();


    }

public void pixellate(){ // pixellate

        // gather all colors from the texture and remove duplicates to create color map / pallette:
        List<Color> colrs = new List<Color>();
        List<Color> colorArray = new List<Color>();
        List<float> colorSort = new List<float>();
        Dictionary <float, Color> myColors = new Dictionary <float, Color>();
        List<Color> colorsFinal = new List<Color>();

        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        
        float n = 0;
        string cnum = n.ToString();
        foreach (Color cs in pix){
            
            float csr = cs.r;
            float csg = cs.g;
            float csb = cs.b;
            float csa = cs.a;
            //string colorString = cs.ToString();
            csColorTotal = csr+csg+csb+csa / 4f;

            
                if (myColors.ContainsKey(csColorTotal)){

                        Debug.Log("skip");
                        myColors.Add(n, cs);



                    }
                    else{

                        myColors.Add(csColorTotal, cs);
                        Debug.Log("adding");

                }
                n++;

            
            


                
            
            

            

            //if (!colorArray.Contains(cs)){
            //    Debug.Log("Texture already has " + cs + " ");
                //cs = Color.black;
            //    colorArray.Add(Color.black);

           // } else{
                    
            //        colorArray.Add(cs);




            }
               

            var list = myColors.Keys.ToList();
            list.Sort();
            //Debug.Log(list);
            foreach(var key in myColors){
            Debug.Log(key.Key);
            Debug.Log(key.Value);
            colorsFinal.Add(key.Value);
            }

            //colorSort.Add(csColorTotal);

            //colorSort.Sort();
        
        //for (int i =0, i <= pic.count){



        
        Color[] ending = colorsFinal.ToArray();
        //colorArray[1].Sort();
        //Color[] cols = colorArray.ToArray();
        //Color[] cols2 = sourceTex.GetPixels(x, y, width, height);
        destTex = new Texture2D(width, height);
        destTex.SetPixels(ending);
        destTex.Apply();
            


    }

public void palletize3(){

        List<Color> colrs = new List<Color>();
        List<Color> colorArray = new List<Color>();
        List<float> colorSort = new List<float>();
        Dictionary <float, Color> myColors = new Dictionary <float, Color>();
        Dictionary <float, Color> myColors2 = new Dictionary <float, Color>();
        List<Color> colorsFinal = new List<Color>();

        

        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels(x, y, width, height);

        destTex2 = new Texture2D(width, height);
        
        int n = 0;
        int nn = 0;
        int n2 = 0;
        int nn2 = 0;

        string cnum = n.ToString();
        foreach (Color cs in pix){
            
            if(nn <= 36){
                if(n ==36){n=0;nn++;}

            float csr = cs.r;
            float csg = cs.g;
            float csb = cs.b;
            float csa = cs.a;
            csColorTotal = csr+csg+csb+csa / 4f;

                

                
                    if(!myColors.ContainsKey(csColorTotal)){

                        myColors.Add(csColorTotal, cs);
                                                 
                        destTex2.SetPixel(n, nn, cs);

                        Debug.Log("adding");
                        Debug.Log(n);
                        n++;
                        }

                        

                
                
                }

            }
 

            
            

            //foreach()

            //foreach (KeyValuePair<float,Color> kvp in myColors) {
            
            
            //var foos = myColors.Values.ToArray();
            //var foos2 = myColors2.Values.ToArray();
            //var connect = foos.Concat(foos2).ToArray();

            //var finish = connect.ToArray();

            //foos.Keys.Sort();
            //var list = myColors.Keys.ToList();
            //list.Sort();
            //Debug.Log(list);
            //foreach(var key in myColors){
            //Debug.Log(key.Key);
            //Debug.Log(key.Value);
            //colorsFinal.Add(key.Value);
            //}
        foreach (Color cs in pix){
            
            if(nn <= 36){
                if(n ==36){n=0;nn++;}

            float csr1 = cs.r;
            float csg1 = cs.g;
            float csb1 = cs.b;
            float csa1 = cs.a;
            float csColorTotal1 = csr1+csg1+csb1+csa1 / 4f;

                

                if (myColors.ContainsKey(csColorTotal1)){
                    
                    
                        Debug.Log("skip");
                        myColors.Add(nn2, Color.black);
                        nn2++;
                        Debug.Log(nn2);

                        
                            
                        destTex2.SetPixel(n, nn, Color.black);

                        Debug.Log("adding");
                        Debug.Log(n);
                        n++;
                        
                        
                        //destTex2.SetPixel(n, nn, cs);
                       


                    }
                    

                        

                
                
                }

        }
        //Color[] ending = colorsFinal.ToArray();
        //Debug.Log(colorsFinal[0]);
        //Debug.Log(colorsFinal[10]);
        //Debug.Log(colorsFinal[50]);
        
        //destTex2.SetPixels(connect);
        destTex2.Apply();
            


    }

    public void randomLoad(){

        sourceTex = (Texture2D)textures[Random.Range(0, textures.Length)];


    }

    // Update is called once per frame
    void Update () {

        if (changedValue && lockChanges == false){



        destPix = new Color[destTex.width * destTex.height ];
        int y = 0;
        while (y < destTex.height) {
            int x = 0;
            while (x < destTex.width) {
                xFrac = x * xFracX / (destTex.width - xFracXw);
                yFrac = y * yFracY / (destTex.height - yFracYh);
                //warpXFrac = Mathf.Sin(xFrac * warpFactor + Time.deltaTime); flux on play
                //warpYFrac = Mathf.Sin(yFrac * warpFactor + Time.deltaTime); flux on play
                warpXFrac = Mathf.Sin(xFrac * warpFactor);
                warpYFrac = Mathf.Sin(yFrac * warpFactor);

                destPix[y * destTex.width + x] = sourceTex.GetPixelBilinear(warpXFrac, warpYFrac);
                x++;
                xFracX = xFracX;
                yFracY = yFracY;
                
            }
            y++;
        }
        destTex.SetPixels(destPix);
        destTex.Apply();
        GetComponent<Renderer>().material.mainTexture = destTex;

        }
        if(rotateTexture){

            uvOffset += ( uvAnimationRate * Time.deltaTime );
            float offset = Time.time * 3;
            if( rotateTexture )
            {
                GetComponent<Renderer>().material.SetTextureOffset("_MainTex", uvOffset);

            }

            
            
        }

        if(parallaxTexture){

            uvOffset = ( uvAnimationRate * Time.deltaTime );
            if( parallaxTexture )
            {
                GetComponent<Renderer>().material.SetTextureOffset("_ParallaxMap", uvOffset);

            }

            
            
        }


        
    }

    void LateUpdate() 
    {

        
    }

    public void SetCurves(AnimationCurve xC, AnimationCurve yC, AnimationCurve zC)
    {


        Debug.Log(xC.Evaluate(1f));
        curveX = xC;
        curveY = yC;
        curveZ = zC;

        xFracX = xC.Evaluate(3f);
        Debug.Log(xFracX);


        if (changedValue && lockChanges == false){



        destPix = new Color[destTex.width * destTex.height ];
        int y = 0;
        while (y < destTex.height) {
            int x = 0;
            while (x < destTex.width) {
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

    public void pushGate(){

    if (changedValue && lockChanges == false){



        destPix = new Color[destTex.width * destTex.height ];
        int y = 0;
        while (y < destTex.height) {
            int x = 0;
            while (x < destTex.width) {
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

        }}

    public void pushGate_useCurves(){

        if(useCurves){useCurves=false;}else {useCurves=true;}

    }


    public void pushGate_random(){

            if (changedValue && lockChanges == false){

            float r_xFrac = Random.Range(0, +100);
            float r_yFrac = Random.Range(0, +100);
            float r_warpXFrac = Random.Range(0, +100);
            float r_warpYFrac = Random.Range(0, +100);
            float r_warpFactor = Random.Range(0, +100);
            float r_xFracXw = Random.Range(0, +100);
            float r_yFracYh = Random.Range(0, +100);
            float r_xFracX = Random.Range(0, +100);
            float r_yFracY = Random.Range(0, +100);


        destPix = new Color[destTex.width * destTex.height ];
        int r_y = 0;
        while (r_y < destTex.height) {
            int r_x = 0;
            while (r_x < destTex.width) {
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



}}
    
    public void pushGate_randomPixel(){

            if (changedValue && lockChanges == false || enableFlow == true){

            float pr_xFrac = Random.Range(0, +100);
            float pr_yFrac = Random.Range(0, +100);
            float pr_warpXFrac = Random.Range(0, +100);
            float pr_warpYFrac = Random.Range(0, +100);
            float pr_warpFactor = Random.Range(0, +100);
            float pr_xFracXw = Random.Range(0, +100);
            float pr_yFracYh = Random.Range(0, +100);
            float pr_xFracX = Random.Range(0, +100);
            float pr_yFracY = Random.Range(0, +100);


        destPix = new Color[destTex.width * destTex.height ];
        int r_y = 0;
        while (r_y < destTex.height) {
            int r_x = 0;
            while (r_x < destTex.width) {
                pr_xFrac = Random.Range(0, destTex.width);
                pr_yFrac = Random.Range(0, destTex.height);
                pr_warpXFrac = Mathf.Sin(pr_xFrac * pr_warpFactor * Time.deltaTime);
                pr_warpYFrac = Mathf.Sin(pr_yFrac * pr_warpFactor * Time.deltaTime);
                destPix[r_y * destTex.width + r_x] = sourceTex.GetPixelBilinear(pr_warpXFrac, pr_warpYFrac);
                r_x++;
                
            }
            r_y++;
        }
        destTex.SetPixels(destPix);
        destTex.Apply();
        GetComponent<Renderer>().material.mainTexture = destTex;




    }}

        public void pushGate_randomSmall(){

            if (changedValue && lockChanges == false){

            float r_xFrac = Random.Range(0, +25);
            float r_yFrac = Random.Range(0, +25);

            float r_warpFactor = Random.Range(0, +25);
            float r_xFracXw = Random.Range(0, +25);
            float r_yFracYh = Random.Range(0, +25);
            float r_xFracX = Random.Range(0, +25);
            float r_yFracY = Random.Range(0, +25);


        destPix = new Color[destTex.width * destTex.height ];
        int r_y = 0;
        while (r_y < destTex.height) {
            int r_x = 0;
            while (r_x < destTex.width) {
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



    public void pushGate_save(string saveName){

        
        Texture2D destTexCopy = new Texture2D(destTex.width, destTex.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(destTex, 0, 0, destTexCopy, 0,0);

        destTexCopy.Apply();
        byte[] bytes = destTexCopy.EncodeToPNG();
        File.WriteAllBytes("Assets/!materialGate/" + saveName + ".png", bytes);
        DestroyImmediate(destTexCopy);
        AssetDatabase.Refresh();



        }

        public void pushGate_savePallette(string saveName){

        
        Texture2D destTexCopy2 = new Texture2D(destTex2.width, destTex2.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(destTex2, 0, 0, destTexCopy2, 0,0);

        destTexCopy2.Apply();
        byte[] bytes = destTexCopy2.EncodeToPNG();
        File.WriteAllBytes("Assets/!materialGate/" + saveName + ".png", bytes);
        DestroyImmediate(destTexCopy2);
        AssetDatabase.Refresh();



        }

}
