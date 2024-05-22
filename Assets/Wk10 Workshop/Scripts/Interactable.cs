using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public enum InteractableType
{
    Default,
    Drill, 
    Hammer,
    Door,
    Vault,
    Money,
    GetawayVan,
}

public class Interactable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private InteractableType interactableType = InteractableType.Default;
    [SerializeField] private Transform interactTransform;
    
    [Header("Debug")]
    [SerializeField] private bool isInteractable = false;
    [SerializeField] private int thiefCount = 0;

    private void Start()
    {
        this.tag = "Interactable";
        
        if (interactableType == InteractableType.Default)
        {
            Debug.LogError("Interactable Type not set on: " + this.gameObject.name);
        }

        if (interactTransform == null)
        {
            Debug.LogError("Interactable Interact Transform not set on: " + this.gameObject.name);
        }
    }
    
    public InteractableType GetInteractType()
    {
        if (isInteractable)
        {
            return interactableType;
        }

        return InteractableType.Default;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            thiefCount++;
            isInteractable = thiefCount > 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            thiefCount--;
            isInteractable = thiefCount > 0;
        }
    }

    public void Interact()
    {
        switch (interactableType)
        {
            case InteractableType.Default:
                Debug.Log("Interacted with: NONE. PROBLEM! MANY SUCH CASES!");
                break;
            case InteractableType.Door:
                Debug.Log("Interacted with: Door.");
                DestroyImmediate(this.gameObject);
                break;
            case InteractableType.Drill:
                Debug.Log("Interacted with: Drill.");
                DestroyImmediate(this.gameObject);
                break;
            case InteractableType.Hammer:
                Debug.Log("Interacted with: Hammer.");
                DestroyImmediate(this.gameObject);
                break;
            case InteractableType.Money:
                Debug.Log("Interacted with: Money.");
                DestroyImmediate(this.gameObject);
                break;
            case InteractableType.Vault:
                Debug.Log("Interacted with: Vault.");
                DestroyImmediate(this.gameObject);
                break;
            case InteractableType.GetawayVan:
                Debug.Log("Interacted with: Getaway Van.");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DisableColliders()
    {
        var childColliders = GetComponentsInChildren<Collider>();
        foreach (var collider in childColliders)
        {
            DestroyImmediate(collider);
        }
    }
}
