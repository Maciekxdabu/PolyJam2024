using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuildPreview : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRend = null;
    [SerializeField] private MeshFilter meshFilter = null;
    [SerializeField] private MeshCollider meshCol = null;
    [SerializeField] private Material normalMaterial = null;
    [SerializeField] private Material invalidMaterial = null;

    private int currentCost = 1000;

    private bool canPlace = true;
    private List<Collider> collidingWith = new List<Collider>();
    private Rigidbody rb = null;

    // ---------- Unity messages

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        collidingWith.Add(other);
        UpdateState(false);
    }

    private void OnTriggerExit(Collider other)
    {
        collidingWith.Remove(other);
        if (collidingWith.Count == 0)
        {
            UpdateState(true);
        }
    }*/

    /*private void OnTriggerStay(Collider other)
    {
        UpdateState(false);
    }*/

    private void FixedUpdate()
    {
        UpdateState(true);
    }

    // ---------- public methods

    public void ChangePosition(Vector3 newPos)
    {
        rb.position = newPos;
    }

    public bool CanPlace()
    {
        return canPlace;
    }

    public void UpdateVisuals(Mesh mesh, int materialsAmount, int newCost)
    {
        meshFilter.mesh = mesh;
        meshCol.sharedMesh = mesh;

        Material[] materials = Enumerable.Repeat<Material>(normalMaterial, materialsAmount).ToArray();
        meshRend.materials = materials;

        currentCost = newCost;
    }

    public void UpdateState(bool newState)
    {
        if (PlayerData.Instance.CanSpent(currentCost) == false)
        {
            if (canPlace)
            {
                canPlace = false;
                UpdateMaterial(invalidMaterial);
            }
            return;
        }

        if (canPlace != newState)
        {
            canPlace = newState;

            if (canPlace)
                UpdateMaterial(normalMaterial);
            else
                UpdateMaterial(invalidMaterial);
            }
    }

    // ---------- private methods

    private void UpdateMaterial(Material newMaterial)
    {
        Material[] materials = meshRend.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = newMaterial;
        }
        meshRend.materials = materials;
    }
}
