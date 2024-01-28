using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Merloj Utilities/Trigger Events")]
[RequireComponent(typeof(Collider))]
public class TriggerEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnterEvents;
    [SerializeField] private UnityEvent onStayEvents;
    [SerializeField] private UnityEvent onExitEvents;

    // ---------- Unity messages

    private void OnTriggerEnter(Collider other)
    {
        onEnterEvents.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        onStayEvents.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onExitEvents.Invoke();
    }
}
