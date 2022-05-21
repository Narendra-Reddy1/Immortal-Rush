using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "UI Events Holder", menuName = "ScriptableObjects/Events/UI Events")]
    public class UIEventsSO : BaseScriptableObject
    {
        [Space]
        public UnityEvent<object> OnTextUpdated;
        public UnityEvent<object> OnPointerDown;
        public UnityEvent<object> OnPointerUp;
    }
}