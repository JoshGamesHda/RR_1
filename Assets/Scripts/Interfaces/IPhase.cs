using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhase
{
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
