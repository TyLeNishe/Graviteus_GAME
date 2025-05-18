using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour
{
    public bool isActive = false;
    public bool panelActivate = false;
    public GameObject panel;
    public Text obsText;
    public Text ignText;
    public Text venText;
    public int obs = 0;
    public int ign = 0;
    public int ven = 0;

    public void ToggleActiveState()
    {
        isActive = !isActive;
    }

    void Update()
    {
        if (panelActivate)
        {
            if (obs > 0 && obs <= 50)
            {
                obsText.color = new Color32(191, 7, 7, 255);
                obsText.text = "Ò¸ìíèóì: ÍÈÇÊÀß";
            }
            else if (obs >= 51 && obs <= 76)
            {
                obsText.color = new Color32(255, 196, 0, 255);
                obsText.text = "Ò¸ìíèóì: ÑÐÅÄÍßß";
            }
            else
            {
                obsText.color = new Color32(0, 240, 7, 255);
                obsText.text = "Ò¸ìíèóì: ÂÛÑÎÊÀß";
            }

            if (ign > 0 && ign <= 50)
            {
                ignText.color = new Color32(191, 7, 7, 255);
                ignText.text = "Îãíåìàñëî: ÍÈÇÊÀß";
            }
            else if (ign >= 51 && ign <= 76)
            {
                ignText.color = new Color32(255, 196, 0, 255);
                ignText.text = "Îãíåìàñëî: ÑÐÅÄÍßß";
            }
            else
            {
                ignText.color = new Color32(0, 240, 7, 255);
                ignText.text = "Îãíåìàñëî: ÂÛÑÎÊÀß";
            }

            if (ven > 0 && ven <= 50)
            {
                venText.color = new Color32(191, 7, 7, 255);
                venText.text = "Òîêñèä: ÍÈÇÊÀß";
            }
            else if (ven >= 51 && ven <= 76)
            {
                venText.color = new Color32(255, 196, 0, 255);
                venText.text = "Òîêñèä: ÑÐÅÄÍßß";
            }
            else
            {
                venText.color = new Color32(0, 240, 7, 255);
                venText.text = "Òîêñèä: ÂÛÑÎÊÀß";
            }

            panel.gameObject.SetActive(true);
        }
        if (!isActive)
        {
            panel.gameObject.SetActive(false);
            panelActivate = false;
        }
    }
}
