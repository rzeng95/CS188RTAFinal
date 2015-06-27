using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour {
 
	//This script enables underwater effects. Attach to main camera.
 
    //Define variable
    private float underwaterLevel;
	public GameObject FPC;
 
    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;
    //private bool once = true;
    void Start () {
		underwaterLevel = 117.5f;
		defaultFog = RenderSettings.fog;
		defaultFogColor = RenderSettings.fogColor;
		defaultFogDensity = RenderSettings.fogDensity;
		defaultSkybox = RenderSettings.skybox;
		
		noSkybox = null;
	    //Set the background color
	    GetComponent<Camera>().backgroundColor = new Color(0.2f, 0.3f, 0.3f, 0.20f);

    }
 
    void Update () {
		
        if (FPC.transform.position.y < underwaterLevel /*&& once == true*/)
        {
			//once = false;
            RenderSettings.fog = true;
            //RenderSettings.fogColor = new Color(0.2f, 0.3f, 0.45f, 0.9f);
			RenderSettings.fogColor = new Color32(36, 53, 68, 100);
            RenderSettings.fogDensity = 0.2f;//0.08f;
            RenderSettings.skybox = noSkybox;
			RenderSettings.fogStartDistance = 1;
        }
		
        else
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
        } 
    }
}