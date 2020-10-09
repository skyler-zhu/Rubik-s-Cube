using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCam : MonoBehaviour
{
    private Camera _cam;
    private String _camName = "TestCam";
    private bool _MoveRight;
    private bool _MoveLeft;
    private bool _MoveUp;
    private bool _MoveDown;
    private bool _isMoving;

    private void Awake()
    {
        _cam = GameObject.Find(_camName).GetComponent<Camera>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Move Right"))
        {
            _MoveRight = !_MoveRight;
            _isMoving = !_isMoving;
        }
        if (GUILayout.Button("Move Left"))
        {
            _MoveLeft = !_MoveLeft;
            _isMoving = !_isMoving;
        }
        if (GUILayout.Button("Move Up"))
        {
            _MoveUp = !_MoveUp;
            _isMoving = !_isMoving;
        }
        if (GUILayout.Button("Move Down"))
        {
            _MoveDown = !_MoveDown;
            _isMoving = !_isMoving;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_MoveRight)
        {
            this.transform.Rotate(Vector3.up, Space.Self);
        }
        if (_MoveLeft)
        {
            this.transform.Rotate(Vector3.down, Space.Self);
        }
        if (_MoveUp)
        {
            this.transform.Rotate(Vector3.right, Space.Self);
        }
        if (_MoveDown)
        {
            this.transform.Rotate(Vector3.left, Space.Self);
        }
    }
}
