using UnityEngine;
using UnityEngine.Events;

// TODO : Ajouter tous vos canaux événementiels ici.
//        Consultez les notes de cours si vous avez oublié comment faire.

public class EventChannels : MonoBehaviour
{
    [SerializeField] private UnityEvent onLevelLose = new();
    [SerializeField] private UnityEvent onLevelWin = new();

    public event UnityAction OnLevelLose
    {
        add => onLevelLose.AddListener(value);
        remove => onLevelLose.RemoveListener(value);
    }

    public void PublishLevelLose()
    {
        onLevelLose.Invoke();
    }
    
    public event UnityAction OnLevelWin
    {
        add => onLevelWin.AddListener(value);
        remove => onLevelWin.RemoveListener(value);
    }

    public void PublishLevelWin()
    {
        onLevelWin.Invoke();
    }
}
