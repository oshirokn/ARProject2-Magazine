using UnityEngine;
using UnityEngine.UI;

public class InteractableMap : MonoBehaviour


{
    [SerializeField] TMPro.TMP_Text DebugText;
    Image buttonImage;

    Color originalColor;

    bool isActive;

   [SerializeField] GameObject Stats;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    void Update()
    {
       
    }

    public void OnPointerEnter()
    {
        DebugText.text = "Pointer over Finland";
        buttonImage.color = Color.red;
        
    }

    public void OnPointerExit()
    {
        DebugText.text = "Pointer over Finland";
        buttonImage.color = originalColor;
    }

    public void OnPointerClick()
    {
        DebugText.text = "Pointer click on Finland";
        buttonImage.color = Color.blue;

        /*
        if(Stats.activeInHierarchy)
        {
            Stats.SetActive(false);
        }
        if (!Stats.activeInHierarchy)
        {
            Stats.SetActive(true);
        }
        */
    }

}
