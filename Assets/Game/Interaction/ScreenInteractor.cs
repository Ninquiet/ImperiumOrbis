using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenInteractor : EntityInteractor
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxRayDistance = 100f;
    [SerializeField] private LayerMask _interactionLayerMask = ~0;
    [SerializeField] private InputActionReference _interactInput;

    private void OnEnable()
    {
        if (_interactInput != null)
        {
            _interactInput.action.Enable();
            _interactInput.action.performed += OnInteractPerformed;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton())
            return;
        
        if (UIUtility.IsPointingUI())
            return;

        Entity entity = GetInteractableEntity();
        if (entity != null)
        {
            InteractWithEntity(entity);
        }
    }

    public Entity GetInteractableEntity()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit[] hits = Physics.RaycastAll(ray, _maxRayDistance, _interactionLayerMask);
        
        foreach (RaycastHit hit in hits)
        {
            Entity entity = hit.collider.GetComponent<Entity>();
            if (entity != null)
                return entity;
        }
        
        return null;
    }

    private void OnDisable()
    {
        if (_interactInput != null)
        {
            _interactInput.action.performed -= OnInteractPerformed;
            _interactInput.action.Disable();
        }
    }
}