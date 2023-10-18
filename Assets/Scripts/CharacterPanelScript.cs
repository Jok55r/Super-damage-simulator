using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelScript : MonoBehaviour
{
    public Creature character;

    public Image image;
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI level;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI dmg;
    public TextMeshProUGUI defence;
    public TextMeshProUGUI crate;
    public TextMeshProUGUI cdmg;
    public TextMeshProUGUI speed;

    public TextMeshProUGUI totalMoney;
    public TextMeshProUGUI totalXP;

    public TextMeshProUGUI levelMoney;
    public TextMeshProUGUI levelXP;

    public void UpdateValues()
    {
        image.sprite = character.data.image;

        nameTMP.text = character.data.creatureName.ToString();
        level.text = character.level.ToString();
        hp.text = character.health.ToString();
        dmg.text = character.curdamage.ToString();
        defence.text = character.curdefence.ToString();
        crate.text = character.curcrate.ToString();
        cdmg.text = character.curcdamage.ToString();
        speed.text = character.curattackSpeed.ToString();

        totalMoney.text = Global.money.ToString();
        totalXP.text = Global.XP.ToString();

        levelMoney.text = (character.MoneyPerLevel * character.level).ToString();
        levelXP.text = character.levelXP.ToString();
    }
}