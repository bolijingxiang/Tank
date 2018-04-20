using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public static List<Tank> tankList = new List<Tank>();

    private void Start()
    {
        tankList.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        tankList.Remove(this);
    }

}
