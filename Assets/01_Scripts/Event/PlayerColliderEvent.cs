using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderEvent : MonoBehaviour
{
    public UnityEvent _tutoEvnet;
    public UnityEvent _bossEvnet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TutoEnterCol")
        {
            tutoColEvent();
        }
        
        if (other.gameObject.name == "BossEventCol")
        {
            BossColEvent();
        }
    }

    public void tutoColEvent()
    {
        _tutoEvnet.Invoke();
    }

    public void BossColEvent()
    {
        _bossEvnet.Invoke();
    }
}
