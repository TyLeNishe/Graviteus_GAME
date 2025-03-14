using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AtomicStoveTypingManegers : MonoBehaviour
{
    public Text StoveTitleText;
    public Text Consumption;
    public Text AtomicStoveOption1;
    public Text AtomicStoveOption2;
    public Text AtomicStoveOption3;
    public float typingSpeed = 0.1f;
    public GameObject Terminal;
    private string fullStoveTitleText;
    private string fullConsumption;
    private string fullAtomicStoveOption1;
    private string fullAtomicStoveOption2;
    private string fullAtomicStoveOption3;
    public bool isTyping;
    private Coroutine typingCoroutine;
    private bool isStopped = false;
    private int tmp = 0;

    void Start()
    {
        fullStoveTitleText = StoveTitleText.text;
        fullConsumption = Consumption.text;
        fullAtomicStoveOption1 = AtomicStoveOption1.text;
        fullAtomicStoveOption2 = AtomicStoveOption2.text;
        fullAtomicStoveOption3 = AtomicStoveOption3.text;

        Terminal.SetActive(true);

        StoveTitleText.text = "";
        Consumption.text = "";
        AtomicStoveOption1.text = "";
        AtomicStoveOption2.text = "";
        AtomicStoveOption3.text = "";

        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText());
    }

    void OnEnable()
    {
        if (tmp == 1)
        {
            StoveTitleText.text = "";
            Consumption.text = "";
            AtomicStoveOption1.text = "";
            AtomicStoveOption2.text = "";
            AtomicStoveOption3.text = "";

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
        for (int i = 0; i < fullStoveTitleText.Length && !isStopped; i++)
        {
            StoveTitleText.text += fullStoveTitleText[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        for (int i = 0; i < fullConsumption.Length && !isStopped; i++)
        {
            Consumption.text += fullConsumption[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        for (int i = 0; i < fullAtomicStoveOption1.Length && !isStopped; i++)
        {
            AtomicStoveOption1.text += fullAtomicStoveOption1[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        for (int i = 0; i < fullAtomicStoveOption2.Length && !isStopped; i++)
        {
            AtomicStoveOption2.text += fullAtomicStoveOption2[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        for (int i = 0; i < fullAtomicStoveOption3.Length && !isStopped; i++)
        {
            AtomicStoveOption3.text += fullAtomicStoveOption3[i];
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

