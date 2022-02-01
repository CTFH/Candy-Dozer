using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoge : MonoBehaviour
{
    public static int num = 10;
    //他のスクリプトからアクセスするとき
    //staticだからcreate emptyしてアタッチしなくてOK
    //hoge.（クラス名.)で扱えるようになる
}
