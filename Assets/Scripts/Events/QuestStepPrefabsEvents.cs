using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class QuestStepPrefabsEvents
{
    public event Action onPlayerDetected;
    public event Action<string> onFindSomeOne;

    public void DetectPlayer() => onPlayerDetected?.Invoke();
    public void FindSomeOne(string name) => onFindSomeOne?.Invoke(name);   
}

