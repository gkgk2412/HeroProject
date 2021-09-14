using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public Text arrowText = null;
    public Text arrowSettingText = null;
    private int arrowSettingGold = 3;


    public Text hpText = null;
    public Text hpSettingText = null;
    private int hpSettingGold = 3;
    public Image[] healthPoints;
    float fakeHealth = 0;
    public HPBar _scrHP;


    float fakeDamage = 0;
    public Text damageSettingText = null;
    private int damageSettingGold = 3;
    public Image[] damagePoints;

    public void HpUpgrade()
    {
        if (fakeHealth < 100 /*&& PlayerControlManager.Instance.MyGold >= hpSettingGold*/) 
        {
            _scrHP.MyMaxHP = _scrHP.MyMaxHP + 10;
            PlayerControlManager.Instance.MyCurHP = PlayerControlManager.Instance.MyCurHP + 10;
            fakeHealth = fakeHealth + 10;

            hpSettingGold = hpSettingGold + 2;

            string message = hpSettingGold.ToString() + " GOLD";
            string message2 = _scrHP.MyMaxHP.ToString();

            hpSettingText.GetComponent<Text>().text = message;
            hpText.GetComponent<Text>().text = message2;
        }
    }

    public void DamageUpgrade()
    {
        if (fakeDamage < 100 /*&& PlayerControlManager.Instance.MyGold >= hpSettingGold*/)
        {
            Arrow.Instance.damage = Arrow.Instance.damage + 10;
            fakeDamage = fakeDamage + 10;

            damageSettingGold = damageSettingGold + 2;

            string message = damageSettingGold.ToString() + " GOLD";

            damageSettingText.GetComponent<Text>().text = message;
        }
    }

    public void ArrowUpgrade()
    {
        if (PlayerControlManager.Instance.MyArrow < 5 /*&& PlayerControlManager.Instance.MyGold >= arrowSettingGold*/)
        {
            PlayerControlManager.Instance.MyArrow = PlayerControlManager.Instance.MyArrow + 1;
            arrowSettingGold = arrowSettingGold + 2;

            string message = PlayerControlManager.Instance.MyArrow.ToString();
            string message2 = arrowSettingGold.ToString() + " GOLD";

            arrowText.GetComponent<Text>().text = message;
            arrowSettingText.GetComponent<Text>().text = message2;
        }
    }

    private void Update()
    {
        HealthBarFiller();
        DamageBarFiller();
    }

    void HealthBarFiller()
    {
        for(int i = 0; i < healthPoints.Length; ++i)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(fakeHealth, i);
        }
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    void DamageBarFiller()
    {
        for (int i = 0; i < damagePoints.Length; ++i)
        {
            damagePoints[i].enabled = !DisplayDamagePoint(fakeDamage, i);
        }
    }

    bool DisplayDamagePoint(float _damage, int pointNumber)
    {
        return ((pointNumber * 10) >= _damage);
    }
}
