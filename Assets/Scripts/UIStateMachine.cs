using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FOGPISystems.StateMachine;

public class UIStateMachine : SimpleStateMachine
{
    public UIHUDState uIHUDState;
    public UIPauseState uIPauseState;

    // Start is called before the first frame update
    void Start()
    {
        uIHUDState.uIStateMachine = this;
        States.Add(uIHUDState);
        uIPauseState.uIStateMachine = this;
        States.Add(uIPauseState);

        foreach(SimpleState state in States)
        {
            state.StateMachine = this;
        }

        ChangeState(nameof(uIHUDState));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        ChangeState(nameof(uIPauseState));
    }

    public void ResumeButton()
    {
        ChangeState(nameof(uIHUDState));
    }
}
