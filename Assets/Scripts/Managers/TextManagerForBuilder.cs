using UnityEngine;
using UnityEngine.UI;

public class TextManagerForBuilder : MonoBehaviour
{
    public bool isObs = true;
    public bool isIgn = false;
    public bool isVen = false;
    public Text title;
    public Text description;
    public Text cost1;
    public Text cost2;
    public void ToggleActiveNext()
    {
        if (isObs) { isObs = false; isIgn = true; isVen = false; }
        else if (isIgn) { isObs = false; isIgn = false; isVen = true; }
    }

    public void ToggleActivePrevios()
    {
        if (isIgn) { isObs = true; isIgn = false; isVen = false; }
        else if (isVen) { isObs = false; isIgn = true; isVen = false; }
    }
    void Update()
    {
        if (isObs)
        {
            title.text = "Буровая установка";
            description.text = "Плавит породу лазером. \n Добывает тёмниум.";
            cost1.text = "Прочнит: 2";
            cost2.text = "Нестабилий: 3";
        }

        if (isIgn)
        {
            title.text = "Нагнетательная скважина";
            description.text = "Качает огнемасло из под повехности. \n Добывает огнемасло.";
            cost1.text = "Прочнит: 1";
            cost2.text = "Нестабилий: 2";
        }

        if (isVen)
        {
            title.text = "Атмосфеный конденсатор";
            description.text = "Извлекает токсид из атмосферы. \n Добывает токсид.";
            cost1.text = "Прочнит: 3";
            cost2.text = "Нестабилий: 2";
        }
    }
}
