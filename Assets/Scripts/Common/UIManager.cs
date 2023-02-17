using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerPrefs playerPrefs;
    private Entity playerEntity;

    public Image healthBar;
    public Image expBar;
    public Text levelText;
    [Space]
    public GameObject upgradeShockwaveButton;
    public GameObject upgradeBurstButton;
    public GameObject upgradeMachinegunButton;
    public GameObject upgradeAirstrikeButton;
    public GameObject upgradeShieldButton;
    [Space]
    public Text upgradeText1;
    public Text upgradeText2;
    public Text upgradeText3;
    public Text upgradeText4;
    public Text upgradeText5;

    private Vector3 upgradeButtonPos1;
    private Vector3 upgradeButtonPos2;
    private Vector3 upgradeButtonPos3;
    private Vector3 upgradeTextPos1;
    private Vector3 upgradeTextPos2;
    private Vector3 upgradeTextPos3;

    private void Start()
    {
        upgradeButtonPos1 = upgradeShockwaveButton.transform.position;
        upgradeButtonPos2 = upgradeBurstButton.transform.position;
        upgradeButtonPos3 = upgradeMachinegunButton.transform.position;
        upgradeTextPos1 = upgradeText1.transform.position;
        upgradeTextPos2 = upgradeText2.transform.position;
        upgradeTextPos3 = upgradeText3.transform.position;

        upgradeText1.text = "Массовый электрический импульс";
        upgradeText2.text = "Выстрел дробью с большим разбросом";
        upgradeText3.text = "Временное увеличение скорости атаки";
        upgradeText4.text = "Удар с воздуха по выбранной области";
        upgradeText5.text = "Наносящий урон щит";

        playerPrefs = GetComponent<PlayerPrefs>();
        playerEntity = GetComponent<Entity>();

        UpdateExp();
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthBar.fillAmount = playerEntity.currentHealth / playerEntity.maxHeatlh;
    }

    public void UpdateExp()
    {
        expBar.fillAmount = playerPrefs.currentExp / playerPrefs.expForNextLevel;
        levelText.text = playerPrefs.level.ToString();
    }

    public void ShowUpgrades()
    {
        Time.timeScale = 0;

        List<GameObject> upgradeButtons = new() { upgradeShockwaveButton, upgradeBurstButton, upgradeMachinegunButton, upgradeAirstrikeButton, upgradeShieldButton };
        List<Text> upgradeTexts = new() { upgradeText1, upgradeText2, upgradeText3, upgradeText4, upgradeText5 };
        int randomIndex;
        
        for (int i = 0; i < 3; i++)
        {
            randomIndex = UnityEngine.Random.Range(0, upgradeButtons.Count);
            upgradeButtons[randomIndex].SetActive(true);
            upgradeTexts[randomIndex].gameObject.SetActive(true);
            switch (i)
            {
                case 0:
                    upgradeButtons[randomIndex].transform.position = upgradeButtonPos1;
                    upgradeTexts[randomIndex].transform.position = upgradeTextPos1;
                    break;

                case 1:
                    upgradeButtons[randomIndex].transform.position = upgradeButtonPos2;
                    upgradeTexts[randomIndex].transform.position = upgradeTextPos2;
                    break;

                case 2:
                    upgradeButtons[randomIndex].transform.position = upgradeButtonPos3;
                    upgradeTexts[randomIndex].transform.position = upgradeTextPos3;
                    break;
            }
            upgradeButtons.RemoveAt(randomIndex);
            upgradeTexts.RemoveAt(randomIndex);
        }
    }
    public void HideUpgrades()
    {
        List<GameObject> upgradeButtons = new() { upgradeShockwaveButton, upgradeBurstButton, upgradeMachinegunButton, upgradeAirstrikeButton, upgradeShieldButton };
        List<Text> upgradeTexts = new() { upgradeText1, upgradeText2, upgradeText3, upgradeText4, upgradeText5 };
        foreach (GameObject upgradeButton in upgradeButtons)
        {
            upgradeButton.SetActive(false);
        }
        foreach (Text upgradeText in upgradeTexts)
        {
            upgradeText.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }

}
