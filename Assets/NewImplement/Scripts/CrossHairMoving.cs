using CnControls;
using UnityEngine;

public class CrossHairMoving : MonoBehaviour
{
    public Transform target;
    public Transform targetBase;
    public Transform baseTransform;

    public LayerMask lineOfSightMask = 0;


    public float smoothTime = 0.15f;
    public float smoothRotate = 0.1f;


    public float xSpeed = 150.0f;
    public float ySpeed = 150.0f;

    public float yMinLimit = -40.0f;
    public float yMaxLimit = 60.0f;

    public float cameraDistance = 2.5f;
    public Vector3 targetOffset = Vector3.zero;

    public bool visibleMouseCursor = true;

    public float minLookingX, maxLookingX;
    public float minLookingY, maxLookingY;


    [HideInInspector]
    public float x, y, z = 0.0f;

    [HideInInspector]
    public float xSmooth, ySmooth, zSmooth = 0.0f;

    [SerializeField] Rigidbody controller;
    [SerializeField] bool isEnable = false;

    private RectTransform rect;
    private void Awake()
    {
        controller = target.GetComponent<Rigidbody>();
        rect = GetComponent<RectTransform>();
        if (visibleMouseCursor) { Cursor.visible = true; } else { Cursor.visible = false; }
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        PlayerPrefs.SetInt("OnAttack", 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isEnable) return;
        //transform.position = new Vector3(baseTransform.position.x, baseTransform.position.y, baseTransform.position.z);
        Control();
    }
    float tempX, tempY;
    public void Control()
    {
        if (GameControl.manager.controlMode == ControlMode.simple)
        {
            tempX = CnInputManager.GetAxis("Mouse X") * xSpeed * 0.02f;
            tempY = CnInputManager.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
        else if (GameControl.manager.controlMode == ControlMode.touch)
        {
            tempX = CnInputManager.GetAxis("Camera X") * xSpeed * 0.02f;
            tempY = CnInputManager.GetAxis("Camera Y") * ySpeed * 0.02f;

        }

        x = tempX * xSpeed * 0.02f;
        y = tempY * ySpeed * 0.02f;

        //if (GameControl.manager.controlMode == ControlMode.simple)
        //{
        //    tempX = (CnInputManager.GetAxis("Mouse X") > 0) ? 1 : -1;
        //    tempY = (CnInputManager.GetAxis("Mouse Y") > 0) ? 1 : -1;
        //}
        //else if (GameControl.manager.controlMode == ControlMode.touch)
        //{
        //    tempX = (CnInputManager.GetAxis("Camera X") > 0) ? 1 : -1;
        //    tempY = (CnInputManager.GetAxis("Camera Y") > 0) ? 1 : -1;

        //}

        //x = tempX * xSpeed * Time.deltaTime;
        //y = tempY * ySpeed * Time.deltaTime;

        x = Mathf.Clamp(rect.localPosition.x + x, minLookingX, maxLookingX);
        y = Mathf.Clamp(rect.localPosition.y + y, minLookingY, maxLookingY);
        //transform.Rotate(new Vector3(-ySmooth2, xSmooth2, zSmooth));
        rect.localPosition = new Vector3(x, y, rect.localPosition.z);

        //transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, minLookingX, maxLookingX), Mathf.Clamp(transform.localEulerAngles.y, minLookingY, maxLookingY), transform.localEulerAngles.z);
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void EnableAiming(bool isEnable)
    {
        this.isEnable = isEnable;
        if (isEnable) return;
        rect.localPosition = Vector3.zero;
    }

    public void DelayEnable()
    {
        isEnable = true;
    }

    public void DelayDisable()
    {
        isEnable = false;
    }

    public void SetupAiming(bool isEnable)
    {
        this.isEnable = isEnable;
    }

    float CameraMotion(float speed, float angle)
    {
        return Mathf.PingPong(Time.time * speed, angle) - angle / 2.0f;
    }
}
