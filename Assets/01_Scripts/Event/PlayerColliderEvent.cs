using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderEvent : MonoBehaviour
{
    public UnityEvent _tutoEvnet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TutoEnterCol")
        {
            tutoColEvent();
        }
    }

    public void tutoColEvent()
    {
        _tutoEvnet.Invoke();
    }
}
