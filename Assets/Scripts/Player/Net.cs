using UnityEngine;
using UnityEngine.InputSystem;

public class Net : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionAsset playerControls;

    [SerializeField] private InputAction swingAction;

    [SerializeField] private Animator anim;


    void Start()
    {
        swingAction = playerControls.FindActionMap("Net").FindAction("Swing");
        anim = this.gameObject.transform.parent.GetComponent<Animator>();
        swingAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (swingAction.triggered)
        {
            Debug.Log("Swing");
            anim.SetTrigger("Swing");
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
        if (other.tag == "Bug") other.gameObject.SetActive(false);
    }

}
