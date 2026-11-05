using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    [Header("UI")]
    public TextMeshProUGUI interactionText;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (MinigameManager.Instance != null && MinigameManager.Instance.isMinigameActive)
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
            return;
        }


        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = interactable.GetInteractionMessage();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
                return;
            }
        }

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
