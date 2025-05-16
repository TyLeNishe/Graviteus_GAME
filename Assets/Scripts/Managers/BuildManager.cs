using UnityEngine;
using UnityEngine.EventSystems;
public class BuildManager : MonoBehaviour
{
    public bool isActive = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Нажат UI-элемент: " + gameObject.name);
    }
}
