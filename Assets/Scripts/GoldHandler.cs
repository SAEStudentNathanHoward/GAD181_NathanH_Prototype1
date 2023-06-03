using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldHandler : MonoBehaviour
{
    // Declaration of the objects needed to display the gold obtained
    [SerializeField] public static int goldObtained;
    [SerializeField] private TextMeshProUGUI goldCounter;

    [SerializeField] private GameObject levelHandlerObject;

    // Continues to update the gold counter in the bottom corner
    private void Update()
    {
        // Setting the gold text
        goldCounter.text = goldObtained.ToString();
    }

    // Method used for adding the gold
    public void AddGold()
    {
        goldObtained = goldObtained + levelHandlerObject.GetComponent<LevelSystem>().levelDificulty;
    }

    public void UpdateGoldDisplay()
    {
        // Setting the gold text
        goldCounter.text = goldObtained.ToString();
    }
}
