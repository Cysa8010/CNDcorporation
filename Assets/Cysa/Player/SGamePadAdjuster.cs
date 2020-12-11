using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* このクラスはゲームパッドの値を使いやすくするものです
 * 対応は以下の通り
 * - LTrigger : XInput.LT
 * - RTrigger : XInput.RT
 * - Left     : XInput.左アナログスティック左
 * - Right    : XInput.左アナログスティック右
 * - Front    : Xinput.左アナログスティック上
 * - Back     : XInput.左アナログスティック下
 * - Up       : XInput.右アナログスティック上
 * - Down     : XInput.右アナログスティック下  
 * - LRot     : XInput.右アナログスティック左
 * - RRot     : XInput.右アナログスティック右
 * 
 * 各キーの内部データについて
 * - Trigger : 押された時の最初のフレームだけtrue
 * - Press   : 押されている間true
 * - Release : 離された最初のフレームにtrue
 * - Value   : 直値
 * 
 * 1つのゲームオブジェクトからしか参照しないなら直差ししてもいいが
 * もし、複数のゲームオブジェクトから参照する場合は
 * SceneのルートにEmptyオブジェクトを作って差すことを推奨
 */

public class SGamePadAdjuster : MonoBehaviour
{
    
    public AnalogKey LTrigger;
    public AnalogKey RTrigger;
    public AnalogKey Left;
    public AnalogKey Right;
    public AnalogKey Front;
    public AnalogKey Back;
    public AnalogKey Up;
    public AnalogKey Down;
    public AnalogKey LRot;
    public AnalogKey RRot;
    // Start is called before the first frame update
    void Start()
    {
        LTrigger.Initialize();
        RTrigger.Initialize();
        Left.Initialize();
        Right.Initialize();
        Front.Initialize();
        Back.Initialize();
        Up.Initialize();
        Down.Initialize();
        LRot.Initialize();
        RRot.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        float value;
        value = Input.GetAxis("Horizontal");
        Left.Update(value, value < -0.8f, -0.8f <= value);
        Right.Update(value, 0.8f < value, value <= 0.8f);

        value = Input.GetAxis("Vertical");
        Front.Update(value, 0.8f < value, value <= 0.8f);
        Back.Update(value, value < -0.8f, -0.8f <= value);

        value = Input.GetAxis("Vertical2");
        Up.Update(value, value < -0.8f, -0.8f <= value);
        Down.Update(value, 0.8f < value, value <= 0.8f);

        value = Input.GetAxis("Horizontal2");
        LRot.Update(value, value < -0.8f, -0.8f <= value);
        RRot.Update(value, 0.8f < value, value <= 0.8f);

        value = Input.GetAxis("LTrigger");
        LTrigger.Update(value, 0 < value, value == 0);
        value = Input.GetAxis("RTrigger");
        RTrigger.Update(value, 0 < value, value == 0);

    }

    public struct AnalogKey
    {
        public bool trigger;
        public bool press;
        public bool release;
        public float value;
        public void Initialize()
        {
            trigger = press = release = false;
            //value = 0f;
        }
        public void Update(float value, bool isPress, bool isFree)
        {
            this.value = value;
            // Trigger
            if (!press && isPress)
            {
                trigger = true;
                press = true;
            }
            // Press Only
            else if (isPress)
            {
                trigger = false;
            }
            // Release
            else if (press && isFree)
            {
                trigger = false;
                press = false;
                release = true;
            }
            else if (release && isFree)
            {
                release = false;
            }
        }
    }

}
