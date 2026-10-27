using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Playing, Paused, Won, Lost }
    public State current {  get; private set; } = State.Playing;
    public event System.Action<State> StateChanged;

    public void SetState(State next)
    {
        if (next == current) return;
        current = next;
        StateChanged?.Invoke(next);
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused) SetState(State.Paused);
    }
}