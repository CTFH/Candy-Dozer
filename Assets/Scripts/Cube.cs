using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);    
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, 0, 0.1f);
        //translateで移動する
        //0.1f=10㎝
        //１秒間に6m
        transform.Rotate(0, 2f, 0);
        //Rotateで回転する

        transform.position = new Vector3(3 * Mathf.Cos(Time.time*3), Mathf.Sin(Time.time*3), Time.time*3);
    }
}
