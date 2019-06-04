using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{

    public Sprite[] resultLabels = new Sprite[6];

    Animator animator;
    Image image;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void ShowResult(int id) {
        image.sprite = resultLabels[id];
        animator.SetTrigger("ShowResult");
    }
}
