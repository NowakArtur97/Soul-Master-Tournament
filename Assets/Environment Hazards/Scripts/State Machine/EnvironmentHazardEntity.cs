using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHazardEntity : MonoBehaviour
{
    public FiniteStateMachine StateMachine { get; internal set; }

    public IdleState IdleState { get; private set; }
    //public ActiveState ActiveState { get; private set; }

    private void Start() => StateMachine = new FiniteStateMachine(IdleState);

    private void Update() => StateMachine.CurrentState.LogicUpdate();
}
