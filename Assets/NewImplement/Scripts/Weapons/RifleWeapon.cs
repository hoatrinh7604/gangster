using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RifleWeapon : Weapon
{
    [SerializeField] MultiAimConstraint body;
    [SerializeField] MultiAimConstraint hand;
    [SerializeField] TwoBoneIKConstraint rightHand;
    [SerializeField] TwoBoneIKConstraint leftHand;
    [SerializeField] float bodyWeight;
    [SerializeField] float handWeight;

    [SerializeField] Transform shootPoint;
    [SerializeField] Transform target;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float delayFiring = 0.5f;
    [SerializeField] float delayFirstFiring = 0.5f;
    [SerializeField] float delayTwoBoneEquip = 0.5f;
    [SerializeField] GameObject targetPoint;
    [SerializeField] RaycastWeapon raycastWeapon;
    private float time = 1000;
    private bool isEnterAttack = false;

    private AutoAiming autoAiming;
    private WeaponController weaponController;
    private bool isEquipCompleted = false;

    private void Update()
    {
        Shooting();
    }

    override public void Equip()
    {
        EquipAnim();

        //GameElements.Instance.cameraController.SwitchState((int)CAMERA.AIMING);
        ControlTarget(true);
        Invoke("DelayApplyTwoBoneWeight", delayTwoBoneEquip);
        Invoke("EquipDone", delayTwoBoneEquip);
    }

    public override void UnEquip()
    {
        UnAttackNoneInvoke();
        UnEquipAnim();
        ControlTarget(false);
        body.weight = 0;
        hand.weight = 0;
        leftHand.weight = 0;
        rightHand.weight = 0;
        PlayerPrefs.SetInt("Attack", 0);
        isEquipCompleted = false;
    }

    public override void UnEquipNow()
    {
        HandleShowMeshUnEquip();
        UnAttackNoneInvoke();
        ControlTarget(false);
        body.weight = 0;
        hand.weight = 0;
        leftHand.weight = 0;
        rightHand.weight = 0;
        PlayerPrefs.SetInt("Attack", 0);
        isEquipCompleted = false;
    }

    private void DelayApplyTwoBoneWeight()
    {
        if (isEnterAttack) return;
        leftHand.weight = 1;
        rightHand.weight = 1;
    }

    private void EquipDone()
    {
        isEquipCompleted = true;
    }

    public override void Attack()
    {
        base.Attack();
        Firing();
    }

    override public void UnAttack()
    {
        raycastWeapon.StopFiring();
        UnAttackNoneInvoke();
        Invoke("DelayApplyTwoBoneWeight", 0);
    }

    public void UnAttackNoneInvoke()
    {
        raycastWeapon.StopFiring();
        animator.SetBool("Attack", false);
        PlayerPrefs.SetInt("OnAttack", 0);
        body.weight = 0;
        hand.weight = 0;
        isEnterAttack = false;
        time = 1000;
        GameElements.Instance.cameraController.SwitchState((int)CAMERA.MAIN);
    }

    override public void OnAttack(Rigidbody rigidBody)
    {
        base.OnAttack(rigidBody);
        PlayerPrefs.SetInt("Attack", 1);
        animator.SetBool("Attack", true);
        PlayAttackAnim();
        body.weight = bodyWeight;
        hand.weight = handWeight;
        leftHand.weight = 0;
        rightHand.weight = 0;
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
        ID = (int)WEAPON.RIFLE;
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
        if (!isEquipCompleted) return;
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
