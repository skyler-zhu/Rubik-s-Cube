using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public struct Box
    {

        private int id;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public Box(int id)
        {
            this.id = id;
        }
    }
    
    private int _count;
    private Box[,,] cubeData;
    private int tempId;
    private int size;
    private Box tempBox;

    public int Count
    {
        get => _count;
        set => _count = value;
    }

    public Box[,,] CubeData
    {
        get => cubeData;
        set => cubeData = value;
    }

    private void MoveBox(int a1, int a2, int a3, int b1, int b2, int b3, int c1, int c2, int c3, int d1, int d2,
                int d3)
    {
        tempBox = cubeData[a1, a2, a3];
        cubeData[a1, a2, a3] = cubeData[b1, b2, b3];
        cubeData[b1, b2, b3] = cubeData[c1, c2, c3];
        cubeData[c1, c2, c3] = cubeData[d1, d2, d3];
        cubeData[d1, d2, d3] = tempBox;
    }


    public void BotRight()
    {
        MoveBox(0, 0, 0, 2, 0, 0, 2, 2, 0, 0, 2, 0);
        MoveBox(1, 0, 0, 2, 1, 0, 1, 2, 0, 0, 1, 0);
    }

    public void BotLeft()
    {
        MoveBox(0, 0, 0, 0, 2, 0, 2, 2, 0, 2, 0, 0);
        MoveBox(1, 0, 0, 0, 1, 0, 1, 2, 0, 2, 1, 0);
    }

    public void MidTopRight()
    { 
        MoveBox(0, 0, 1, 2, 0, 1, 2, 2, 1, 0, 2, 1);
        MoveBox(1, 0, 1, 2, 1, 1, 1, 2, 1, 0, 1, 1);
    }

    public void MidTopLeft()
    {
        MoveBox(0, 0, 1, 0, 2, 1, 2, 2, 1, 2, 0, 1);
        MoveBox(1, 0, 1, 0, 1, 1, 1, 2, 1, 2, 1, 1);
    }

    public void TopRight()
    {
        MoveBox(0, 0, 2, 2, 0, 2, 2, 2, 2, 0, 2, 2);
        MoveBox(1, 0, 2, 2, 1, 2, 1, 2, 2, 0, 1, 2);
    }

    public void TopLeft()
    {
        MoveBox(0, 0, 2, 0, 2, 2, 2, 2, 2, 2, 0, 2);
        MoveBox(1, 0, 2, 0, 1, 2, 1, 2, 2, 2, 1, 2);
    }

    public void FrontRight()
    { 
        MoveBox(2, 0, 1, 1, 0, 2, 0, 0, 1, 1, 0, 0); 
        MoveBox(2, 0, 0, 2, 0, 2, 0, 0, 2, 0, 0, 0); 
    }

    public void FrontLeft()
    {
        MoveBox(1, 0, 0, 0, 0, 1, 1, 0, 2, 2, 0, 1);
        MoveBox(0, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 0);
    }

    public void MidFrontRight()
    {
        MoveBox(2, 1, 1, 1, 1, 2, 0, 1, 1, 1, 1, 0);
        MoveBox(2, 1, 0, 2, 1, 2, 0, 1, 2, 0, 1, 0);
    }

    public void MidFrontLeft()
    {
        MoveBox(1, 1, 0, 0, 1, 1, 1, 1, 2, 2, 1, 1);
        MoveBox(0, 1, 0, 0, 1, 2, 2, 1, 2, 2, 1, 0);
    }

    public void BackRight()
    {
        MoveBox(2, 2, 1, 1, 2, 2, 0, 2, 1, 1, 2, 0);
        MoveBox(2, 2, 0, 2, 2, 2, 0, 2, 2, 0, 2, 0);
    }

    public void BackLeft()
    {
        MoveBox(1, 2, 0, 0, 2, 1, 1, 2, 2, 2, 2, 1);
        MoveBox(0, 2, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0);
    }

    public void LeftRight()
    {
        MoveBox(0, 0, 2, 0, 2, 2, 0, 2, 0, 0, 0, 0);
        MoveBox(0, 0, 1, 0, 1, 2, 0, 2, 1, 0, 1, 0);
    }

    public void LeftLeft()
    {
        MoveBox(0, 0, 0, 0, 2, 0, 0, 2, 2, 0, 0, 2); 
        MoveBox(0, 1, 0, 0, 2, 1, 0, 1, 2, 0, 0, 1);
    }

    public void MidRightRight()
    {
        MoveBox(1, 0, 2, 1, 2, 2, 1, 2, 0, 1, 0, 0);
        MoveBox(1, 0, 1, 1, 1, 2, 1, 2, 1, 1, 1, 0);
    }

    public void MidRightLeft()
    {
        MoveBox(1, 0, 0, 1, 2, 0, 1, 2, 2, 1, 0, 2);
        MoveBox(1, 1, 0, 1, 2, 1, 1, 1, 2, 1, 0, 1);
    }

    public void RightRight()
    {
        MoveBox(2, 0, 2, 2, 2, 2, 2, 2, 0, 2, 0, 0);
        MoveBox(2, 0, 1, 2, 1, 2, 2, 2, 1, 2, 1, 0);
    }

    public void RightLeft()
    {
        MoveBox(2, 0, 0, 2, 2, 0, 2, 2, 2, 2, 0, 2);
        MoveBox(2, 1, 0, 2, 2, 1, 2, 1, 2, 2, 0, 1);
    }

    public void DrawMap(char xyz)
    {
        if (xyz == 'z')
        {
            for (int q = 0; q < 3; q++)
            {
                for (int i = 2; i >= 0; i--)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        print(CubeData[x, i, q].Id);
                    }
                }
                print("");
            }
        }
        else if (xyz == 'y')
        {
            for (int q = 0; q < 3; q++)
            {
                for (int i = 2; i >= 0; i--)
                {
                    for (int x = 2; x >= 0; x--)
                    {
                        print(CubeData[q, x, i].Id);
                    }
                }
                print("");
            }
        }
        else if (xyz == 'x')
        {
            for (int q = 0; q < 3; q++)
            {
                for (int i = 2; i >= 0; i--)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        print( CubeData[x, q, i].Id);
                    }
                }
                print("");
            }
        }
    }

    public Boolean CheckWin()
    {
        tempId = 1;
        for (int z = 0; z < size; z++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (cubeData[x, y, z].Id != tempId)
                    {
                        return false;
                    }
                    else
                    {
                        tempId += 1;
                    }
                }
            }
        }
        return true;
    }

    internal void createCube()
    {
        size = 3;
        _count = 0;
        cubeData = new Box[3, 3, 3];
        tempId = 1;
        for (var z = 0; z < 3; z++)
        for (var y = 0; y < 3; y++)
        for (var x = 0; x < 3; x++)
        {
            var box = new Box(tempId);
            cubeData[x, y, z] = box;
            tempId += 1;
        }

        tempId = 1;
    }

    private void Awake()
    {
        createCube();
    }
}
