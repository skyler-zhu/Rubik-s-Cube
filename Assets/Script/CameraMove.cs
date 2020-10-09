using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool _inSubScreen;
    
    private String _currentMoveCamName = "TestCam";
    private Camera _currentMoveCam;
    private Vector3 _prePosi;
    private Touch _touch;
    [SerializeField] private int moveSpeed = 180;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentMoveCam = GameObject.Find(_currentMoveCamName).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x <= 400 && Input.mousePosition.y <= 445)
            {
                _prePosi = _currentMoveCam.ScreenToViewportPoint(Input.mousePosition);
                _inSubScreen = true;
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (_inSubScreen)
            {
                Vector3 direction = _prePosi - _currentMoveCam.ScreenToViewportPoint(Input.mousePosition);
                _currentMoveCam.transform.position = new Vector3(2,2,2);
                _currentMoveCam.transform.Rotate(new Vector3(1,0,0), direction.y * moveSpeed);
                _currentMoveCam.transform.Rotate(new Vector3(0,1,0), -direction.x * moveSpeed);
                _currentMoveCam.transform.Translate(new Vector3(0,0,-5));
                    
                _prePosi = _currentMoveCam.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.mousePosition.x >= 400 || Input.mousePosition.y >= 445)
            {
                _inSubScreen = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _inSubScreen = false;
        }
    }
}
