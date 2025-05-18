using UnityEngine;
using UnityEngine.EventSystems;

public class BuilderAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;
    public bool pressed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isHovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isHovered", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (!pressed)
        {
            animator.SetBool("isClicked", true);
            pressed = true;
        }
        else
        {
            animator.SetBool("isClicked", false);
            pressed = false;
        }
    }
}
