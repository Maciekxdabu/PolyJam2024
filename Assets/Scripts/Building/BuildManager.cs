using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject planeGO = null;
    [SerializeField] private BuildPreview previewGO = null;
    [SerializeField] private GameObject towerToPlace = null;

    private bool buildingEnabled = true;

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
        inputs.Building.PlaceBuilding.started += PlaceCurrentTower;
    }

    private void OnEnable()
    {
        inputs.Building.Enable();
    }

    private void OnDisable()
    {
        inputs.Building.Disable();
    }

    void FixedUpdate()
    {
        Ray ray = playerCamera.ScreenPointToRay(mousePos);
        float enter = 0.0f;
        if (plane.Raycast(ray, out enter))
        {
            previewGO.ChangePosition(ray.GetPoint(enter));
        }
    }

    // ---------- public bool

    public void EnableBuilding()
    {
        OnEnable();
        previewGO.gameObject.SetActive(true);

        buildingEnabled = true;
    }

    public void DisableBuilding()
    {
        buildingEnabled = false;

        OnDisable();
        previewGO.gameObject.SetActive(false);
    }

    public void ChangeToNewBuilding(GameObject newBuilding)
    {
        towerToPlace = newBuilding;
        Tower tower = towerToPlace.GetComponent<Tower>();
        previewGO.UpdateVisuals(tower.GetMesh(), tower.GetMaterialsAmount());
    }

    // ---------- private methods

    private void PlaceCurrentTower(InputAction.CallbackContext ctx)
    {
        if (buildingEnabled && ctx.started && previewGO.CanPlace())
        {
            Instantiate(towerToPlace, previewGO.transform.position, Quaternion.identity);
        }
    }
}
