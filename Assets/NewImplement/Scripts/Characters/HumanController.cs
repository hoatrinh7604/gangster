using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour, IHealth
{
    [SerializeField] int hp = 1;
    [SerializeField] Transform root;
    bool isDeath = false;
    [SerializeField] Animator animator;
    [SerializeField] Collider mainCollisder;
    Component[] Rigidbodys;
    Component[] Colliders;
    private bool canGetDamage = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        mainCollisder = GetComponent<Collider>();
        Rigidbodys = root.GetComponentsInChildren(typeof(Rigidbody));
        Colliders = root.GetComponentsInChildren(typeof(Collider));
        DisableRagdoll(false);
    }

    public void OnDamage(int damage, Vector3 force)
    {
        if (!canGetDamage) return;
        if (isDeath) return;
        canGetDamage = false;
        hp -= damage;
        animator.Play("KB_Hit_m_MidFront_Med");
        Invoke("ReloadState", 1);
        if(hp <= 0)
        {
            Death(force);
        }
    }

    private void ReloadState()
    {
        canGetDamage = true;
    }

    void DisableRagdoll(bool active)
    {

        foreach (Rigidbody rigidbody in Rigidbodys)
            rigidbody.isKinematic = !active;


        foreach (Collider collider in Colliders)
            collider.enabled = active;

    }

    public void AddForce(Vector3 force)
    {
        if (force == null) return;
        foreach (Rigidbody rigidbody in Rigidbodys)
            rigidbody.AddForce(500* force);
    }

    public bool IsDeath()
    {
        return isDeath;
    }

    public void Death(Vector3 force)
    {
        mainCollisder.enabled = false;
        animator.enabled = false;
        DisableRagdoll(true);
        AddForce(force);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AICharacterControl>().enabled = false;
        GetComponent<ThirdPersonCharacter>().enabled = false;
        isDeath = true;

        Destroy(this.gameObject, 5f);
    }
}
