using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitializationTypingManager : MonoBehaviour
{
    public Text mainText;
    public float sec;
    public float typingSpeed = 0.1f;
    public bool isTyping;
    public GameObject objectToHide;
    public GameObject objectToOpen;
    public GameObject objectToOpen1;
    public GameObject Head;

    private string fullMainText;
    private Coroutine typingCoroutine;
    private bool isStopped = false;

    void Start()
    {
        fullMainText = mainText.text;
        mainText.text = "";
        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText());
    }

    void OnEnable()
    {
        StartCoroutine(HideAndShowObjects());
    }

    private IEnumerator HideAndShowObjects()
    {
        yield return new WaitForSeconds(sec);
        if (objectToHide != null) objectToHide.SetActive(false);
        if (objectToOpen != null) objectToOpen.SetActive(true);
        if (objectToOpen1 != null) objectToOpen1.SetActive(true);
        if (Head != null) Head.SetActive(true);
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < fullMainText.Length && !isStopped; i++)
        {
            mainText.text += fullMainText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                typingSpeed = 0.005f;
            }
        }
    }
}
