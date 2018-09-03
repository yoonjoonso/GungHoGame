using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float lifetime;
    float totalLife;
    Material mat;

	void Start ()	
	{
        mat = GetComponent<MeshRenderer>().material;
        totalLife = lifetime;
	}

	void Update ()	
	{
        lifetime -= Time.deltaTime;

		if(lifetime < 0)
        {
            GameManagerScript.instance.KillPlayer();
        }
        else
        {
            float hue = lifetime / totalLife;
            mat.SetColor("_TintColor", Color.HSVToRGB(hue,1,1));
        }
	}
}