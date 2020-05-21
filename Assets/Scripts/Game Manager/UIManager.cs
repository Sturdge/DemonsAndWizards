using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    /*TODO - Add proper player HUD*/

    [SerializeField]
    private List<TextMeshProUGUI> goldTexts;

    public void UpdateGoldText()
    {
        for(int i = 0; i < GameManager.Instance.EntityManager.Players.Count; i++)
        {
            goldTexts[i].text = $"Gold: {GameManager.Instance.EntityManager.Players[i].Money}";
        }

    }
}
