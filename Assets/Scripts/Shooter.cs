using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;

    int shotPower = MaxShotPower;
    AudioSource shotSound;

    public GameObject[] candyPrefabs;//GameObject型でプレハブ登録
    public Transform candyParentTransform;//親子関係を結びたい　
    //GameObject型にするとcandyParent
    //candy.transform.parent=GetComponent<Transform>();
    //またはcandyParent.transformにしないといけない
    //(GetComponet.transformだけど、transoformだから省略できる)
    //親にしたいオブジェクトのTransform componentが必要
    public CandyManager candyManager;
    //CandyManagerコンポーネントを持ったゲームオブジェクトをインスペクター上から登録できるようになる
    //登録しないとNullのまま　
    //shooterからcandyManagerを使えるようにする
    //staticを使ったら上記しなくてもクラス名.で使用可能
    //staticはみんなで１つの情報を共有しているがgameOBject作る方法はそれぞれが
    //それぞれの情報を持っている
    
    //createemptyしてスクリプトをアタッチしてシーンに登場したらインスタンス化
    //staticはインスタンス化必要ない

    public float shotForce;
    public float shotTorque;//Toeque回転する力、回転力
    public float baseWidth;//StageObjectのBase（作成したやつ）のwidth

    private void Start()
    {
        shotSound = GetComponent<AudioSource>();
        //いきなりGetComponentはスクリプトがついているゲームオブジェクト
        //（shooter）
    }

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
        Debug.Log(hoge.num);
        hoge.num++;
        //キャンディを生成できる条件外ならばShotしない
        if (candyManager.GetCandyAmount() <= 0) return;
        
        //現在のshotpowerが0だったらキャンディ投入をキャンセル
        if (shotPower <= 0) return;

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
       //親になるオブジェクトを設定する際には、Transform.Parentのプロパティに、親になるGameobjectのTransformを代入

        //CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();//candyのrigidbodyにアクセス
        candyRigidBody.AddForce(transform.forward * shotForce);//forward→その物体がz軸を向けている方向が前
        //transform.forward自分が向いている方向に shotForce力を加える
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
        //回転力を与える

        //Candyのストックを消費
        candyManager.ConsumeCandy();

        //ShotPowerを消費
        ConsumePower();

        shotSound.Play();
    }

    private void OnGUI()
    {
        GUI.color = Color.black;

        //ShotPowerの残数を+の数で表示
        string label = "";
        for (int i = 0; i < shotPower; i++)
        {
            label = label + "+";
        }
        GUI.Label(new Rect(50, 65, 100, 30), label);
    }

    void ConsumePower()
    {
        //ShotPowerを消費すると同時に回復のカウントをスタート
        shotPower--;
        StartCoroutine(RecoverPower());
        //5回連続でクリックすると同時に5個のコルーチンが走る
    }

    IEnumerator RecoverPower()
    {
        //一定数待った後にshotPowerを回復
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
