using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;

    public interactiveEvent _kingScr;

    bool isDialogue = false; //대화중일 경우 true
    bool isNext = false;     //특정 키 입력 대기

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;       //대화 카운트
    int contextCount = 0;    //대사 카운트

    private void Update()
    {
        if(isDialogue)
        {
            if(isNext)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    isNext = false;
                    txt_Dialogue.text = "";

                    if(++contextCount < dialogues[lineCount].contexts.Length)
                        StartCoroutine(TypeWriter());

                    else
                    {
                        contextCount = 0;

                        if(++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }

                        else
                        {
                            EndDialogue();
                        }
                    }


                }
            }
        }
    }

    public void showDialogue(Dialogue[] p_dialogue)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogue;

        StartCoroutine(TypeWriter());
    }

    public void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;

        SettingUI(false);
        _kingScr.ExecuteKing();
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");

        //txt_Dialogue.text = t_ReplaceText;
        txt_Name.text = dialogues[lineCount].name;

        for(int i = 0; i < t_ReplaceText.Length; ++i)
        {
            txt_Dialogue.text += t_ReplaceText[i];

            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
        yield return null;
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }
}
