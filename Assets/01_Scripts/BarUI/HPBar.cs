using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Image _HPBar;

    private float maxHP = 100;

    private float speed = 5.0f;

    private void Start()
    {
        _HPBar.fillAmount = (float)PlayerControlManager.Instance.curHealth / (float)maxHP;
    }

    private void Update()
    {
        HandleStamina();
    }

    public void HandleStamina()
    {
        _HPBar.fillAmount = Mathf.Lerp(_HPBar.fillAmount, (float)PlayerControlManager.Instance.curHealth / (float)maxHP, Time.deltaTime * speed);
    }
}
