using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Vector3 startPosition;

    public float amplitude;//増幅幅　増やすとpusherが大きく動く
    public float speed;

    void Start()
    {
        startPosition = transform.localPosition; 
        //scriptがついている物体のtransform.component
        //Stageの子要素に入っているので親からのズレ（ローカルポジション）
        //Stageからのズレ＝ローカルポジション
    }

    void Update()
    {
        //変位を計算
        float z = amplitude * Mathf.Sin(Time.time * speed);
        //小数点z
        //MathfクラスのSin関数（-1~1)
        //Time.timeゲームが開始してからの時間
        //Sin（角度の代わりに時間×速さ）
        //Sin（）に増幅幅をかける
        //板を動かす大きさを決めたい
        //スピードを早くするとSinの周期が早くなるので板の動く周期が早くなる


        //zを変位させたポジションに再設定
        transform.localPosition = startPosition + new Vector3(0, 0, z);
        //もとのStartポジションにZを足して計算してtransform.localPositionを更新
        //i秒間に60回
    }
}
