using UnityEngine;

public class EntityInteractor : MonoBehaviour
{
    public virtual void InteractWithEntity(Entity entity)
    {
        Debug.Log($"Interacted with entity: {entity.name}");
    }
}