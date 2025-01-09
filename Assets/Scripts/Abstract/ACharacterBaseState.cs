using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacterBaseState
{
    public abstract void EnterState(CharacterStateManager character);
    public abstract void UpdateState(CharacterStateManager character);
}
