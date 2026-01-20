using UnityEngine.EventSystems;

public static class UIUtility
{
    public static bool IsPointingUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
