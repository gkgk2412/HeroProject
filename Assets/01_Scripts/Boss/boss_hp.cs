using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss_hp : MonoBehaviour
{
    private static boss_hp instance;
    public static boss_hp Instance => instance;

    public boss_hp()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    [SerializeField]
    private BossController _bossScr;

    public Image _HPBar;
    
    [SerializeField]
    private Text _HpText;

    private float speed = 5.0f;

    private void Update()
    {
        if (_bossScr.b_CurrentHp <= 0)
            _bossScr.b_CurrentHp = 0;

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
