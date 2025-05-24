using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MainOptionsTypingManager : MonoBehaviour
{
    public Text tytle;
    public Text sysLog;

    public float typingSpeed = 0.1f;
    private string fulltytle;
    private string fullsysLog;
    public bool isTyping;
    private Coroutine typingCoroutine;
    private bool isStopped = false;
    private int tmp;

    void Start()
    {
        fulltytle = tytle.text;
        fullsysLog = sysLog.text;
        tytle.text = "";
        sysLog.text = "";
        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText());
    }

    void OnEnable()
    {
        if (tmp == 1)
        {
            tytle.text = "";
            sysLog.text = "";
            isTyping = true;
            typingCoroutine = StartCoroutine(TypeText());
            tmp = 0;
        }
    }

    void OnDisable()
    {
        tmp = 1;
        typingSpeed = 0.02f;
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < fulltytle.Length && !isStopped; i++)
        {
            tytle.text += fulltytle[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        for (int i = 0; i < fullsysLog.Length && !isStopped; i++)
        {
            sysLog.text += fullsysLog[i];
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
