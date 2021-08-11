using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangeManager : MonoBehaviour
{
    public static TextChangeManager instance;

    void Start()
    {
        instance = this;
    }

    public void ChangeTextFunc(GameObject obj, string _text)
    {
        obj.GetComponent<Text>().text = _text;
    }
}
