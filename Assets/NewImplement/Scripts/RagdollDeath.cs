using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollDeath : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Animator animator = null;

    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;

    private void Awake()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetTrigger(true);
        SetStart();
        //SetDrag();
    }

    public void Death(Collision collision)
    {
        ToggleRagdoll(true);
        var force = collision.contacts[0].normal;
        foreach (var rb in ragdollBodies)
        {
            rb.AddExplosionForce(5f, 10*force, 2, 5, ForceMode.VelocityChange);
        }
    }

    private void ToggleRagdoll(bool state)
    {
        

        foreach(var rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = state;
            SetTrigger(!state);
        }

        animator.enabled = !state;
    }   
    
    private void SetTrigger(bool state)
    {
        for(int i = 1; i<ragdollColliders.Length; i++)
        {
            ragdollColliders[i].isTrigger = state;
        }
    }

    public void SetStart()
    {
        for (int i = 1; i < ragdollBodies.Length; i++)
        {
            ragdollBodies[i].isKinematic = true;
        }
    }

    private void SetDrag()
    {
        foreach (var rb in ragdollBodies)
        {
            rb.angularDrag = 0;
            rb.drag = 0;
        }
    }
}
