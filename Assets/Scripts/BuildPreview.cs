using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuildPreview : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRend = null;
    [SerializeField] private Material normalMaterial = null;
    [SerializeField] private Material invalidMaterial = null;

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

    private void OnTriggerStay(Collider other)
    {
        UpdateState(false);
    }

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

    // ---------- private methods

    private void UpdateState(bool newState)
    {
        if (canPlace != newState)
        {
            canPlace = newState;
            Material materialToPlace;
            Material[] materials = meshRend.materials;

            if (canPlace)
                materialToPlace = normalMaterial;
            else
                materialToPlace = invalidMaterial;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = materialToPlace;
            }
            meshRend.materials = materials;
        }
    }
}
