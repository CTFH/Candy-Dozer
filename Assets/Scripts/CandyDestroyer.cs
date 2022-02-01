using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;

    private void OnTriggerEnter(Collider other)
       //isTriggerなコライダーをもったゲームオブジェクトに足して作用する
       //メソッドがmonoBehaviorにあって、それをオーバーライドしている
       //otherは自分じゃないというときに使ったりする
    {
        if(other.gameObject.tag=="Candy")
            {
            //指定数だけCanduのストックを増やす
            candyManager.AddCandy(reward);
            
            //オブジェクトを削除
            Destroy(other.gameObject);
                //otherがなかったらCandyDestroyerが消えちゃう
            }
    }
}
