using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class MainCamera : MonoBehaviour
{
    private Touch initTouch = new Touch();
    public GameObject obj;
    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;
    public float speed = 0.5f;
    public float dirX = -1;
    public float dirY = -1;

    private void Start()
    {
        origRot = obj.transform.eulerAngles;
    }

    private void Update()
    {
        if (obj.transform.rotation.eulerAngles.x < 270 && obj.transform.rotation.eulerAngles.x > 90)
        {
            dirY = 1.0f;
        }
        else
        {
            dirY = -1f;
        }
        
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < 400 && touch.position.y < 500)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initTouch = touch;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = initTouch.position.x - touch.position.x;
                    float deltaY = initTouch.position.y - touch.position.y;
                    rotX -= deltaY * Time.deltaTime * speed * dirX;
                    rotY += deltaX * Time.deltaTime * speed * dirY;
                    obj.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    initTouch = new Touch();
                }
            }
        }
    }
}
