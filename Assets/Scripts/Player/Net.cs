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

    [SerializeField] Vector3 origin;
    [SerializeField] Vector3 hitMark;


    void Start()
    {
        setPosition = gameObject.transform.localPosition;
        swingPosition = new Vector3(0.72f, -0.75f, 0.47f);
        netResetTimer = netResetTimerMax;
        swingAction = playerControls.FindActionMap("Net").FindAction("Swing");
        swingAction.Enable();
        canSwing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (swingAction.triggered)
        {
            if (canSwing)
            {
                gameObject.transform.localPosition = swingPosition;
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localRotation.x, -25f, gameObject.transform.localRotation.z);
                swingingDown = true;
                canSwing = false;
            }
            
        }

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
            Debug.Log(other.gameObject.name);
            transform.parent.parent.GetComponent<Inventory>().CollectBug(other.gameObject.GetComponent<Bug>());
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            // Debug.Log("Hit Terrain");
            swingingDown = false;
            countdown = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
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
