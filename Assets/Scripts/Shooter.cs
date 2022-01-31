using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs;//GameObject型でプレハブ登録
    public Transform candyParentTransform;//親子関係を結びたい　
    //GameObject型にするとcandy Parent
    //transform.parent=candyParent.transformにしないといけない
    //(getComponet.transformだけど、transoformだから省略できる)
    //親にしたいオブジェクトのTransform componentが必要
    public float shotForce;
    public float shotTorque;//Toeque回転する力、回転力
    public float baseWidth;//StageObjectのBase（作成したやつ）のwidth

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();//Fire1はマウスの通常クリックと
        //Left controlに組み込まれてる
        //押してる間ずっとTrue
        //shotを呼ぶ
    }

    //キャンディプレハブからランダムに１つ選ぶ
    GameObject SampleCandy()//戻り値の型＝GameObject　関数名=SampleCandy
    {
        int index = Random.Range(0, candyPrefabs.Length);//Random.Range端含まない
        //0~3
        //floatの時は端含む（intは含まない）
        return candyPrefabs[index];
    }

    Vector3 GetInstantiatePosition() //戻り値の型=Vector3　メソッド名=GetInstantiatePosition
    {
        //画面サイズとInputの割合からキャンディ生成のポジションを計算
        float x = baseWidth *
            (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
        //input.mousePosition.x...クリックしたx座標
        //Base Width（-2.5~2.5=5） transform.position=(0,2.5,-8)
        //画面の右端クリックしたらbase Widthの右端から発射するようにする
    }
    public void Shot()
    {
        //プレハブからキャンディオブジェクトを生成
        GameObject candy = (GameObject)Instantiate(//引数３つ
            SampleCandy(),
            GetInstantiatePosition(),
            //candyPrefab,//何を
            //transform.position,//どこに　
                               //いきなりtransformはこのスクリプトがアタッチされているトランスフォームコンポーネント
            Quaternion.identity//どの向きで　
                               //QuaternionIdentityはRotationが000を意味する　
                               //そこから発射
                               //Quaternion4次元数　３次元空間内での回転を表すときに使う
                               //ベクトルの３軸を空間上に作ってそれを回転させるのがquaternion
                               //個別（x軸y軸z軸）で回転を制御すると特定の条件である軸の回転が失われてしまう
                               //quaternion Euler(0,0,0)を使うとEuler角を内部でquaternionに変換
                               //QuaternionIdentity＝QuaternionEuler(0,0,0)
            ); 

        //生成したCandyオブジェクトの親をcandyParentTransformに設定する
        candy.transform.parent = candyParentTransform;
        //親子関係結ぶときはtransformをかませる
       //親になるオブジェクトを設定する際には、TransformParentのプロパティに、親になるGameobjectのTransformを代入

        //CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();//candyのrigidbodyにアクセス
        candyRigidBody.AddForce(transform.forward * shotForce);//forward→その物体がz軸を向けている方向が前
        //transform.forward自分が向いている方向に shotForce力を加える
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
        //回転力を与える
    }
}
