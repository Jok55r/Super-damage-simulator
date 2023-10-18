using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager  : MonoBehaviour
{
    public GameObject levelPanel;
    public GameObject player;
    public static float gameSpeed = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ToggleLevelPanel();
    }

    public void TryLevelUP()
    {
        Creature character = player.GetComponent<PlayerScript>().character[player.GetComponent<PlayerScript>().current];

        if (character.MoneyPerLevel * character.level <= Global.money && character.levelXP <= Global.XP)
        {
            Global.money -= character.MoneyPerLevel;
            Global.XP -= character.levelXP;
            character.LevelUP();
            levelPanel.GetComponent<CharacterPanelScript>().UpdateValues();
        }
    }

    public void ToggleLevelPanel()
    {
        Creature character = player.GetComponent<PlayerScript>().character[player.GetComponent<PlayerScript>().current];

        levelPanel.SetActive(!levelPanel.activeSelf);
        gameSpeed = levelPanel.activeSelf ? 0 : 1;
        levelPanel.GetComponent<CharacterPanelScript>().character = character;
        levelPanel.GetComponent<CharacterPanelScript>().UpdateValues();
    }
}