using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float minDensity;
    public float maxDensity;
    public float speed;
    
	// Use this for initialization
	void Start ()
	{
	    RenderSettings.fogDensity = minDensity;
	}
	
	// Update is called once per frame
	void Update () {
	    if (RenderSettings.fogDensity < maxDensity)
	    {
	        RenderSettings.fogDensity += speed * Time.deltaTime;
	    }
	}
}
