using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    [Header("Mission Config")]
    [SerializeField] Sprite missionAvatar;
    [SerializeField] Color IconColor = new Color(1, 1, 1, 0.9f);
    [SerializeField] ItemEffect m_Effect = ItemEffect.None;
    [SerializeField] string InfoItem = "Info Icon here";

    [Header("Target")]
    public GameObject GraphicPrefab = null;
    public Transform Target = null;

    [Header("Settings")]
    public bool OffScreen = true;
    public float BorderOffScreen = 0.01f;
    public float OffScreenSize = 10;
    public float Size = 20;
    public Vector3 OffSet = Vector3.zero;
    public Sprite DeathIcon;

    private Image Graphic = null;
    private RectTransform RectRoot;
    private GameObject cacheItem = null;
    private bool isShownMissionIcon = false;
    private void Start()
    {
        //CreateWaypointIcon();
    }

    private void Update()
    {
        ShowIconOnMinimap();
    }

    public void CreateWaypointIcon()
    {
        if (bl_MiniMap.MapUIRoot != null)
        {
            CreateIcon();
        }
        else { Debug.Log("You need a MiniMap in scene for use MiniMap Items."); }
    }

    void CreateIcon()
    {
        cacheItem = Instantiate(GraphicPrefab) as GameObject;
        RectRoot = bl_MiniMap.MapUIRoot;
        Graphic = cacheItem.GetComponent<Image>();
        if (missionAvatar != null) { Graphic.sprite = missionAvatar; Graphic.color = IconColor; }
        cacheItem.transform.SetParent(RectRoot.transform, false);
        Graphic.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (Target == null) { Target = this.GetComponent<Transform>(); }
        StartEffect();
        bl_IconItem ii = cacheItem.GetComponent<bl_IconItem>();
        ii.GetInfoItem(InfoItem);
    }

    void StartEffect()
    {
        Animation a = Graphic.GetComponent<Animation>();
        if (m_Effect == ItemEffect.Pulsing)
        {
            a.Play("Pulsing");
        }
        else if (m_Effect == ItemEffect.Fade)
        {
            a.Play("Fade");
        }
    }

    public void ShowIconOnMinimap()
    {
        if (!isShownMissionIcon) return;
        //If a component missing, return for avoid bugs.
        if (Target == null)
            return;
        if (Graphic == null)
            return;
        //Get the Rect of Target UI
        RectTransform rt = Graphic.GetComponent<RectTransform>();
        //Setting the modify position
        Vector3 CorrectPosition = TargetPosition + OffSet;
        //Convert the position of target in ViewPortPoint
        Vector2 vp2 = bl_MiniMap.MiniMapCamera.WorldToViewportPoint(CorrectPosition);
        //Calculate the position of target and convert into position of screen
        Vector2 position = new Vector2((vp2.x * RectRoot.sizeDelta.x) - (RectRoot.sizeDelta.x * 0.5f),
            (vp2.y * RectRoot.sizeDelta.y) - (RectRoot.sizeDelta.y * 0.5f));
        //if show off screen
        if (OffScreen)
        {
            //Calculate the max and min distance to move the UI
            //this clamp in the RectRoot sizeDela for border
            position.x = Mathf.Clamp(position.x, -((RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen), ((RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen));
            position.y = Mathf.Clamp(position.y, -((RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen), ((RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen));
        }

        //calculate the position of UI again, determine if offscreen
        //if offscreen reduce the size
        float size = Size;
        //Use this (useCompassRotation when have a circle miniMap)
        if (m_miniMap.useCompassRotation)
        {
            //Compass Rotation
            Vector3 screenPos = Vector3.zero;
            //Calculate diference
            Vector3 forward = Target.position - m_miniMap.TargetPosition;
            //Position of target from camera
            Vector3 cameraRelativeDir = bl_MiniMap.MiniMapCamera.transform.InverseTransformDirection(forward);
            //normalize values for screen fix
            cameraRelativeDir.z = 0;
            cameraRelativeDir = cameraRelativeDir.normalized / 2;
            //Convert values to positive for calculate area OnScreen and OffScreen.
            float posPositiveX = Mathf.Abs(position.x);
            float relativePositiveX = Mathf.Abs((0.5f + (cameraRelativeDir.x * m_miniMap.CompassSize)));
            //when target if offScreen clamp position in circle area.
            if (posPositiveX >= relativePositiveX)
            {
                screenPos.x = 0.5f + (cameraRelativeDir.x * m_miniMap.CompassSize)/*/ Camera.main.aspect*/;
                screenPos.y = 0.5f + (cameraRelativeDir.y * m_miniMap.CompassSize);
                position = screenPos;
                size = OffScreenSize;
            }
            else
            {
                size = Size;
            }
        }
        else
        {
            if (position.x == (RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen || position.y == (RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen ||
                position.x == -(RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen || -position.y == (RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen)
            {
                size = OffScreenSize;
            }
            else
            {
                size = Size;
            }
        }
        //Apply position to the UI (for follow)
        rt.anchoredPosition = position;
        //Change size with smooth transition
        rt.sizeDelta = Vector2.Lerp(rt.sizeDelta, new Vector2(size, size), Time.deltaTime * 8);
        Quaternion r = Quaternion.identity;
        r.x = Target.rotation.x;


        rt.localRotation = r;
    }

    public void DestroyItem(bool inmediate = false)
    {
        if (Graphic == null)
        {
            Debug.Log("Graphic Item of " + this.name + " not exist in scene");
            return;
        }

        if (DeathIcon == null || inmediate)
        {
            Graphic.GetComponent<bl_IconItem>().DestroyIcon(inmediate);
        }
        else
        {
            Graphic.GetComponent<bl_IconItem>().DestroyIcon(inmediate, DeathIcon);
        }
    }

    public Vector3 TargetPosition
    {
        get
        {
            if (Target == null)
            {
                return Vector3.zero;
            }

            return new Vector3(Target.position.x, 0, Target.position.z);
        }
    }

    private bl_MiniMap _minimap = null;
    private bl_MiniMap m_miniMap
    {
        get
        {
            if (_minimap == null)
            {
                _minimap = this.cacheItem.transform.root.GetComponentInChildren<bl_MiniMap>();
            }
            return _minimap;
        }
    }

    public void HideMissionIcon()
    {
        isShownMissionIcon = false;
        DestroyItem();
    }

    public void SetMission(Transform target, Sprite icon)
    {
        CreateWaypointIcon();
        this.Target = target;
        missionAvatar = icon;
        isShownMissionIcon = true;
    }
}
