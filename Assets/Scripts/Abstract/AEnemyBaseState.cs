using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
}

