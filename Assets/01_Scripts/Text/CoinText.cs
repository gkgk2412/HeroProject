using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    void Update()
    {
        string message = "X " + PlayerControlManager.Instance.GetGold().ToString();

        this.GetComponent<Text>().text = message;
    }
}
