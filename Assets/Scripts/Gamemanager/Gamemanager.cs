using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RangeGeneraterTankPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector3[] randomPosition ={new Vector3(-40,0,40),new Vector3(40,0,40),new Vector3(-40,0,-40),new Vector3(24,0,-30),new Vector3(-40,40) };

    void RangeGeneraterTankPosition()
    {
        GameObject tank = Instantiate(Resources.Load("AITank")) as GameObject;
        tank.transform.position = randomPosition[Random.Range(0,3)];
    }
}
