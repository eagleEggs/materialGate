using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class matGateController : MonoBehaviour {

	[ExecuteInEditMode]

	[SerializeField] 							private Texture2D 	sourceTex;
    [SerializeField] 	[Range(-1000, +1000)]	private float 		warpFactor;
    [SerializeField] 	[Range(-1000, +1000)]	private float 		xFracX;
    [SerializeField] 	[Range(-1000, +1000)]	private float 		yFracY;
    [SerializeField] 	[Range(-1000, +1000)]	private float 		xFracXw;
    [SerializeField] 	[Range(-1000, +1000)]	private float 		yFracYh;
    											private float 		warpXFrac;
    											private float 		warpYFrac;
    											private float 		xFrac;
    											private float 		yFrac;


    [SerializeField] 							private Texture2D 	destTex;
    											private Color[] 	destPix;
    											private float 		checkChange;
    [SerializeField] 							private bool 		changedValue=true;
    [SerializeField] 							private bool 		lockChanges=false;

    											private AnimationCurve curveX;
    											private AnimationCurve curveY;
    											private AnimationCurve curveZ;
    										    public bool useCurves = false;
    										    public bool enableFlow = false;

    void Awake(){

    	
    }

    void Start() {

    	
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
                warpXFrac = Mathf.Sin(xFrac * warpFactor + Time.deltaTime);
                warpYFrac = Mathf.Sin(yFrac * warpFactor + Time.deltaTime);
                //float warpYFrac = Mathf.Cos (yFrac * warpFactor);
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
    	if(enableFlow){
    		
    	}


		
	}

	public void SetCurves(AnimationCurve xC, AnimationCurve yC, AnimationCurve zC)
    {


    	Debug.Log(xC.Evaluate(1f));
        curveX = xC;
        curveY = yC;
        curveZ = zC;

        xFracX = xC.Evaluate(3f);
        Debug.Log(xFracX);
        //yFracY = yC;
        //warpFactor = zC;

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
                //float warpYFrac = Mathf.Cos (yFrac * warpFactor);
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
                //float warpYFrac = Mathf.Cos (yFrac * warpFactor);
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

    		float r_xFrac = Random.Range(-1000, +1000);
    		float r_yFrac = Random.Range(-1000, +1000);
    		float r_warpXFrac = Random.Range(-1000, +1000);
    		float r_warpYFrac = Random.Range(-1000, +1000);
    		float r_warpFactor = Random.Range(-1000, +1000);
    		float r_xFracXw = Random.Range(-1000, +1000);
    		float r_yFracYh = Random.Range(-1000, +1000);
    		float r_xFracX = Random.Range(-1000, +1000);
    		float r_yFracY = Random.Range(-1000, +1000);


		destPix = new Color[destTex.width * destTex.height ];
        int r_y = 0;
        while (r_y < destTex.height) {
            int r_x = 0;
            while (r_x < destTex.width) {
                r_xFrac = r_x * r_xFracX / (destTex.width - r_xFracXw);
                r_yFrac = r_y * r_yFracY / (destTex.height - r_yFracYh);
                r_warpXFrac = Mathf.Sin(r_xFrac * r_warpFactor + Time.deltaTime);
                r_warpYFrac = Mathf.Sin(r_yFrac * r_warpFactor + Time.deltaTime);
                //float warpYFrac = Mathf.Cos (yFrac * warpFactor);
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

    		float pr_xFrac = Random.Range(-1000, +1000);
    		float pr_yFrac = Random.Range(-1000, +1000);
    		float pr_warpXFrac = Random.Range(-1000, +1000);
    		float pr_warpYFrac = Random.Range(-1000, +1000);
    		float pr_warpFactor = Random.Range(-1000, +1000);
    		float pr_xFracXw = Random.Range(-1000, +1000);
    		float pr_yFracYh = Random.Range(-1000, +1000);
    		float pr_xFracX = Random.Range(-1000, +1000);
    		float pr_yFracY = Random.Range(-1000, +1000);


		destPix = new Color[destTex.width * destTex.height ];
        int r_y = 0;
        while (r_y < destTex.height) {
            int r_x = 0;
            while (r_x < destTex.width) {
                pr_xFrac = Random.Range(0, destTex.width);
                pr_yFrac = Random.Range(0, destTex.height);
                pr_warpXFrac = Mathf.Sin(pr_xFrac * pr_warpFactor * Time.deltaTime);
                pr_warpYFrac = Mathf.Sin(pr_yFrac * pr_warpFactor * Time.deltaTime);
                //float warpYFrac = Mathf.Cos (yFrac * warpFactor);
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

    		float r_xFrac = Random.Range(0, +50);
    		float r_yFrac = Random.Range(0, +50);
    		//float r_warpXFrac = Random.Range(0, +50);
    		//float r_warpYFrac = Random.Range(0, +50);
    		float r_warpFactor = Random.Range(0, +50);
    		float r_xFracXw = Random.Range(0, +50);
    		float r_yFracYh = Random.Range(0, +50);
    		float r_xFracX = Random.Range(0, +50);
    		float r_yFracY = Random.Range(0, +50);


		destPix = new Color[destTex.width * destTex.height ];
        int r_y = 0;
        while (r_y < destTex.height) {
            int r_x = 0;
            while (r_x < destTex.width) {
                r_xFrac = r_x * r_xFracX / (destTex.width - r_xFracXw);
                r_yFrac = r_y * r_yFracY / (destTex.height - r_yFracYh);
                float r_warpXFrac = Mathf.Sin(r_xFrac * r_warpFactor + Time.deltaTime);
                float r_warpYFrac = Mathf.Sin(r_yFrac * r_warpFactor + Time.deltaTime);
                //float warpYFrac = Mathf.Cos (yFrac * warpFactor);
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
		//destTexCopy = new Color[destTex.width * destTex.height ];
		//destTexCopy.SetPixels(destTex);
        destTexCopy.Apply();
        byte[] bytes = destTexCopy.EncodeToPNG();
        File.WriteAllBytes("Assets/!materialGate/" + saveName + ".png", bytes);
        DestroyImmediate(destTexCopy);
        AssetDatabase.Refresh();
		//AssetDatabase.CreateAsset(destTexCopy, "Assets/!materialGate/" + saveName + ".png");


		}

}
