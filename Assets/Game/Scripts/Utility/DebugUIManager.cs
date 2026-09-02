using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject grid;
    [SerializeField]
    private Button lvlUpButton;
    private TextMeshProUGUI speedField;
    private TextMeshProUGUI maxSpeedField;
    private TextMeshProUGUI curLvlField;
    private TextMeshProUGUI curExpField;
    private TextMeshProUGUI expToUpField;
    private TextMeshProUGUI numOfWeaponField;
    private TextMeshProUGUI numOfAccessibleWeaponsField;
    private Rigidbody2D playerRb;

    private void Start()
    {
        speedField = grid.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        maxSpeedField = grid.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        curLvlField = grid.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        curExpField = grid.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        expToUpField = grid.transform.GetChild(9).GetComponent<TextMeshProUGUI>();
        numOfWeaponField = grid.transform.GetChild(11).GetComponent<TextMeshProUGUI>();
        numOfAccessibleWeaponsField = grid.transform.GetChild(13).GetComponent<TextMeshProUGUI>();

        playerRb = Managers.Player.gameObject.GetComponent<Rigidbody2D>();

        lvlUpButton.onClick.AddListener(Managers.ExperienceManager.LevelUp);
    }

    private void Update()
    {
        speedField.text = playerRb.linearVelocity.magnitude.ToString();

        if (float.Parse(maxSpeedField.text) < playerRb.linearVelocity.magnitude)
        {
            maxSpeedField.text = playerRb.linearVelocity.magnitude.ToString();
        }

        curLvlField.text = Managers.ExperienceManager.CurrentLvl.ToString();
        curExpField.text = Managers.ExperienceManager.CurrentExp.ToString();
        expToUpField.text = Managers.ExperienceManager.CurrentExpToProgress.ToString();

        numOfWeaponField.text = Managers.Weapons.GetItemsCount().ToString();
        numOfAccessibleWeaponsField.text = Managers.GameRules.Data.accessibleWeapons.Count.ToString();
    }
}
