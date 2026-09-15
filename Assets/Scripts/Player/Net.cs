using UnityEngine;
using UnityEngine.InputSystem;

public class Net : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionAsset playerControls;

    [SerializeField] private InputAction swingAction;

    [SerializeField] private Vector3 setPosition; // 0.9, -0.75, 0
    [SerializeField] private Vector3 swingPosition; // 0.72, -0.75, 0.47 + rotation -25.0

    [SerializeField] private float netResetTimerMax = 1f;
    [SerializeField] private float netResetTimer;

    [SerializeField] private bool swingingDown;
    [SerializeField] public bool canSwing;
    [SerializeField] private bool resetingSwing;
    [SerializeField] private bool countdown;

    [SerializeField] Transform indicatorRaycastStartPoint;
    [SerializeField] Transform indicatorRaycastMiddlePoint;
    [SerializeField] Transform indicatorRaycastEndPoint;

    [SerializeField] GameObject netRaycastPointsObject;
    private Transform[] netRaycastPoints;

    public GameObject marker;


    void Start()
    {
        setPosition = gameObject.transform.localPosition;
        swingPosition = new Vector3(0.72f, -0.75f, 0.47f);
        netResetTimer = netResetTimerMax;

        netRaycastPoints = new Transform[6];
        for (int i = 0; i < 6; i++)
        {
            netRaycastPoints[i] = this.gameObject.transform.GetChild(0).GetChild(i).transform;
        }

        swingAction = playerControls.FindActionMap("Net").FindAction("Swing");
        swingAction.Enable();
        canSwing = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debuging();

        if (canSwing && Mouse.current.leftButton.isPressed)
        {
            marker.SetActive(true);
            Vector3 direction = indicatorRaycastEndPoint.position - indicatorRaycastMiddlePoint.position;
            if (Physics.Linecast(indicatorRaycastStartPoint.position, indicatorRaycastMiddlePoint.position, out RaycastHit hitInfo))
            {
                //Debug.DrawLine(indicatorRaycastStartPoint.position, hitInfo.point, Color.red);
                // marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                marker.transform.position = hitInfo.point;
            }
            else if (Physics.Raycast(indicatorRaycastMiddlePoint.position, direction, out RaycastHit secondHitInfo, 5f))
            {
                //Debug.DrawLine(indicatorRaycastMiddlePoint.position, hitInfo.point, Color.red);
                // marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                marker.transform.position = secondHitInfo.point;
            }


        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            marker.SetActive(false);
            gameObject.transform.localPosition = swingPosition;
            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localRotation.x, -25f, gameObject.transform.localRotation.z);
            swingingDown = true;
            canSwing = false;
        }
        /*if (swingAction.triggered)
        {
            if (canSwing)
            {
                Vector3 direction = indicatorRaycastEndPoint.position - indicatorRaycastMiddlePoint.position;
                // float distance = 10f; // direction.magnitude;
                
                if (Physics.Linecast(indicatorRaycastStartPoint.position, indicatorRaycastMiddlePoint.position, out RaycastHit hitInfo))
                {
                    //Debug.DrawLine(indicatorRaycastStartPoint.position, hitInfo.point, Color.red);
                    marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    marker.transform.localPosition = hitInfo.point;
                }
                else if (Physics.Raycast(indicatorRaycastMiddlePoint.position, direction, out RaycastHit secondHitInfo, 5f))
                {
                    //Debug.DrawLine(indicatorRaycastMiddlePoint.position, hitInfo.point, Color.red);
                    marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    marker.transform.localPosition = secondHitInfo.point;
                }

                gameObject.transform.localPosition = swingPosition;
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localRotation.x, -25f, gameObject.transform.localRotation.z);
                swingingDown = true;
                canSwing = false;
            }
        }*/

        if (swingingDown)
        {
            SwingDown();
        }

        if (resetingSwing)
        {
            SwingUp();
        }

        if (countdown)
        {
            netResetTimer -= Time.deltaTime;
            if (netResetTimer < 0f)
            {
                resetingSwing = true;
                countdown = false;
            }
        }
    }

    void Debuging()
    {
        Debug.DrawLine(indicatorRaycastStartPoint.position, indicatorRaycastMiddlePoint.position, Color.red);
        Debug.DrawLine(indicatorRaycastMiddlePoint.position, indicatorRaycastEndPoint.position, Color.red);

        foreach (Transform tf in netRaycastPoints)
        {
            Vector3 endPos = tf.position + (tf.transform.forward * 5);
            Debug.DrawLine(tf.position, endPos, Color.red);

        }
    }

    private void OnEnable()
    {
        swingAction.Enable();
    }

    private void OnDisable()
    {
        swingAction.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bug")
        {
            Bug collectedBug = other.gameObject.GetComponent<Bug>();
            // transform.parent.parent.GetComponent<Inventory>().CollectBug(collectedBug);
            collectedBug.transform.parent.GetComponent<SpawnLocation>().SetTimer();
            collectedBug.BeCollected(transform.parent.parent.GetComponent<Inventory>());
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Tree")
        {
            // Debug.Log("Hit Terrain");
            swingingDown = false;
            countdown = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Tree")
        {
            // Debug.Log("Hit Terrain");
            swingingDown = false;
            countdown = true;
        }
    }

    private void SwingDown()
    {
        Vector3 newLocation = new Vector3(5f, 0, 0);

        gameObject.transform.Rotate(newLocation, Space.Self);

        foreach (Transform tf in netRaycastPoints)
        {
            Vector3 endPos = tf.position + (tf.transform.forward * 5);
            Debug.DrawLine(tf.position, endPos, Color.red);
            if (Physics.Linecast(tf.position, endPos, out RaycastHit hit))
            {
                // Debug.Log($"Cast Distance: {hit.distance}");
                if (hit.distance < 0.1f)
                {
                    // Destroy(marker.gameObject);
                    swingingDown = false;
                    countdown = true;
                }
            }
        }
    }

    private void SwingUp()
    {
        Vector3 newLocation = new Vector3(-5f, 0, 0);

        gameObject.transform.Rotate(newLocation, Space.Self);

        if (gameObject.transform.localRotation.x < 0)
        {
            resetingSwing = false;
            gameObject.transform.localPosition = setPosition;
            netResetTimer = netResetTimerMax;
            canSwing = true;
        }
        // Debug.Log(gameObject.transform.localRotation.x);
    }
}
