using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
       //isTriggerなコライダーをもったゲームオブジェクトに足して作用する
       //メソッドがmonoBehaviorにあって、それをオーバーライドしている
    {
        if(other.gameObject.tag=="Candy")
            {
            //オブジェクトを削除
            Destroy(other.gameObject);
                //otherがなかったらCandyDestroyerが消えちゃう
            }
    }
}
