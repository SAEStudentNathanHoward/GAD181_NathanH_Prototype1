using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    // Declaration of the variables for dificulty and the next level
    [Header("Level and Timer Settings")]
    [SerializeField] public int levelDificulty;
    [SerializeField] private string nextLevel;
    [SerializeField] private GameObject monsterHandler;
    [SerializeField] private GameObject goldHandler;

    // Declaration for the variables of the costs on the upgrade screen
    [Header("Upgrade Menu Costs")]
    private bool upgradeScreenVisable;
    public static int weaponUpgradeCost;
    public static int nextKeyCost;
    [SerializeField] private TextMeshProUGUI weaponCostDisplay;
    [SerializeField] private TextMeshProUGUI keyCostDisplay;
    [SerializeField] private Button weaponButton;
    [SerializeField] private Sprite weaponButtonStop;

    // Declartation of the canvas's used at the main menu
    [Header("Game Canvas Object (For Main Menu ONLY)")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas instructionCanvas;

    // Declaration of the Upgrade screen canvas and the game canvas
    [Header("Upgrade Screen Handler")]
    [SerializeField] private Canvas upgradeScreenCanvas;
    [SerializeField] private Canvas gameScreenCanvas;

    // Sets the key cost to 1 at the start of the levels (for a bug fix)
    private void Start()
    {
        upgradeScreenVisable = false;

        if (nextKeyCost == 0)
        {
            nextKeyCost = 1;
        }

        if (weaponUpgradeCost == 0)
        {
            weaponUpgradeCost = 1;
        }
    }

    // Constatnly runs while game is active
    private void Update()
    {
        // Checks to see if the timer object is set (null reference error fix)
        if (monsterHandler != null)
        {
            // Checks to see if the seconds remaining in the current level is 0
            if (monsterHandler.GetComponent<TenSecondTimer>().timeRemaining == 0 && upgradeScreenVisable == false )
            {
                UpgradeScreen();
            }
        }
    }

    // Method for when the upgrade screen is finished
    public void UpgradeFinish()
    {
            GoldHandler.goldObtained -= nextKeyCost;
            upgradeScreenCanvas.enabled = false;
            nextKeyCost = nextKeyCost * 2;
            LoadNextLevel();
    }

    // Method that displays the upgrade screen
    private void UpgradeScreen()
    {
        if (SceneManager.GetActiveScene().name == "Level5(Boss)")
        {
            if (monsterHandler.GetComponent<MonsterHandler>().monsterHP == 0)
            {
                LoadNextLevel();
            }
            else
            {
                SceneManager.LoadScene("LossScreen");
            }
        }
        else
        {
            upgradeScreenVisable = true;
            if (GoldHandler.goldObtained < nextKeyCost)
            {
                SceneManager.LoadScene("LossScreen");
                Debug.Log("This is as far as you go");
            }
            else
            {
                weaponCostDisplay.text = weaponUpgradeCost.ToString();
                keyCostDisplay.text = nextKeyCost.ToString();

                upgradeScreenCanvas.enabled = true;
                monsterHandler.GetComponent<MonsterHandler>().stopInput = true;
            }
        }
    }

    // Method used to load the instructions (only from the main menu)
    public void LoadHowTo()
    {
        instructionCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
    }

    // Method used to load the next level (set in inspector)
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    // Method used to upgrade the weapons damage
    public void WeaponLevelUpgrade()
    {
        if (GoldHandler.goldObtained -  weaponUpgradeCost > nextKeyCost)
        {
            GoldHandler.goldObtained -= weaponUpgradeCost;
            MonsterHandler.weaponLevel += 1;
            weaponUpgradeCost = weaponUpgradeCost * 2 ;
            weaponCostDisplay.text = weaponUpgradeCost.ToString();
            keyCostDisplay.text = nextKeyCost.ToString();

            if (GoldHandler.goldObtained - weaponUpgradeCost <= nextKeyCost)
            {
                weaponButton.interactable = false;
                weaponButton.image.sprite = weaponButtonStop;
            }
        }
        else
        {
            weaponButton.interactable = false;
            weaponButton.image.sprite = weaponButtonStop;
            Debug.Log("You cant afford this and the next level key");
        }
    }

    // Method used to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
