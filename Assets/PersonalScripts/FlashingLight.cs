using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{

    public Material greenColor;
    //private Material startMaterial;
    //private float FadeDuration = 1f;

    private MeshRenderer meshRenderer;
    private Material material;
    public int timeToTurnGreen = 1700;
    private int timer;

	// Use this for initialization
	void Start ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        //startMaterial = material;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (timer < timeToTurnGreen)
        {
            //float lerp = Time.time / FadeDuration;
            //material.SetColor("_Color", material.color * MapValue(0f,1f,0.2f,0.3f, Mathf.Cos(lerp)));

            float emission = Mathf.PingPong(Time.time, 1.0f);
            Color baseColor = Color.red;
            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
            material.SetColor("_EmissionColor", finalColor);

            timer++;
            Debug.Log(timer);
        }
        else
        {
            
            meshRenderer.material = greenColor;
            //material.color = greenColor.color;
        }
	}

    float MapValue(float fromRangeMin, float fromRangeMax, float toRangeMin, float toRangeMax, float convert)
    {
        return toRangeMin + (toRangeMax - toRangeMin) * ((convert - fromRangeMin) / (fromRangeMax - fromRangeMin));
    }
}
