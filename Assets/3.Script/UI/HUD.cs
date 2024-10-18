using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { EXP, Level, Kill, Time, HP }
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText   = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.EXP: //슬라이더에 적용할 값: 현재 경험치 / 최대 경험치
                float current_exp = GameManager.Instance.exp;
                float max_exp = GameManager.Instance.nextLevEXP[Mathf.Min(GameManager.Instance.level, GameManager.Instance.nextLevEXP.Length-1)];
                mySlider.value = current_exp / max_exp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);                
                // Format: 각 숫자 인자값을 지정된 형태의 문자열로 만들어주는 메서드
                // Format({그 자리에 오는 인덱스의 순번:그 숫자에 대한 형태}, a)
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break;
            case InfoType.Time:
                int min = Mathf.FloorToInt(Time.time / 60);
                int sec = Mathf.FloorToInt(Time.time % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                // D0, D1, D2...: 자리수를 지정
                break;
            case InfoType.HP:
                float currentHP = GameManager.Instance.currentHP;
                float maxHP = GameManager.Instance.MAXHP;
                mySlider.value = currentHP / maxHP;
                break;
        }
    }

}
