using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Image _HPBar;
    [SerializeField]
    private float maxHP = 100;

    private float speed = 5.0f;


    public float MyMaxHP
    {
        get
        {
            return maxHP;
        }

        set
        {
            maxHP = value;
        }
    }

    private void Update()
    {
        _HPBar.fillAmount = (float)PlayerControlManager.Instance.MyCurHP / (float)maxHP;
        HandleStamina();
    }

    public void HandleStamina()
    {
        _HPBar.fillAmount = Mathf.Lerp(_HPBar.fillAmount, (float)PlayerControlManager.Instance.MyCurHP / (float)maxHP, Time.deltaTime * speed);
    }
}
