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
            case InfoType.EXP: //�����̴��� ������ ��: ���� ����ġ / �ִ� ����ġ
                float current_exp = GameManager.Instance.exp;
                float max_exp = GameManager.Instance.nextLevEXP[Mathf.Min(GameManager.Instance.level, GameManager.Instance.nextLevEXP.Length-1)];
                mySlider.value = current_exp / max_exp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);                
                // Format: �� ���� ���ڰ��� ������ ������ ���ڿ��� ������ִ� �޼���
                // Format({�� �ڸ��� ���� �ε����� ����:�� ���ڿ� ���� ����}, a)
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break;
            case InfoType.Time:
                int min = Mathf.FloorToInt(Time.time / 60);
                int sec = Mathf.FloorToInt(Time.time % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                // D0, D1, D2...: �ڸ����� ����
                break;
            case InfoType.HP:
                float currentHP = GameManager.Instance.currentHP;
                float maxHP = GameManager.Instance.MAXHP;
                mySlider.value = currentHP / maxHP;
                break;
        }
    }

}
