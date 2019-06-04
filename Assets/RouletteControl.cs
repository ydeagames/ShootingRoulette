using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteControl : MonoBehaviour
{

    public float rot_speed = 100;    // 回転速度

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, this.rot_speed, 0);
        //this.rot_speed *= 0.96f;
    }
}
