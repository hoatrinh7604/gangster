using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunWeapon : Weapon
{
    [SerializeField] MultiAimConstraint body;
    [SerializeField] MultiAimConstraint hand;
    [SerializeField] float bodyWeight;
    [SerializeField] float handWeight;

    [SerializeField] Transform shootPoint;
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float delayFiring = 0.5f;
    [SerializeField] float delayFirstFiring = 0.5f;
    [SerializeField] GameObject targetPoint;
    [SerializeField] RaycastWeapon raycastWeapon;
    private float time = 1000;
    private bool isEnterAttack = false;

    private AutoAiming autoAiming;
    private WeaponController weaponController;

    private void Update()
    {
        Shooting();
    }

    override public void Equip()
    {
        EquipAnim();
        
        //GameElements.Instance.cameraController.SwitchState((int)CAMERA.AIMING);
        ControlTarget(true);
    }

    public override void UnEquip()
    {
        UnEquipAnim();
        ControlTarget(false);
        body.weight = 0;
        hand.weight = 0;

        UnAttack();
    }

    public override void UnEquipNow()
    {
        HandleShowMeshUnEquip();
        ControlTarget(false);
        body.weight = 0;
        hand.weight = 0;

        UnAttack();
    }

    public override void Attack()
    {
        base.Attack();
        Firing();
        print("Attack");
    }

    override public void UnAttack()
    {
        raycastWeapon.StopFiring();
        animator.SetBool("Attack", false);
        PlayerPrefs.SetInt("OnAttack", 0);
        body.weight = 0;
        hand.weight = 0;
        isEnterAttack = false;
        time = 1000;
        //autoAiming.SetPlayeLookAt(false);
        //PlayerCamera.instance.camTask = CamTask.Rotate;
        // MONSTER
        GameElements.Instance.cameraController.SwitchState((int)CAMERA.MAIN);
    }
    
    override public void OnAttack(Rigidbody rigidBody)
    {
        base.OnAttack(rigidBody);

        animator.SetBool("Attack", true);
        PlayAttackAnim();
        body.weight = bodyWeight;
        hand.weight = handWeight;
        time = delayFirstFiring;
        isEnterAttack = true;

        
        //PlayerCamera.instance.camTask = CamTask.Shoot;
        //autoAiming.SetPlayeLookAt(true);
        //autoAiming.SwitchCamera();
        //PlayerCamera.instance.PlayerLookTarget(target.position);
        print("OnAttack");
        // MONSTER
        GameElements.Instance.cameraController.SwitchState((int)CAMERA.AIMING);
    }

    public override void SetInfo()
    {
        ID = (int)WEAPON.GUN;
        weaponController = FindObjectOfType<WeaponController>();
        autoAiming = weaponController.GetAutoAiming();
    }

    public void Firing()
    {
        //Vector3 direction = (target.position - shootPoint.position).normalized;
        //var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));
        //bullet.GetComponent<GunBulletController>().Setup(direction, target);
        raycastWeapon.StartFiring();
        Reload();
    }

    public void Shooting()
    {
        if (!isEnterAttack)
        {
            if (PlayerPrefs.GetInt("OnAttack") == 1)
            {
                OnAttack(weaponController.rigidBody);
            }
            return;
        }
        else if (PlayerPrefs.GetInt("OnAttack") == 0)
        {
            UnAttack();
            return;
        }

        time -= Time.deltaTime;
        if (time > 0) return;

        Attack();
    }

    public void Reload()
    {
        time = delayAttack;
    }

    public void ControlTarget(bool isEnable)
    {
        targetPoint.GetComponent<AutoAiming>().enabled = isEnable;
    }

}
