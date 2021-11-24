using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public Text arrowText = null;
    public Text arrowSettingText = null;
    private int arrowSettingGold = 3;

    public AudioSource audiosource;
    public AudioClip audioClip;
    public AudioClip audioClip02;

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
        if (PlayerControlManager.Instance.MyGold < hpSettingGold)
        {
            audiosource.PlayOneShot(audioClip02, 1.0f);
            string message = "골드가 부족합니다!";

            FloatingTextManager.instance.CreateFloatingGoldText(this.transform.position, message);
        }

        if (fakeHealth < 100 && PlayerControlManager.Instance.MyGold >= hpSettingGold)
        {
            audiosource.PlayOneShot(audioClip, 1.0f);

            _scrHP.MyMaxHP = _scrHP.MyMaxHP + 10;
            PlayerControlManager.Instance.MyCurHP = PlayerControlManager.Instance.MyCurHP + 10;
            fakeHealth = fakeHealth + 10;

            PlayerControlManager.Instance.MyGold = PlayerControlManager.Instance.MyGold - hpSettingGold;
            hpSettingGold = hpSettingGold + 2;

            string message = hpSettingGold.ToString() + " GOLD";
            string message2 = _scrHP.MyMaxHP.ToString();

            hpSettingText.GetComponent<Text>().text = message;
            hpText.GetComponent<Text>().text = message2;
        }
    }

    public void DamageUpgrade()
    {
        if (PlayerControlManager.Instance.MyGold < damageSettingGold)
        {
            audiosource.PlayOneShot(audioClip02, 1.0f);
            string message = "골드가 부족합니다!";

            FloatingTextManager.instance.CreateFloatingGoldText(this.transform.position, message);
        }
        
        if (fakeDamage < 100 && PlayerControlManager.Instance.MyGold >= damageSettingGold)
        {
            audiosource.PlayOneShot(audioClip, 1.0f);

            Arrow.Instance._damage = Arrow.Instance._damage + 10;
            fakeDamage = fakeDamage + 10;

            PlayerControlManager.Instance.MyGold = PlayerControlManager.Instance.MyGold - damageSettingGold;
            damageSettingGold = damageSettingGold + 2;

            string message = damageSettingGold.ToString() + " GOLD";

            damageSettingText.GetComponent<Text>().text = message;
        }
    }

    public void ArrowUpgrade()
    {
        if (PlayerControlManager.Instance.MyGold < arrowSettingGold)
        {
            audiosource.PlayOneShot(audioClip02, 1.0f);
            string message = "골드가 부족합니다!";

            FloatingTextManager.instance.CreateFloatingGoldText(this.transform.position, message);
        }

        if (PlayerControlManager.Instance.MyArrow < 5 && PlayerControlManager.Instance.MyGold >= arrowSettingGold)
        {
            audiosource.PlayOneShot(audioClip, 1.0f);

            PlayerControlManager.Instance.MyArrow = PlayerControlManager.Instance.MyArrow + 1;

            PlayerControlManager.Instance.MyGold = PlayerControlManager.Instance.MyGold - arrowSettingGold;
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
