using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public static bool interactPressed = false;

    private PlayerInput playerInput;
    public static InputAction interactAction;

    [Header("Interaction Settings")]
    [SerializeField] private float checkRadius = 0.3f;
    [SerializeField] private LayerMask interactableLayer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        interactAction.started += OnInteractStarted;
        interactAction.performed += DoInteract;
        interactAction.canceled += DoInteractCanceled;
    }

    private void OnDisable()
    {
        interactAction.started -= OnInteractStarted;
        interactAction.performed -= DoInteract;
        interactAction.canceled -= DoInteractCanceled;
    }

    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        if (CheckForInteractable())
        {
            interactPressed = true;
            Debug.Log("Interact started on Interactable object");
        }
    }
    private void DoInteract(InputAction.CallbackContext context)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius, interactableLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Interactable"))
            {
                var interactable = hit.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    interactable.Interact(); // THIS triggers the UnityEvent
                    Debug.Log("Interacted with: " + hit.name);
                    return; // stop after first hit
                }
            }
        }
    }

    private void DoInteractCanceled(InputAction.CallbackContext context)
    {
        interactPressed = false;
        Debug.Log("Interact released");
    }

    private bool CheckForInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius, interactableLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Interactable"))
            {
                Debug.Log("Standing on interactable: " + hit.name);
                return true; 
            }
        }

        Debug.Log("No interactable under player");
        return false; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
