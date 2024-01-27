using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject planeGO = null;
    [SerializeField] private GameObject previewGO = null;

    private Camera playerCamera = null;
    Plane plane;

    //input
    Inputs inputs;
    Vector2 mousePos = Vector2.zero;

    private void Awake()
    {
        playerCamera = Camera.main;
        plane = new Plane(Vector3.up, planeGO.transform.position.y);

        inputs = new Inputs();
        inputs.Building.MousePos.performed += (ctx) =>
        {
            mousePos = ctx.ReadValue<Vector2>();
        };
    }

    private void OnEnable()
    {
        inputs.Building.Enable();
    }

    private void OnDisable()
    {
        inputs.Building.Disable();
    }

    void Update()
    {
        //get the raycast point
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(mousePos);
        float enter = 0.0f;
        if (plane.Raycast(ray, out enter))
        {
            previewGO.transform.position = ray.GetPoint(enter);
        }
    }
}
