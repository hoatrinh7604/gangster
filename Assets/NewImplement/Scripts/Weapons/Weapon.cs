using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ID;
    public Animator animator;
    [SerializeField] Vector3 localPosition;
    [SerializeField] Vector3 localRotation;
    [SerializeField] string attackAnimName;
    [SerializeField] string equipAnimName;
    [SerializeField] string unEquipAnimName;
    [SerializeField] float delayShowWeaponEquip;
    [SerializeField] float delayShowWeaponUnEquip;
    [SerializeField] GameObject mesh;
    public float delayAttack = 1;
    [SerializeField] GameObject originObject;
    public float forceScale = 3;

    private string currentAnim = "Unknow";
    private string baseAnim = "Unknow";
    private float timer = 0;
    private Rigidbody rigidBody;
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        SetInfo();
    }

    virtual public void Equip()
    {
        EquipAnim();
    }

    virtual public void UnEquip()
    {
        UnEquipAnim();
    }

    virtual public void UnEquipNow()
    {
        
    }


    virtual public void Attack()
    {
        if (attackAnimName.Equals("")) return;
        if (timer > 0) return;
        if (attackAnimName.Equals(currentAnim)) return;
        timer = 1;
        PlayAttackAnim();
       // originObject.SetActive(false);
        Vector3 force = new Vector3(transform.forward.x, 0, transform.forward.z);
        rigidBody.AddRelativeForce(force * forceScale, ForceMode.Impulse);
        Invoke("ResetAttack", delayAttack);
    }

    virtual public void UnAttack()
    {
        currentAnim = baseAnim;
    }
    
    virtual public void OnAttack(Rigidbody rigidBody)
    {
        this.rigidBody = rigidBody;
    }

    public void PlayAttackAnim()
    {
        animator.Play(attackAnimName);
        currentAnim = attackAnimName;
    }

    public void ResetAttack()
    {
        currentAnim = baseAnim;
        timer = 0;
    }

    public void EquipAnim()
    {
        if (equipAnimName.Equals("")) return;
        animator.Play(equipAnimName);
        Invoke("HandleShowMeshEquip", delayShowWeaponEquip);
    }
    
    public void UnEquipAnim()
    {
        if (unEquipAnimName.Equals("")) return;
        animator.Play(unEquipAnimName);
        Invoke("HandleShowMeshUnEquip", delayShowWeaponUnEquip);
    }

    public void HandleShowMeshEquip()
    {
        mesh.SetActive(true);
        originObject.SetActive(false);
    }

    public void HandleShowMeshUnEquip()
    {
        mesh.SetActive(false);
        originObject.SetActive(true);
        gameObject.SetActive(false);
    }

    virtual public void SetInfo()
    {

    }
}
