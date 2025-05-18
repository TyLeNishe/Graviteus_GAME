using UnityEngine;
using UnityEngine.EventSystems;

public class SpeedAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    private Animator animator;
    private static SpeedAnimation currentClickedButton;

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
        if (obj1 != null)
        {
            Animator obj1Animator = obj1.GetComponent<Animator>();
            if (obj1Animator != null)
            {
                obj1Animator.SetBool("isAnotherOpened", true);
                obj1Animator.SetBool("isClicked", false);
            }
        }

        if (obj2 != null)
        {
            Animator obj2Animator = obj2.GetComponent<Animator>();
            if (obj2Animator != null)
            {
                obj2Animator.SetBool("isAnotherOpened", true);
                obj2Animator.SetBool("isClicked", false);
            }
        }

        if (obj3 != null)
        {
            Animator obj3Animator = obj3.GetComponent<Animator>();
            if (obj3Animator != null)
            {
                obj3Animator.SetBool("isAnotherOpened", true);
                obj3Animator.SetBool("isClicked", false);
            }
        }

        if (currentClickedButton != null && currentClickedButton != this)
        {
            currentClickedButton.animator.SetBool("isClicked", false);
        }

        animator.SetBool("isAnotherOpened", false);
        animator.SetBool("isClicked", true);
        currentClickedButton = this;
    }
}