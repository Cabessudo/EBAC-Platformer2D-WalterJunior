using System.Collections;
using System.Collections.Generic;

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionary;
    private StateBase _currState;
    public StateBase state
    {
        get{ return _currState; }
    }

    public void InitState()
    {
        dictionary = new Dictionary<T, StateBase>();
    }

    public void SwitchState(T stateType, object obj = null)
    {
        _currState?.OnExitState(obj);

        _currState = dictionary[stateType];

        _currState.OnEnterState(obj);
    }
}
