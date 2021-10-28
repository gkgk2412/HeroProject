using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss_hp : MonoBehaviour
{
    [SerializeField]
    private BossController _bossScr;

    [SerializeField]
    private Image _HPBar;
    
    [SerializeField]
    private Text _HpText;

    private float speed = 5.0f;

    private void Update()
    {
        string message = _bossScr.b_CurrentHp.ToString() + "%";
        _HpText.text = message;

        _HPBar.fillAmount = (float)_bossScr.b_CurrentHp / (float)_bossScr.b_hp;
        HandleStamina();
    }

    public void HandleStamina()
    {
        _HPBar.fillAmount = Mathf.Lerp(_HPBar.fillAmount, (float)_bossScr.b_CurrentHp / (float)_bossScr.b_hp, Time.deltaTime * speed);
    }
}
