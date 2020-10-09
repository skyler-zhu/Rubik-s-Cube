using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC : MonoBehaviour
{
    //
    private GameObject[] planes;
    public Material front;
    public Material back;
    public Material top;
    public Material bot;
    public Material left;
    public Material right;
    

    private void Awake()
    {
        if (planes == null)
        {
            planes = GameObject.FindGameObjectsWithTag("plane");
        }
    }

    internal void ResetColor()
    {
        foreach (GameObject plane in planes)
        {
            Renderer rend = plane.GetComponent<Renderer>();
            Vector3 posi = plane.transform.position;
            if (Math.Abs(posi.z - 0.4) < 0.05)
            {
                rend.material = front;
            }
            else if (Math.Abs(posi.y - 3.6) < 0.05)
            {
                rend.material = top;
            }
            else if (Math.Abs(posi.x - 0.4) < 0.05)
            {
                rend.material = left;
            }
            else if (Math.Abs(posi.x - 3.6) < 0.05)
            {
                rend.material = right;
            }
            else if (Math.Abs(posi.z - 3.6) < 0.05)
            {
                rend.material = back;
            }
            else if (Math.Abs(posi.y - 0.4) < 0.05)
            {
                rend.material = bot;
            }
        }
    }
}
