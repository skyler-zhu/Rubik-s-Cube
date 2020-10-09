using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(this.transform.position);
        print(this.transform.rotation.eulerAngles);
        print("=========================");
    }
}
