using UnityEngine;
using UnityEngine.Events; // 1

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    protected GameEvent gameEvent; // 2
    [SerializeField]
    private UnityEvent response; // 3

    protected void OnEnable() // 4
    {
        gameEvent.RegisterListener(this);
    }

    protected void OnDisable() // 5
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6
    {
        response.Invoke();
    }
}
