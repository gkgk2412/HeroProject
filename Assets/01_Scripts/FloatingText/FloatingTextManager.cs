using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;

    [SerializeField] GameObject go_Prefab_FloatingText;
    [SerializeField] GameObject go_Prefab_FloatingDamageText;
    [SerializeField] GameObject go_Prefab_FloatingGoldText;
    [SerializeField] GameObject go_Prefab_FloatingPlayerDamageText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void CreateFloatingText(Vector3 pos, string _text)
    {
        GameObject clone = Instantiate(go_Prefab_FloatingText, pos, go_Prefab_FloatingText.transform.rotation);
        clone.GetComponentInChildren<Text>().text = _text;
    }

    public void CreateFloatingDamageText(Vector3 pos, string _text)
    {
        GameObject clone = Instantiate(go_Prefab_FloatingDamageText, pos, go_Prefab_FloatingDamageText.transform.rotation);
        clone.GetComponentInChildren<Text>().text = _text;
    }

    public void CreateFloatingGoldText(Vector3 pos, string _text)
    {
        GameObject clone = Instantiate(go_Prefab_FloatingGoldText, pos, go_Prefab_FloatingGoldText.transform.rotation);
        clone.GetComponentInChildren<Text>().text = _text;
    }

    public void CreateFloatingPlayerDamageText(Vector3 pos, string _text)
    {
        GameObject clone = Instantiate(go_Prefab_FloatingPlayerDamageText, pos, go_Prefab_FloatingPlayerDamageText.transform.rotation);
        clone.GetComponentInChildren<Text>().text = _text;
    }
}
