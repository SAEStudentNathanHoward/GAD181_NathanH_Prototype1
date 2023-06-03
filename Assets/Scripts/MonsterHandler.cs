using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class MonsterHandler : MonoBehaviour
{
    // Declaration of the monster image list and the object in which to display the image
    [Header("Monster Options")]
    [SerializeField] private List<Sprite> monsterImages;
    [SerializeField] private Image monsterDisplay;

    // Declaration of the monster and player stats
    public int monsterHP;
    private int monstersSlain;
    public static int weaponLevel;
    public bool stopInput;

    // Declaration of UI component that displays HP
    [SerializeField] private TextMeshProUGUI monsterHPDisplay;

    // Declaration of the sword slash sound effect
    [Header("Sound Effects")]
    [SerializeField] private AudioSource swordSlash;

    // Declaration of the GoldHandler object to allow adding of gold
    [Header("Gold Handler Object")]
    [SerializeField] private GameObject goldHandlerObject;

    // Declaration of the LevelHandler object to allow dificulty changes
    [Header("Level System Object")]
    [SerializeField] private GameObject levelHandlerObject;

    // Start is called at the start of the game
    private void Start()
    {
        monsterHP = 10 * levelHandlerObject.GetComponent<LevelSystem>().levelDificulty;
        stopInput = false;
        SpawnMonster();

        if (weaponLevel == 0)
        {
            weaponLevel = 1;
        }
    }

    // Update is called once a frame
    private void Update()
    {
        // Checking if the monster is slain and if so spawning a new monster
        if (monsterHP <= 0)
        {
            monstersSlain += 1;
            if (SceneManager.GetActiveScene().name == "Level5(Boss)")
            {
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                SpawnMonster();
                goldHandlerObject.GetComponent<GoldHandler>().AddGold();
                monsterHPDisplay.text = monsterHP.ToString();
            }
        }

        // Checking if the monster is attacked by pressing the space bar
        if (Input.GetKeyDown(KeyCode.Space) && stopInput == false)
        {
            AttackMonster();
        }
    }
    
    // Method used to spawn the monsters
    private void SpawnMonster()
    {
        if (SceneManager.GetActiveScene().name == "Level5(Boss)")
        {
            monsterDisplay.sprite = monsterImages[0];
            monsterHP = 50 * levelHandlerObject.GetComponent<LevelSystem>().levelDificulty;
        }
        else
        {
            monsterDisplay.sprite = monsterImages[Random.Range(1, monsterImages.Count)];
            monsterHP = 10 * levelHandlerObject.GetComponent<LevelSystem>().levelDificulty;
        }
    }

    // Method used to attack the monster
    private void AttackMonster()
    {
        //Debug.Log("the monster is attackeed");
        //Debug.Log("the monsters hp is" + monsterHP);
        //play animation
        swordSlash.Play();
        monsterHP -= 1 * weaponLevel;
        monsterHPDisplay.text = monsterHP.ToString();
    }
}
