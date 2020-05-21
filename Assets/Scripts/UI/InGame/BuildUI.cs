using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class BuildUI : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private GameObject buildUI;
    [SerializeField]
    private Image[] buildUIElements;

    [Header("Graphical")]
    [SerializeField]
    private Sprite selectedSprite;
    [SerializeField]
    private Sprite inactiveSprite;
    [SerializeField]
    private TextMeshProUGUI turretNameText;
    [SerializeField]
    private TextMeshProUGUI turretCostText;

    private PlayerController player;
    private Image previousSelection;
    private TurretBuildZone buildZone;
    private Turret selectedTurret;

    public bool IsOpen { get; private set; }
    public int SelectedIndex { get; private set; }

    public void OpenBuildUI(PlayerController p)
    {
        player = p;
        buildZone = player.CurrentBuildZone;

        buildUI.SetActive(true);
        IsOpen = true;

        SelectedIndex = 0;
        selectedTurret = buildZone.Turrets[SelectedIndex];
        ChangeDisplayText();
        ChangeSelectedSprite(SelectedIndex);
    }

    public void CloseBuildUI()
    {
        buildUI.SetActive(false);
        IsOpen = false;

        player.Input.SwitchCurrentActionMap("Gameplay");

        player = null;
    }

    private void OnConfirm(InputValue inputValue)
    {
        if (player.Money > buildZone.Turrets[SelectedIndex].Data.Cost)
        {
            player.CurrentBuildZone.BuildTurret(SelectedIndex);
            player.ModifiyMoney(-selectedTurret.Data.Cost);
            CloseBuildUI();
        }
    }

    private void OnCancel(InputValue inputValue)
    {
        CloseBuildUI();
    }

    private void OnSelectionMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        //SelectedIndex += (int)input.x;

        if (input.x > 0)
        {
            SelectedIndex++;
            if (SelectedIndex == player.CurrentBuildZone.Turrets.Count)
                SelectedIndex = 0;
        }
        else if (input.x < 0)
        {
            SelectedIndex--;
            if (SelectedIndex < 0)
                SelectedIndex = player.CurrentBuildZone.Turrets.Count -1;
        }

        selectedTurret = buildZone.Turrets[SelectedIndex];
        ChangeDisplayText();
        ChangeSelectedSprite(SelectedIndex);
        
    }

    private void ChangeSelectedSprite(int index)
    {
        buildUIElements[SelectedIndex].sprite = selectedSprite;
        if (previousSelection != null)
            previousSelection.sprite = inactiveSprite;
        previousSelection = buildUIElements[SelectedIndex];
    }

    private void ChangeDisplayText()
    {
        turretNameText.text = selectedTurret.Data.TurretName;
        turretCostText.text = $"Cost: {selectedTurret.Data.Cost}";
    }

}
