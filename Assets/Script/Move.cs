using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Script
{
    public class Move : MonoBehaviour
    {
        private String _topCore = "TopCore";
        private String _botCore = "BotCore";
        private String _leftCore = "LeftCore";
        private String _rightCore = "RightCore";
        private String _frontCore = "FrontCore";
        private String _backCore = "BackCore";
        private String _midCore = "MidCore";
        private String _midCoreX = "MidCoreX";
        private String _midCoreZ = "MidCoreZ";
        private String _currentCamName = "TempCam";

        //是否应该转动
        private bool _allowMove;
        private bool _topRight;
        private bool _topLeft;
        private bool _leftRight;
        private bool _leftLeft;
        private bool _rightLeft;
        private bool _rightRight;
        private bool _botLeft;
        private bool _botRight;
        private bool _frontLeft;
        private bool _frontRight;
        private bool _backRight;
        private bool _backLeft;
        private bool _midTopLeft;
        private bool _midTopRight;
        private bool _midRightLeft;
        private bool _midRightRight;
        private bool _midFrontLeft;
        private bool _midFrontRight;

        //开始前观察魔方
        private bool _moveLock = false;

        public Logic _logic;
        private String _logicObj = "GameLogic";

        private MC _mc;
        private String _MCObj = "Material Control";

        //是否开始检查停止
        private bool _check;

        //转动核心
        private Transform _tempP;
        
        //临时转动核心孩子和孩子的index
        private Transform[] _tempList;
        private int _index;
        
        //起始核心角度
        public decimal degree;
        
        public int speed = 250;

        //修复目标角度和矢量
        private Vector3 _fixVector;
        private Quaternion _fixRotation;
        
        //魔方是否正在移动
        private bool _movingCube;
        
        private RaycastHit _hit;
        private Transform _touchedSurface;
        
        private Vector3 _hitPosi;
        private RaycastHit _secHit;

        private decimal angleFixRange = 3;
        
        private Vector3 _mouse;
        Transform _plane;
        private Camera _currentCam;

        private Transform _currentMoveCore;
        private Camera _currentMoveCam;
        
        private bool _inMainScreen;
        private Touch _touchTemp;
        private Touch _touchTempTwo;
        private Touch _touchSub;
        private Touch _touchMain;
        private Touch _touchC;
        private Touch _touch;
        
        private bool _inSubScreen;
    
        private String _currentMoveCamName = "TestCam";
        private Vector3 _prePosi;
        [SerializeField] private int moveSpeed = 180;

        private enum TouchDirt {X, NegX, Y, NegY, Z, NegZ, None}
        
        private void Awake()
        {
            _currentMoveCam = GameObject.Find(_currentMoveCamName).GetComponent<Camera>();
            _currentCam = GameObject.Find(_currentCamName).GetComponent<Camera>();
            _tempList = new Transform[9];
            _allowMove = true;
            _fixVector = new Vector3();
            _movingCube = false;
            _logic = GameObject.Find(_logicObj).GetComponent<Logic>();
            _mc = GameObject.Find(_MCObj).GetComponent<MC>();
        }
        
        //把方块加到对应核心, 可优化
        private void Top()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _topCore)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.y);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.y <= 1.15 && child.transform.localPosition.y >= 1.05)
                {
                    if (child.name != _topCore)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void Left()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _leftCore)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.x);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.x <= -1.05 && child.transform.localPosition.x >= -1.15)
                {
                    if (child.name != _leftCore)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void Right()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _rightCore)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.x);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.x <= 1.15 && child.transform.localPosition.x >= 1.05)
                {
                    if (child.name != _rightCore)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void Bottom()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _botCore)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.y);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.y <= -1.05 && child.transform.localPosition.y >= -1.15)
                {
                    if (child.name != _botCore)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void Front()
        {
            foreach (Transform variable in this.transform)
            {
                if (variable.name == _frontCore)
                {
                    _tempP = variable;
                    degree = Math.Abs((decimal)variable.localRotation.eulerAngles.z);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.z <= -1.05 && child.transform.localPosition.z >= -1.15)
                {
                    if (child.name != _frontCore)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void Back()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _backCore)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.z);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.z <= 1.15 && child.transform.localPosition.z >= 1.05)
                {
                    if (child.name != _backCore)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void MidTop()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _midCore)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.y);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.y <= 0.1 && child.transform.localPosition.y >= -0.1)
                {
                    if (child.name != _midCore && child.name != _frontCore && child.name != _backCore &&
                        child.name != _leftCore && child.name != _rightCore && child.name != _midCoreX &&
                        child.name != _midCoreZ)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void MidRight()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _midCoreX)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.x);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.x <= 0.1 && child.transform.localPosition.x >= -0.1)
                {
                    if (child.name != _midCore && child.name != _frontCore && child.name != _backCore &&
                        child.name != _botCore && child.name != _topCore && child.name != _midCoreX &&
                        child.name != _midCoreZ)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }
        private void MidFront()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == _midCoreZ)
                {
                    _tempP = child;
                    degree = Math.Abs((decimal)child.localRotation.eulerAngles.z);
                    _check = true;
                }
            }
            foreach (Transform child in this.transform)
            {
                if (child.transform.localPosition.z <= 0.1 && child.transform.localPosition.z >= -0.1)
                {
                    if (child.name != _midCore && child.name != _botCore && child.name != _leftCore &&
                        child.name != _rightCore && child.name != _topCore && child.name != _midCoreZ &&
                        child.name != _midCoreX)
                    {
                        _tempList[_index] = child;
                        _index += 1;
                    }
                }
            }
            foreach (Transform child in _tempList)
            {
                child.SetParent(_tempP);
            }

            _index = 0;
        }

        //检查是否应该停止转动,可优化
        private void CheckTopStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) < (90 + angleFixRange))
                {
                    _topRight = false;
                    _topLeft = false;
                    FixAngle(90, "y", (int)angleFixRange);
                    FixAngle(0, "y",(int)angleFixRange);
                    FixAngle(180, "y",(int)angleFixRange);
                    FixAngle(270, "y",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) < (270 + angleFixRange))
                {
                    _topRight = false;
                    _topLeft = false;
                    FixAngle(90, "y",(int)angleFixRange);
                    FixAngle(0, "y",(int)angleFixRange);
                    FixAngle(180, "y",(int)angleFixRange);
                    FixAngle(270, "y",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckLeftStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) < (90 + angleFixRange))
                {
                    _leftRight = false;
                    _leftLeft = false;
                    FixAngle(90, "x",(int)angleFixRange);
                    FixAngle(0, "x",(int)angleFixRange);
                    FixAngle(180, "x",(int)angleFixRange);
                    FixAngle(270, "x",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) < (270 + angleFixRange))
                {
                    _leftRight = false;
                    _leftLeft = false;
                    FixAngle(90, "x",(int)angleFixRange);
                    FixAngle(0, "x",(int)angleFixRange);
                    FixAngle(180, "x",(int)angleFixRange);
                    FixAngle(270, "x",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckRightStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) < (90 + angleFixRange))
                {
                    _rightRight= false;
                    _rightLeft = false;
                    FixAngle(90, "x",(int)angleFixRange);
                    FixAngle(0, "x",(int)angleFixRange);
                    FixAngle(180, "x",(int)angleFixRange);
                    FixAngle(270, "x",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) < (270 + angleFixRange))
                {
                    _rightRight= false;
                    _rightLeft = false;
                    FixAngle(90, "x",(int)angleFixRange);
                    FixAngle(0, "x",(int)angleFixRange);
                    FixAngle(180, "x",(int)angleFixRange);
                    FixAngle(270, "x",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckBotStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) < (90 + angleFixRange))
                {
                    _botRight = false;
                    _botLeft = false;
                    FixAngle(90, "y", (int)angleFixRange);
                    FixAngle(0, "y",(int)angleFixRange);
                    FixAngle(180, "y",(int)angleFixRange);
                    FixAngle(270, "y",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) < (270 + angleFixRange))
                {
                    _botRight = false;
                    _botLeft = false;
                    FixAngle(90, "y",(int)angleFixRange);
                    FixAngle(0, "y",(int)angleFixRange);
                    FixAngle(180, "y",(int)angleFixRange);
                    FixAngle(270, "y",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckFrontStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) < (90 + angleFixRange))
                {
                    _frontRight = false;
                    _frontLeft = false;
                    FixAngle(90, "z", (int)angleFixRange);
                    FixAngle(0, "z",(int)angleFixRange);
                    FixAngle(180, "z",(int)angleFixRange);
                    FixAngle(270, "z",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) < (270 + angleFixRange))
                {
                    _frontRight = false;
                    _frontLeft = false;
                    FixAngle(90, "z",(int)angleFixRange);
                    FixAngle(0, "z",(int)angleFixRange);
                    FixAngle(180, "z",(int)angleFixRange);
                    FixAngle(270, "z",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckBackStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) < (90 + angleFixRange))
                {
                    _backRight = false;
                    _backLeft = false;
                    FixAngle(90, "z", (int)angleFixRange);
                    FixAngle(0, "z",(int)angleFixRange);
                    FixAngle(180, "z",(int)angleFixRange);
                    FixAngle(270, "z",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) < (270 + angleFixRange))
                {
                    _backRight = false;
                    _backLeft = false;
                    FixAngle(90, "z",(int)angleFixRange);
                    FixAngle(0, "z",(int)angleFixRange);
                    FixAngle(180, "z",(int)angleFixRange);
                    FixAngle(270, "z",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckMidTopStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) < (90 + angleFixRange))
                {
                    _midTopLeft = false;
                    _midTopRight = false;
                    FixAngle(90, "y", (int)angleFixRange);
                    FixAngle(0, "y",(int)angleFixRange);
                    FixAngle(180, "y",(int)angleFixRange);
                    FixAngle(270, "y",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.y) - degree) < (270 + angleFixRange))
                {
                    _midTopLeft = false;
                    _midTopRight = false;
                    FixAngle(90, "y",(int)angleFixRange);
                    FixAngle(0, "y",(int)angleFixRange);
                    FixAngle(180, "y",(int)angleFixRange);
                    FixAngle(270, "y",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckMidRightStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) < (90 + angleFixRange))
                {
                    _midRightLeft= false;
                    _midRightRight = false;
                    FixAngle(90, "x",(int)angleFixRange);
                    FixAngle(0, "x",(int)angleFixRange);
                    FixAngle(180, "x",(int)angleFixRange);
                    FixAngle(270, "x",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.x) - degree) < (270 + angleFixRange))
                {
                    _midRightLeft= false;
                    _midRightRight = false;
                    FixAngle(90, "x",(int)angleFixRange);
                    FixAngle(0, "x",(int)angleFixRange);
                    FixAngle(180, "x",(int)angleFixRange);
                    FixAngle(270, "x",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }
        private void CheckMidFrontStop()
        {
            if (_check)
            {
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) > (90 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) < (90 + angleFixRange))
                {
                    _midFrontLeft = false;
                    _midFrontRight = false;
                    FixAngle(90, "z", (int)angleFixRange);
                    FixAngle(0, "z",(int)angleFixRange);
                    FixAngle(180, "z",(int)angleFixRange);
                    FixAngle(270, "z",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
                if (Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) > (270 - angleFixRange) && 
                    Math.Abs(Math.Abs((decimal)_tempP.localRotation.eulerAngles.z) - degree) < (270 + angleFixRange))
                {
                    _midFrontLeft = false;
                    _midFrontRight = false;
                    FixAngle(90, "z",(int)angleFixRange);
                    FixAngle(0, "z",(int)angleFixRange);
                    FixAngle(180, "z",(int)angleFixRange);
                    FixAngle(270, "z",(int)angleFixRange);
                    ResetRelation();
                    _movingCube = false;
                    _check = false;
                }
            }
        }

        //每次移动结束修复魔方角度误差
        private void FixAngle(int d, string axi, int range)
        {
            _fixRotation = _tempP.localRotation;
            if (axi == "x")
            {
                if (d == 0)
                {
                    if ((_fixRotation.eulerAngles.x < range && _fixRotation.eulerAngles.x > 0) || 
                        (_fixRotation.eulerAngles.x > (360 - range) && _fixRotation.eulerAngles.x < 360))
                    {
                        _fixVector.x = d;
                        _fixVector.y = _fixRotation.eulerAngles.y;
                        _fixVector.z = _fixRotation.eulerAngles.z;
                        _tempP.localRotation = Quaternion.Euler(_fixVector);
                    }
                }else if (_fixRotation.eulerAngles.x < (d + range) && _fixRotation.eulerAngles.x > (d - range))
                {
                    _fixVector.x = d;
                    _fixVector.y = _fixRotation.eulerAngles.y;
                    _fixVector.z = _fixRotation.eulerAngles.z;
                    _tempP.localRotation = Quaternion.Euler(_fixVector);
                }
            }else if (axi == "y")
            {
                if (d == 0)
                {
                    if ((_fixRotation.eulerAngles.y < range && _fixRotation.eulerAngles.y > 0) || 
                        (_fixRotation.eulerAngles.y > (360 - range) && _fixRotation.eulerAngles.y < 360))
                    {
                        _fixVector.y = d;
                        _fixVector.x = _fixRotation.eulerAngles.x;
                        _fixVector.z = _fixRotation.eulerAngles.z;
                        _tempP.localRotation = Quaternion.Euler(_fixVector);
                    }
                }else if (_fixRotation.eulerAngles.y < (d + range) && _fixRotation.eulerAngles.y > (d - range))
                {
                    _fixVector.y = d;
                    _fixVector.x = _fixRotation.eulerAngles.x;
                    _fixVector.z = _fixRotation.eulerAngles.z;
                    _tempP.localRotation = Quaternion.Euler(_fixVector);
                }
            }
            else
            {
                if (d == 0)
                {
                    if ((_fixRotation.eulerAngles.z < range && _fixRotation.eulerAngles.z > 0) || 
                        (_fixRotation.eulerAngles.z > (360 - range) && _fixRotation.eulerAngles.z < 360))
                    {
                        _fixVector.z = d;
                        _fixVector.x = _fixRotation.eulerAngles.x;
                        _fixVector.y = _fixRotation.eulerAngles.y;
                        _tempP.localRotation = Quaternion.Euler(_fixVector);
                    }
                }else if (_fixRotation.eulerAngles.z < (d + range) && _fixRotation.eulerAngles.z > (d - range))
                {
                    _fixVector.z = d;
                    _fixVector.x = _fixRotation.eulerAngles.x;
                    _fixVector.y = _fixRotation.eulerAngles.y;
                    _tempP.localRotation = Quaternion.Euler(_fixVector);
                }
            }
        }

        //解除转动核心和方块的关系
        private void ResetRelation()
        {
            foreach (Transform child in _tempList)
            {
                child.SetParent(this.transform);
            }
        }

        //通过点击的面,点击方向和视角决定转法
        private float _planeX;
        private float _planeY;
        private float _planeZ;
        private void DecideMoveFront(Transform touchePlane, TouchDirt dirt)
        {
            if (dirt == TouchDirt.None)
            {
                return;
            }

            var position = touchePlane.position;
            _planeX = position.x;
            _planeY = position.y;
            _planeZ = position.z;
            
            //x = 0, y = 0, yellow
            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY- 0.9) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveBotRight();
                        break;
                    case TouchDirt.Y:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveLeftRight();
                        break;
                    default:
                        MoveBotLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.Y:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveLeftRight();
                        break;
                    default:
                        MoveMidTopLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveTopRight();
                        break;
                    case TouchDirt.Y:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveLeftRight();
                        break;
                    default:
                        MoveTopLeft();
                        break;
                }
            }

            //x = 1, y = 0, yellow
            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveBotRight();
                        break;
                    case TouchDirt.Y:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveMidRightRight();
                        break;
                    default:
                        MoveBotLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.Y:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveMidRightRight();
                        break;
                    default:
                        MoveMidTopLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveTopRight();
                        break;
                    case TouchDirt.Y:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveMidRightRight();
                        break;
                    default:
                        MoveTopLeft();
                        break;
                }
            }

            //x = 2, y = 0, yellow
            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveBotRight();
                        break;
                    case TouchDirt.Y:
                        MoveRightLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveRightRight();
                        break;
                    default:
                        MoveBotLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.Y:
                        MoveRightLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveRightRight();
                        break;
                    default:
                        MoveMidTopLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 0.4) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegX:
                        MoveTopRight();
                        break;
                    case TouchDirt.Y:
                        MoveRightLeft();
                        break;
                    case TouchDirt.NegY:
                        MoveRightRight();
                        break;
                    default:
                        MoveTopLeft();
                        break;
                }
            }
        }

        private void DecideMoveLeft(Transform touchePlane, TouchDirt dirt)
        {
            if (dirt == TouchDirt.None)
            {
                return;
            }
            var position = touchePlane.position;
            _planeX = position.x;
            _planeY = position.y;
            _planeZ = position.z;
            
            //x = 0, y = 2, green
            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveBotRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveBackRight();
                        break;
                    default:
                        MoveBackLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveBackRight();
                        break;
                    default:
                        MoveBackLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveTopRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveBackRight();
                        break;
                    default:
                        MoveBackLeft();
                        break;
                }
            }

            //x = 1
            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveBotRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidFrontRight();
                        break;
                    default:
                        MoveMidFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidFrontRight();
                        break;
                    default:
                        MoveMidFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveTopRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidFrontRight();
                        break;
                    default:
                        MoveMidFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveBotRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveFrontRight();
                        break;
                    default:
                        MoveFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveFrontRight();
                        break;
                    default:
                        MoveFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.4) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.Z:
                        MoveTopRight();
                        break;
                    case TouchDirt.NegZ:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveFrontRight();
                        break;
                    default:
                        MoveFrontLeft();
                        break;
                }
            }
        }
        
        private void DecideMoveRight(Transform touchePlane, TouchDirt dirt)
        {
            if (dirt == TouchDirt.None)
            {
                return;
            }
            var position = touchePlane.position;
            _planeX = position.x;
            _planeY = position.y;
            _planeZ = position.z;
            
            //x = 0, y = 2, red
            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveBotRight();
                        break;
                    case TouchDirt.Z:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveFrontLeft();
                        break;
                    default:
                        MoveFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.Z:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveFrontLeft();
                        break;
                    default:
                        MoveFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveTopRight();
                        break;
                    case TouchDirt.Z:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveFrontLeft();
                        break;
                    default:
                        MoveFrontRight();
                        break;
                }
            }

            //x = 1
            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveBotRight();
                        break;
                    case TouchDirt.Z:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidFrontLeft();
                        break;
                    default:
                        MoveMidFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.Z:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidFrontLeft();
                        break;
                    default:
                        MoveMidFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveTopRight();
                        break;
                    case TouchDirt.Z:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidFrontLeft();
                        break;
                    default:
                        MoveMidFrontRight();
                        break;
                }
            }

            //x = 2
            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveBotRight();
                        break;
                    case TouchDirt.Z:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveBackLeft();
                        break;
                    default:
                        MoveBackRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.Z:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveBackLeft();
                        break;
                    default:
                        MoveBackRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.6) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveTopRight();
                        break;
                    case TouchDirt.Z:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveBackLeft();
                        break;
                    default:
                        MoveBackRight();
                        break;
                }
            }
        }

        private void DecideMoveTop(Transform touchPlane, TouchDirt dirt)
        {
            if (dirt == TouchDirt.None)
            {
                return;
            }

            var position = touchPlane.position;
            _planeX = position.x;
            _planeY = position.y;
            _planeZ = position.z;
            
            //x = 0, y = 2, red
            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveLeftRight();
                        break;
                    case TouchDirt.Z:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.X:
                        MoveFrontRight();
                        break;
                    default:
                        MoveFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveLeftRight();
                        break;
                    case TouchDirt.Z:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.X:
                        MoveMidFrontRight();
                        break;
                    default:
                        MoveMidFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveLeftRight();
                        break;
                    case TouchDirt.Z:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.X:
                        MoveBackRight();
                        break;
                    default:
                        MoveBackLeft();
                        break;
                }
            }

            //x = 1
            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidRightRight();
                        break;
                    case TouchDirt.Z:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.X:
                        MoveFrontRight();
                        break;
                    default:
                        MoveFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidRightRight();
                        break;
                    case TouchDirt.Z:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.X:
                        MoveMidFrontRight();
                        break;
                    default:
                        MoveMidFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidRightRight();
                        break;
                    case TouchDirt.Z:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.X:
                        MoveBackRight();
                        break;
                    default:
                        MoveBackLeft();
                        break;
                }
            }

            //x = 2
            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveRightRight();
                        break;
                    case TouchDirt.Z:
                        MoveRightLeft();
                        break;
                    case TouchDirt.X:
                        MoveFrontRight();
                        break;
                    default:
                        MoveFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveRightRight();
                        break;
                    case TouchDirt.Z:
                        MoveRightLeft();
                        break;
                    case TouchDirt.X:
                        MoveMidFrontRight();
                        break;
                    default:
                        MoveMidFrontLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 3.6) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveRightRight();
                        break;
                    case TouchDirt.Z:
                        MoveRightLeft();
                        break;
                    case TouchDirt.X:
                        MoveBackRight();
                        break;
                    default:
                        MoveBackLeft();
                        break;
                }
            }
        }

        private void DecideMoveBack(Transform touchePlane, TouchDirt dirt)
        {
            if (dirt == TouchDirt.None)
            {
                return;
            }
            var position = touchePlane.position;
            _planeX = position.x;
            _planeY = position.y;
            _planeZ = position.z;
            
            //x = 0, y = 2, red
            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveBotRight();
                        break;
                    case TouchDirt.NegX:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveRightRight();
                        break;
                    default:
                        MoveRightLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.NegX:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveRightRight();
                        break;
                    default:
                        MoveRightLeft();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveTopRight();
                        break;
                    case TouchDirt.NegX:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveRightRight();
                        break;
                    default:
                        MoveRightLeft();
                        break;
                }
            }

            //x = 1
            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveBotRight();
                        break;
                    case TouchDirt.NegX:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidRightRight();
                        break;
                    default:
                        MoveMidRightLeft();
                        break;
                }
            }
            
            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.NegX:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidRightRight();
                        break;
                    default:
                        MoveMidRightLeft();
                        break;
                }
            }
            
            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveTopRight();
                        break;
                    case TouchDirt.NegX:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveMidRightRight();
                        break;
                    default:
                        MoveMidRightLeft();
                        break;
                }
            }
            
            //x = 2
            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 0.9) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveBotRight();
                        break;
                    case TouchDirt.NegX:
                        MoveBotLeft();
                        break;
                    case TouchDirt.Y:
                        MoveLeftRight();
                        break;
                    default:
                        MoveLeftLeft();
                        break;
                }
            }
            
            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 2.0) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveMidTopRight();
                        break;
                    case TouchDirt.NegX:
                        MoveMidTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveLeftRight();
                        break;
                    default:
                        MoveLeftLeft();
                        break;
                }
            }
            
            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 3.1) < 0.05 && Math.Abs(_planeZ - 3.6) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.X:
                        MoveTopRight();
                        break;
                    case TouchDirt.NegX:
                        MoveTopLeft();
                        break;
                    case TouchDirt.Y:
                        MoveLeftRight();
                        break;
                    default:
                        MoveLeftLeft();
                        break;
                }
            }
        }
        
        private void DecideMoveBot(Transform touchPlane, TouchDirt dirt)
        {
            if (dirt == TouchDirt.None)
            {
                return;
            }

            var position = touchPlane.position;
            _planeX = position.x;
            _planeY = position.y;
            _planeZ = position.z;
            
            //x = 0, y = 2, blue
            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.Z:
                        MoveLeftRight();
                        break;
                    case TouchDirt.X:
                        MoveBackLeft();
                        break;
                    default:
                        MoveBackRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.Z:
                        MoveLeftRight();
                        break;
                    case TouchDirt.X:
                        MoveMidFrontLeft();
                        break;
                    default:
                        MoveMidFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 0.9) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveLeftLeft();
                        break;
                    case TouchDirt.Z:
                        MoveLeftRight();
                        break;
                    case TouchDirt.X:
                        MoveFrontLeft();
                        break;
                    default:
                        MoveFrontRight();
                        break;
                }
            }

            //x = 1
            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.Z:
                        MoveMidRightRight();
                        break;
                    case TouchDirt.X:
                        MoveBackLeft();
                        break;
                    default:
                        MoveBackRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.Z:
                        MoveMidRightRight();
                        break;
                    case TouchDirt.X:
                        MoveMidFrontLeft();
                        break;
                    default:
                        MoveMidFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 2.0) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveMidRightLeft();
                        break;
                    case TouchDirt.Z:
                        MoveMidRightRight();
                        break;
                    case TouchDirt.X:
                        MoveFrontLeft();
                        break;
                    default:
                        MoveFrontRight();
                        break;
                }
            }

            //x = 2
            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 3.1) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveRightLeft();
                        break;
                    case TouchDirt.Z:
                        MoveRightRight();
                        break;
                    case TouchDirt.X:
                        MoveBackLeft();
                        break;
                    default:
                        MoveBackRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 2.0) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveRightLeft();
                        break;
                    case TouchDirt.Z:
                        MoveRightRight();
                        break;
                    case TouchDirt.X:
                        MoveMidFrontLeft();
                        break;
                    default:
                        MoveMidFrontRight();
                        break;
                }
            }

            if (Math.Abs(_planeX - 3.1) < 0.05 && Math.Abs(_planeY - 0.4) < 0.05 && Math.Abs(_planeZ - 0.9) < 0.05)
            {
                switch (dirt)
                {
                    case TouchDirt.NegZ:
                        MoveRightLeft();
                        break;
                    case TouchDirt.Z:
                        MoveRightRight();
                        break;
                    case TouchDirt.X:
                        MoveFrontLeft();
                        break;
                    default:
                        MoveFrontRight();
                        break;
                }
            }
        }

        private enum MoveWay
        {
            TopRight, TopLeft, LeftRight, LeftLeft,RightRight,RightLeft,BotRight,BotLeft,FrontRight,FrontLeft,
            MidTopRight, MidTopLeft, BackRight, BackLeft, MidRightRight,MidRightLeft, MidFrontRight, MidFrontLeft
        }
        private static int _stepsCap = 20;

        private bool _allowAddStep = true;
        
        List<MoveWay> steps = new List<MoveWay>(_stepsCap);

        private void AddSteps(MoveWay moveWay)
        {
            if (_allowAddStep)
            {
                if (steps.Count < _stepsCap)
                {
                    steps.Add(moveWay);
                }
                else
                {
                    steps.RemoveAt(0);
                    steps.Add(moveWay);
                }
            }
        }

        public void ClearSteps()
        {
            steps.Clear();
        }
        
        internal void Redo()
        {
            if (steps.Count == 0)
            {
                return;
            }

            if (!_movingCube)
            {
                _allowAddStep = false;
            MoveWay moveWay = steps[steps.Count - 1];
            steps.RemoveAt(steps.Count - 1);
            switch (moveWay)
            {
                case MoveWay.TopLeft:
                    MoveTopRight();
                    break;
                case MoveWay.TopRight:
                    MoveTopLeft();
                    break;
                case MoveWay.BackLeft:
                    MoveBackRight();
                    break;
                case MoveWay.BackRight:
                    MoveBackLeft();
                    break;
                case MoveWay.BotLeft:
                    MoveBotRight();
                    break;
                case MoveWay.BotRight:
                    MoveBotLeft();
                    break;
                case MoveWay.FrontLeft:
                    MoveFrontRight();
                    break;
                case MoveWay.FrontRight:
                    MoveFrontLeft();
                    break;
                case MoveWay.LeftLeft:
                    MoveLeftRight();
                    break;
                case MoveWay.LeftRight:
                    MoveLeftLeft();
                    break;
                case MoveWay.RightLeft:
                    MoveRightRight();
                    break;
                case MoveWay.RightRight:
                    MoveRightLeft();
                    break;
                case MoveWay.MidFrontLeft:
                    MoveMidFrontRight();
                    break;
                case MoveWay.MidFrontRight:
                    MoveMidFrontLeft();
                    break;
                case MoveWay.MidRightLeft:
                    MoveMidRightRight();
                    break;
                case MoveWay.MidRightRight:
                    MoveMidRightLeft();
                    break;
                case MoveWay.MidTopLeft:
                    MoveMidTopRight();
                    break;
                case MoveWay.MidTopRight:
                    MoveMidTopLeft();
                    break;
            }

            _allowAddStep = true;
            }
        }
        
        
        internal void RandomTheCube()
        {
            RandomCube();
            InvokeRepeating(nameof(Randomize), 0.0f, 0.4f);
        }

        //移动方法,不检查是否停止
        private void MoveTopRight()
        {
            Top();
            _logic.TopRight();
            AddSteps(MoveWay.TopRight);
            _topRight = true;
            _movingCube = true;
        }

        private void MoveTopLeft()
        {
            Top();
            _logic.TopLeft();
            AddSteps(MoveWay.TopLeft);
            _topLeft = true;
            _movingCube = true;
        }

        private void MoveLeftRight()
        {
            Left();
            _logic.LeftRight();
            AddSteps(MoveWay.LeftRight);
            _leftRight = true;
            _movingCube = true;
        }

        private void MoveLeftLeft()
        {
            Left();
            _logic.LeftLeft();
            AddSteps(MoveWay.LeftLeft);
            _leftLeft = true;
            _movingCube = true;
        }

        private void MoveRightRight()
        {
            Right();
            _logic.RightRight();
            AddSteps(MoveWay.RightRight);
            _rightRight = true;
            _movingCube = true;
        }

        private void MoveRightLeft()
        {
            Right();
            _logic.RightLeft();
            AddSteps(MoveWay.RightLeft);
            _rightLeft = true;
            _movingCube = true;
        }

        private void MoveBotRight()
        {
            Bottom();
            _logic.BotRight();
            AddSteps(MoveWay.BotRight);
            _botRight = true;
            _movingCube = true;
        }

        private void MoveBotLeft()
        {
            Bottom();
            _logic.BotLeft();
            AddSteps(MoveWay.BotLeft);
            _botLeft = true;
            _movingCube = true;
        }

        private void MoveFrontRight()
        {
            Front();
            _logic.FrontRight();
            AddSteps(MoveWay.FrontRight);
            _frontRight = true;
            _movingCube = true;
        }

        private void MoveFrontLeft()
        {
            Front();
            _logic.FrontLeft();
            AddSteps(MoveWay.FrontLeft);
            _frontLeft = true;
            _movingCube = true;
        }

        private void MoveMidTopRight()
        {
            MidTop();
            _logic.MidTopRight();
            AddSteps(MoveWay.MidTopRight);
            _midTopRight = true;
            _movingCube = true;
        }

        private void MoveMidTopLeft()
        {
            MidTop();
            _logic.MidTopLeft();
            AddSteps(MoveWay.MidTopLeft);
            _midTopLeft = true;
            _movingCube = true;
        }

        private void MoveBackRight()
        {
            Back();
            _logic.BackRight();
            AddSteps(MoveWay.BackRight);
            _backRight = true;
            _movingCube = true;
        }

        private void MoveBackLeft()
        {
            Back();
            _logic.BackLeft();
            AddSteps(MoveWay.BackLeft);
            _backLeft = true;
            _movingCube = true;
        }

        private void MoveMidRightRight()
        {
            MidRight();
            _logic.MidRightRight();
            AddSteps(MoveWay.MidRightRight);
            _midRightRight = true;
            _movingCube = true;
        }

        private void MoveMidRightLeft()
        {
            MidRight();
            _logic.MidRightLeft();
            AddSteps(MoveWay.MidRightLeft);
            _midRightLeft = true;
            _movingCube = true;
        }

        private void MoveMidFrontRight()
        {
            MidFront();
            _logic.MidFrontRight();
            AddSteps(MoveWay.MidFrontRight);
            _midFrontRight = true;
            _movingCube = true;
        }

        private void MoveMidFrontLeft()
        {
            MidFront();
            _logic.MidFrontLeft();
            AddSteps(MoveWay.MidFrontLeft);
            _midFrontLeft = true;
            _movingCube = true;
        }

        private float _xDis;
        private float _yDis;
        private float _zDis;
        
        private TouchDirt MoveControlUlt(Vector3 a, Vector3 b)
        {
            if ((a - b).magnitude < 0.15)
            {
                return TouchDirt.None;
            }

            _xDis = b.x - a.x;
            _yDis = b.y - a.y;
            _zDis = b.z - a.z;

            //X or NegX
            if (Math.Abs(_xDis) >= Math.Abs(_yDis) && Math.Abs(_xDis) >= Math.Abs(_zDis))
            {
                if (_xDis > 0)
                {
                    return TouchDirt.X;
                }
                
                return TouchDirt.NegX;
            }
            if (Math.Abs(_yDis) >= Math.Abs(_xDis) && Math.Abs(_yDis) >= Math.Abs(_zDis))
            {
                if (_yDis > 0)
                {
                    return TouchDirt.Y;
                }
                
                return TouchDirt.NegY;
            }
            if (Math.Abs(_zDis) >= Math.Abs(_xDis) && Math.Abs(_zDis) >= Math.Abs(_yDis))
            {
                if (_zDis > 0)
                {
                    return TouchDirt.Z;
                }

                return TouchDirt.NegZ;
            }
            
            return TouchDirt.None;
        }

        //实际转圈代码，同时检查是否停止
        private void UpdateMove()
        {
            if (_topRight)
            {
                _tempP.Rotate(Vector3.up * (Time.deltaTime * speed));
                CheckTopStop();
            }

            if (_topLeft)
            {
                _tempP.Rotate(Vector3.down * (Time.deltaTime * speed));
                CheckTopStop();
            }
            if (_leftRight)
            {
                _tempP.Rotate(Vector3.left * (Time.deltaTime * speed));
                CheckLeftStop();
            }
            if (_leftLeft)
            {
                _tempP.Rotate(Vector3.right * (Time.deltaTime * speed));
                CheckLeftStop();
            }
            if (_rightLeft)
            {
                _tempP.Rotate(Vector3.right * (Time.deltaTime * speed));
                CheckRightStop();
            }
            if (_rightRight)
            {
                _tempP.Rotate(Vector3.left * (Time.deltaTime * speed));
                CheckRightStop();
            }
            if (_botLeft)
            {
                _tempP.Rotate(Vector3.down * (Time.deltaTime * speed));
                CheckBotStop();
            }
            if (_botRight)
            {
                _tempP.Rotate(Vector3.up * (Time.deltaTime * speed));
                CheckBotStop();
            }
            if (_frontRight)
            {
                _tempP.Rotate(Vector3.back * (Time.deltaTime * speed));
                CheckFrontStop();
            }
            if (_frontLeft)
            {
                _tempP.Rotate(Vector3.forward * (Time.deltaTime * speed));
                CheckFrontStop();
            }
            if (_backRight)
            {
                _tempP.Rotate(Vector3.back * (Time.deltaTime * speed));
                CheckBackStop();
            }
            if (_backLeft)
            {
                _tempP.Rotate(Vector3.forward * (Time.deltaTime * speed));
                CheckBackStop();
            }
            if (_midTopLeft)
            {
                _tempP.Rotate(Vector3.down * (Time.deltaTime * speed));
                CheckMidTopStop();
            }
            if (_midTopRight)
            {
                _tempP.Rotate(Vector3.up * (Time.deltaTime * speed));
                CheckMidTopStop();
            }
            if (_midRightLeft)
            {
                _tempP.Rotate(Vector3.right * (Time.deltaTime * speed));
                CheckMidRightStop();
            }
            if (_midRightRight)
            {
                _tempP.Rotate(Vector3.left * (Time.deltaTime * speed));
                CheckMidRightStop();
            }
            if (_midFrontLeft)
            {
                _tempP.Rotate(Vector3.forward * (Time.deltaTime * speed));
                CheckMidFrontStop();
            }
            if (_midFrontRight)
            {
                _tempP.Rotate(Vector3.back * (Time.deltaTime * speed));
                CheckMidFrontStop();
            }
        }
        internal void ReStart()
        {
            _logic.createCube();
            _mc.ResetColor();
        }
        
        List<MoveWay> randomSteps = new List<MoveWay>();

        private void RandomCube()
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                int x = random.Next(18);
                switch (x)
                {
                    case 0:
                        randomSteps.Add(MoveWay.FrontLeft);
                        break;
                    case 1:
                        randomSteps.Add(MoveWay.FrontRight);
                        break;
                    case 2:
                        randomSteps.Add(MoveWay.MidFrontLeft);
                        break;
                    case 3:
                        randomSteps.Add(MoveWay.MidFrontRight);
                        break;
                    case 4:
                        randomSteps.Add(MoveWay.BackLeft);
                        break;
                    case 5:
                        randomSteps.Add(MoveWay.BackRight);
                        break;
                    case 6:
                        randomSteps.Add(MoveWay.LeftLeft);
                        break;
                    case 7:
                        randomSteps.Add(MoveWay.LeftRight);
                        break;
                    case 8:
                        randomSteps.Add(MoveWay.MidRightLeft);
                        break;
                    case 9:
                        randomSteps.Add(MoveWay.MidRightRight);
                        break;
                    case 10:
                        randomSteps.Add(MoveWay.RightLeft);
                        break;
                    case 11:
                        randomSteps.Add(MoveWay.RightRight);
                        break;
                    case 12:
                        randomSteps.Add(MoveWay.TopLeft);
                        break;
                    case 13:
                        randomSteps.Add(MoveWay.TopRight);
                        break;
                    case 14:
                        randomSteps.Add(MoveWay.MidTopLeft);
                        break;
                    case 15:
                        randomSteps.Add(MoveWay.MidTopRight);
                        break;
                    case 16:
                        randomSteps.Add(MoveWay.BotLeft);
                        break;
                    case 17:
                        randomSteps.Add(MoveWay.BotRight);;
                        break;
                }
            }
        }

        private void Randomize()
        {
            if (!_movingCube)
            {
                MoveWay moveWay = randomSteps[0];
                randomSteps.RemoveAt(0);
                switch (moveWay)
                {
                    case MoveWay.TopLeft:
                        MoveTopRight();
                        break;
                    case MoveWay.TopRight:
                        MoveTopLeft();
                        break;
                    case MoveWay.BackLeft:
                        MoveBackRight();
                        break;
                    case MoveWay.BackRight:
                        MoveBackLeft();
                        break;
                    case MoveWay.BotLeft:
                        MoveBotRight();
                        break;
                    case MoveWay.BotRight:
                        MoveBotLeft();
                        break;
                    case MoveWay.FrontLeft:
                        MoveFrontRight();
                        break;
                    case MoveWay.FrontRight:
                        MoveFrontLeft();
                        break;
                    case MoveWay.LeftLeft:
                        MoveLeftRight();
                        break;
                    case MoveWay.LeftRight:
                        MoveLeftLeft();
                        break;
                    case MoveWay.RightLeft:
                        MoveRightRight();
                        break;
                    case MoveWay.RightRight:
                        MoveRightLeft();
                        break;
                    case MoveWay.MidFrontLeft:
                        MoveMidFrontRight();
                        break;
                    case MoveWay.MidFrontRight:
                        MoveMidFrontLeft();
                        break;
                    case MoveWay.MidRightLeft:
                        MoveMidRightRight();
                        break;
                    case MoveWay.MidRightRight:
                        MoveMidRightLeft();
                        break;
                    case MoveWay.MidTopLeft:
                        MoveMidTopRight();
                        break;
                    case MoveWay.MidTopRight:
                        MoveMidTopLeft();
                        break;
                }
            }
        }

        public bool MoveLock
        {
            get => _moveLock;
            set => _moveLock = value;
        }


        private void HandleMove(Vector3 point)
        {
            Ray camRay = _currentCam.ScreenPointToRay (point);
            Physics.Raycast(camRay, out _secHit);
            TouchDirt dirt = MoveControlUlt(_hitPosi, _secHit.point);
            if (_plane && !_movingCube && dirt != TouchDirt.None)
            {
                if (Math.Abs(_plane.position.z - 0.4) < 0.05)
                {
                    DecideMoveFront(_plane, dirt);
                    _allowMove = false;
                }
                else if (Math.Abs(_plane.position.y - 3.6) < 0.05)
                {
                    DecideMoveTop(_plane, dirt);
                    _allowMove = false;
                }
                else if (Math.Abs(_plane.position.x - 0.4) < 0.05)
                {
                    DecideMoveLeft(_plane, dirt);
                    _allowMove = false;
                }
                else if (Math.Abs(_plane.position.x - 3.6) < 0.05)
                {
                    DecideMoveRight(_plane, dirt);
                    _allowMove = false;
                }
                else if (Math.Abs(_plane.position.z - 3.6) < 0.05)
                {
                    DecideMoveBack(_plane, dirt);
                    _allowMove = false;
                }
                else if (Math.Abs(_plane.position.y - 0.4) < 0.05)
                {
                    DecideMoveBot(_plane, dirt);
                    _allowMove = false;
                }
            }
        }

        private void HandleCam(Touch touch)
        {
            switch (touch.phase)
            {
                            
                case TouchPhase.Began:
                                
                    _prePosi = _currentMoveCam.ScreenToViewportPoint(touch.position);
                    _inSubScreen = true;
                    break;
                        
                case TouchPhase.Moved:
                    if (_inSubScreen)
                    {
                        Vector3 direction = _prePosi - _currentMoveCam.ScreenToViewportPoint(touch.position);
                        _currentMoveCam.transform.position = new Vector3(2,2,2);
                        _currentMoveCam.transform.Rotate(new Vector3(1,0,0), direction.y * .1f);
                        _currentMoveCam.transform.Rotate(new Vector3(0,1,0), -direction.x * .1f);
                        _currentMoveCam.transform.Translate(new Vector3(0,0,-5));
                    
                        _prePosi = _currentMoveCam.ScreenToViewportPoint(touch.position);
                    }

                    if (touch.position.x >= 400 || touch.position.y >= 445)
                    {
                        _inSubScreen = false;
                    }
                    break;

                case TouchPhase.Ended:
                    _inSubScreen = false;
                    break;
            }
        }

        private void HandleCube(Touch touch)
        {
            switch (touch.phase)
            {

                case TouchPhase.Began:
                    Ray camRay = _currentCam.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(camRay, out _hit))
                    {
                        _plane = _hit.transform;
                    }

                    _mouse = touch.position;
                    _hitPosi = _hit.point;
                    _inMainScreen = true;
                    break;

                case TouchPhase.Moved:
                    if (touch.position.x <= 400)
                    {
                        _inMainScreen = false;
                    }

                    if (_allowMove && _inMainScreen)
                    {
                        if (_plane)
                        {
                            HandleMove(touch.position);
                        }
                    }

                    break;

                case TouchPhase.Ended:
                    _plane = null;
                    _allowMove = true;
                    _inMainScreen = false;
                    break;
            }
        }

        private bool _singleHand;
        private void Update()
        {
            if (randomSteps.Count == 0)
            {
                CancelInvoke();
            }
            /*
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    _touch = Input.GetTouch(i);
                    if (_touch.position.x > 400)
                    {
                        if (!_moveLock)
                        {
                            HandleCube(_touch);
                        }
                    }
                    else if (_touch.position.x <= 400 && _touch.position.y <= 445)
                    {
                        HandleCam(_touch);
                    }
                    else if (_touchTemp.position.x <= 400 && _touchTemp.position.y > 445)
                    {
                    }
                }
            }*/
            if (Input.touchCount == 1)
            {
                _touch = Input.GetTouch(0);
                if (_touch.position.x > 400)
                {
                    if (!_moveLock)
                    {
                        HandleCube(_touch);
                    }
                }
                else if (_touch.position.x <= 400 && _touch.position.y <= 445)
                {
                    HandleCam(_touch);
                }
            }

            /*
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 1)
                {
                    _touchTemp = Input.GetTouch(0);
                    if (_touchTemp.position.x > 400)
                    {
                        HandleCube(_touchTemp);
                    }
                    else if (_touchTemp.position.x <= 400 && _touchTemp.position.y <= 445)
                    {
                        HandleCam(_touchTemp);
                    }
                    else if (_touchTemp.position.x <= 400 && _touchTemp.position.y > 445)
                    {
                    }
                    
                }
                else
                {
                    _touchTemp = Input.GetTouch(0);
                    _touchTempTwo = Input.GetTouch(1);
                    if (_touchTemp.position.x > 400)
                    {
                        _touchMain = Input.GetTouch(0);
                        if (_touchTempTwo.position.x <= 400 && _touchTempTwo.position.y <= 445)
                        {
                            _touchSub = Input.GetTouch(1);
                        }
                        else
                        {
                            _touchC = Input.GetTouch(1);
                        }
                    }
                    else if (_touchTemp.position.x <= 400 && _touchTemp.position.y <= 445)
                    {
                        _touchSub = Input.GetTouch(0);
                        _touchTempTwo = Input.GetTouch(1);
                        if (_touchTempTwo.position.x > 400)
                        {
                            _touchMain = Input.GetTouch(1);
                        }
                        else
                        {
                            _touchC = Input.GetTouch(1);
                        }
                    }
                    else
                    {
                        _touchC = Input.GetTouch(0);
                        if (_touchTempTwo.position.x > 400)
                        {
                            _touchMain = Input.GetTouch(1);
                        }
                        else
                        {
                            _touchSub = Input.GetTouch(1);
                        }
                    }
                    
                    HandleCube(_touchMain);
                    HandleCam(_touchSub);
                }
            }
            */
            UpdateMove();

            if (Input.GetMouseButtonDown(0) && !_moveLock)
            {
                if (Input.mousePosition.x > 400)
                {
                    Ray camRay = _currentCam.ScreenPointToRay (Input.mousePosition);
                    if (Physics.Raycast(camRay, out _hit))
                    {
                        _plane = _hit.transform;
                    }

                    _mouse = Input.mousePosition;
                    _hitPosi = _hit.point;
                    _inMainScreen = true;
                }
            }

            if (Input.GetMouseButton(0) && !_moveLock)
            {
                if (Input.mousePosition.x <= 400)
                {
                    _inMainScreen = false;
                }
                
                if (_allowMove && _inMainScreen)
                {
                    if (_plane)
                    {
                        if (_mouse != Input.mousePosition)
                        {
                            HandleMove(Input.mousePosition);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && !_moveLock)
            {
                _plane = null;
                _allowMove = true;
                _inMainScreen = false;
            }

        }
    }
}
