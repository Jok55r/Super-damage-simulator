using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WishManager : MonoBehaviour
{
    public Image prefab;
    public Button button;

    public TextMeshProUGUI primoText;

    public int lastLeg;
    public const int guarantee = 90;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            gameObject.SetActive(!gameObject.activeSelf);
            if (gameObject.activeSelf)
            {
                Global.gameSpeed = 0;
                primoText.text = Global.primogems.ToString() + " Primogems";
            }
            else
            {
                Global.gameSpeed = 1;
            }
        }
    }

    public void Wish1()
    {
        if (Global.primogems < 16)
            return;

        Global.primogems -= 16;

        Drop();
        primoText.text = Global.primogems.ToString() + " Primogems";
    }

    public void Wish10()
    {
        if (Global.primogems < 160)
            return;

    }

    private void Drop()
    {
        if (UnityEngine.Random.Range(0f, 100f) < (float)(lastLeg+1f) / guarantee)
        {
            CreatureData drop = Resources.LoadAll<CreatureData>("Creatures").Where(obj => obj.star == Star.five).ToArray()[Random.Range(0, Resources.LoadAll<CreatureData>("Creatures").Length)];
            prefab.sprite = drop.image;
        }
        else
        {
            List<CreatureData> allFourStar = Resources.LoadAll<CreatureData>("Assets/Creatures").Where(obj => obj.star == Star.four).ToList();
            int rnd = Random.Range(0, allFourStar.Count);
            CreatureData drop = allFourStar[rnd];
            prefab.sprite = drop.image;
        }
    }
}