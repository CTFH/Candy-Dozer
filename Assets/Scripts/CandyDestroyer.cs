using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public GameObject effectPrefab;
    public GameObject effectGoldPrefab;
    public Vector3 effectRotation;

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

            if(effectPrefab !=null && effectGoldPrefab != null)
                //横に落ちた時がNull
            {
                if (other.gameObject.tag == "Candy")
                {

                    //Candyのポジションにエフェクトを生成
                    Instantiate(
                        effectPrefab,//何を（パーティクル）
                        other.transform.position,//どこに
                                                 //other=destroyerに入ってきたcandy
                                                 //otherはcube(candy)
                        Quaternion.Euler(effectRotation)
                        //どの向きで
                        //effectRotation入れないと0,0,0で生成しちゃう
                        //effectRotationはVector3でInspectorから登録した90
                        //Quaternionは回転情報
                        //どの向きに物体を回転させるか（どの向きで登場させるか）
                        //Quaternionは４次元数
                        );
                }
                else
                {
                    Instantiate(
                                effectGoldPrefab,
                                other.transform.position,
                                 Quaternion.Euler(effectRotation)
                                 );
                }
            }
            }
    }
}
