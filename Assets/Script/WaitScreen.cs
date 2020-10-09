using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitScreen : MonoBehaviour
{
    public Animator animator1;
    private static readonly int In = Animator.StringToHash("In");
    private static readonly int Out = Animator.StringToHash("Out");

    public void FadeIn()
    {
        animator1.SetTrigger(In);
    }

    public void FadeOut()
    {
        animator1.SetTrigger(Out);
    }
}
