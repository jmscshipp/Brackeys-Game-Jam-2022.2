using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimations : MonoBehaviour
{
    public Animator anim;

    private void OnMouseOver() {
        anim.Play("Button Animation 1");
    }

    private void OnMouseExit() {
        anim.Play("Button Idle");
    }
}
