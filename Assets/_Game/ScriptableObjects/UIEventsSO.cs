using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UI Events Holder", menuName = "ScriptableObjects/Events")]
public class UIEventsSO : ScriptableObject
{
    public  UnityEvent<object> OnTextUpdated;
    public  UnityEvent<object> OnPointerDown;
    public  UnityEvent<object> OnPointerUp;
}
