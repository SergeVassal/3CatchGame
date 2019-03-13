using UnityEngine;


public class HatMovementController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float hatMovementSpeed;

    private Renderer hatRenderer;
    private Rigidbody2D rBody;
    private float maxHatMovementWidth;    
    private bool isHatContollable;


    void Start()
    {
        InitializeVariables();        
    }

    private void InitializeVariables()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        hatRenderer = GetComponent<Renderer>();
        rBody = GetComponent<Rigidbody2D>();
        GetMaxMovementWidth();        
        isHatContollable = false;
    }

    private void GetMaxMovementWidth()
    {
        Vector3 screenDimen = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 screenToWorld = cam.ScreenToWorldPoint(screenDimen);
        float hatHalfWidth = hatRenderer.bounds.extents.x;
        maxHatMovementWidth = screenToWorld.x - hatHalfWidth;
    }

    void FixedUpdate()
    {
        MoveHat();        
    }

    private void MoveHat()
    {
        if (!isHatContollable)
        {
            CalculateAndApplyNewHatPosition();       
        }
    }

    private void CalculateAndApplyNewHatPosition()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalMovementDelta = horizontalInput * hatMovementSpeed;

        float nextHorizontalPositionRaw = rBody.position.x + (horizontalMovementDelta * Time.fixedDeltaTime);
        float nextHorizontalPositionClamped = Mathf.Clamp(nextHorizontalPositionRaw, -maxHatMovementWidth, maxHatMovementWidth);

        rBody.MovePosition(new Vector2(nextHorizontalPositionClamped, rBody.position.y));
    }

    public void toggleHatMovementController(bool canControlHat)
    {
        isHatContollable = canControlHat;
    }    
}
