using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    [SerializeField]
    private Image _staminaBar;

    private float maxStamina = 100;

    private float speed = 5.0f;

    private void Start()
    {
        _staminaBar.fillAmount = (float)PlayerControlManager.Instance.curStamina / (float)maxStamina;
    }

    private void Update()
    {
        HandleStamina();
    }

    public void HandleStamina()
    {
        _staminaBar.fillAmount= Mathf.Lerp(_staminaBar.fillAmount, (float)PlayerControlManager.Instance.curStamina / (float)maxStamina, Time.deltaTime * speed);
    }
}
