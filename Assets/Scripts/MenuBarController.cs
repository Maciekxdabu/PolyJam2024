using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBarController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private BuildManager buildMan = null;

    // ---------- Event handlers

    public void OnPointerEnter(PointerEventData eventData)
    {
        buildMan.DisableBuilding();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buildMan.EnableBuilding();
    }
}
