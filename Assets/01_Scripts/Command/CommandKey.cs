using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandKey : MonoBehaviour
{
    public MonoBehaviour mono;

    public virtual void Execute() { }
}

