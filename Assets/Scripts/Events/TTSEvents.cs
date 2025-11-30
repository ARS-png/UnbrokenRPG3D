using System;
using UnityEngine;

public class TTSEvents
{
    public event Action<string> onSpeak;
    public void Speak(string text) => onSpeak?.Invoke(text);
}
