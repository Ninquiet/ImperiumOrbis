using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenInteractor : EntityInteractor
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxRayDistance = 100f;
    [SerializeField] private LayerMask _interactionLayerMask = ~0;
    [SerializeField] private InputActionReference _interactInput;
    [SerializeField] private EntityInteractor _parentInteractor;

    private void OnEnable()
    {
        if (_interactInput != null)
            _interactInput.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        if (_interactInput != null)
            _interactInput.action.performed -= OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        Entity entity = GetInteractableEntity();
        if (entity != null && _parentInteractor != null)
        {
            _parentInteractor.InteractWithEntity(entity);
        }
    }

    public Entity GetInteractableEntity()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance, _interactionLayerMask))
        {
            return hit.collider.GetComponent<Entity>();
        }
        
        return null;
    }
}