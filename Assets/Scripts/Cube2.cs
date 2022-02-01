using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{
    Vector3 axis = Vector3.zero;
    //.zeroで(0,0,0)のベクトル
    //.oneで(1,1,1)
    void Start()
    {
        StartCoroutine(RotateCube());
        //呼ぶ際は通常と違ってスタートコルーチンの中でメソッドを呼ぶ
    }

    void Update()
        //1回呼び出すと1度×1秒間に60回
    {
        transform.Rotate(axis);
        //今回しても軸が0,0,0なので何もおこらない
        //マルチスレッドのような感じで行っているので同時進行のようになっている
        //（Updateはずっと1秒間に60回回っていて、同時にコルーチンが並行でx座標やy座標を動かしている）
    }
    IEnumerator RotateCube()
        {
        //axis.y = 1f;//0,0,0,のy座標に1を設定    
        //yield return new WaitForSeconds(3f);
        //axis.y = 0;
        //axis.x = 1f;
        //yield return new WaitForSeconds(2f);
        //axis.x = 0;
        //axis.z = 1f;
        yield return new WaitForSeconds(5f);
        axis.y = 1f;

        //コルーチンの中では最低１回のyeild returnの記述が必要
            yield return null;
        }
}
