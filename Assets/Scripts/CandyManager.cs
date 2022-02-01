using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 10;

    //現在のキャンディストック数
    public int candy = DefaultCandyAmount;
    //ストック回復までの残り秒数
    int counter;//0

    public void ConsumeCandy()
    {
        if (candy > 0)
        {
            candy--;
        }
    }

    public int GetCandyAmount()
    {
        return candy;
    }

    public void AddCandy(int amount)
    {
        candy += amount;
    }

    private void OnGUI()//画面に文字を出力するやつ（もともとあるメソッド）
    {
        //GUI.matrix = Matrix4x4.Scale(Vector3.one * 3);　
        //文字が小さいときに文字を大きくする　p145

        GUI.color = Color.black;

        //キャンディのストック数を表示
        string label = $"Candy :  { candy}";
        //string label = "Candy : " + candy;

        //name:Maz
        //age:23

        //Java
        //System.out.printf("名前：%s(%d才)",name,age);

        //jsp
        //<p>名前:<%= name%>(<%= age%>才)</p>

        //JavaScript
        //console.log(`名前:${name}(${age}才)`)

        //Python
        //print(f'名前：{name}({age}才)')

        //C#
        //Console.WriteLine($"名前:{name}({age}才)")


        //回復カウントしている時だけ秒数表示
        if (counter > 0)
        {
            label = label + "(" + counter + "s)";
        }
            //もしカウンターに数字が入っていたら

        GUI.Label(new Rect(50,50,100,30),label);
    }

    private void Update()
    {
       //キャンディのストックがデフォルトより少なく、
       //回復カウントをしていないときにカウントをスタートさせる
       if(candy < DefaultCandyAmount && counter <=0)
            //カウンターに値が入っていなかったら
            //最初0が入っててコルーチンスタート
            //コルーチンがスタートすると10が入ってカウントを始める
            //カウンターが0でなくなるのでコルーチン作動止まる
        {
            StartCoroutine(RecoverCandy());
        }
    }

    IEnumerator RecoverCandy()
        //子ルーチンメソッドの戻り値は必ずIEnumerator型
    {
        counter = RecoverySeconds;

        //1秒ずつカウントを進める
        while(counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            //1秒停止（後下に行く）
            //普通のreturnはreturnで抜ける。けどこれは違う。下に行く。
            counter--;
        }

        candy++;
    }
}
