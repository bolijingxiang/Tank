using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkin : MonoBehaviour {

    public Color color=Color.white;
    // List<Material> list = new List<Material>();
    MeshRenderer[] list;
	// Use this for initialization
	void Start () {
        color = Color.white;

        list = GetComponentsInChildren<MeshRenderer>();

        //Debug.Log(list.Length);

       // ChangeMaterialColor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMaterialColor()
    {
        for(int i=0;i<list.Length;i++)
        {
            if(list[i].material.name.IndexOf("TankColour")>-1)
            {
                list[i].material.color = new Color((float)Random.Range(0, 255)/255, (float)Random.Range(0, 255)/255, (float)Random.Range(0, 255)/255,255);
            }
        }
    }
}
