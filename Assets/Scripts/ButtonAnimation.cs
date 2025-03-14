using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject obj1;
    public GameObject obj2;
    private Animator animator;
    private static ButtonAnimation currentClickedButton;

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
        Animator obj1Animator = obj1.GetComponent<Animator>();
        if (obj1Animator != null)
        {
            obj1Animator.SetBool("isAnotherOpened", true);
            obj1Animator.SetBool("isClicked", false);
            animator.SetBool("isAnotherOpened", false);
        }

        Animator obj2Animator = obj2.GetComponent<Animator>();
        if (obj2Animator != null)
        {
            obj2Animator.SetBool("isAnotherOpened", true);
            obj2Animator.SetBool("isClicked", false);
            animator.SetBool("isAnotherOpened", false);
        }

        if (currentClickedButton != null && currentClickedButton != this)
        {
            currentClickedButton.ResetClickState();
        }

        animator.SetBool("isClicked", true);
        currentClickedButton = this;
    }

    private void ResetClickState()
    {
        animator.SetBool("isClicked", false);
    }
}
