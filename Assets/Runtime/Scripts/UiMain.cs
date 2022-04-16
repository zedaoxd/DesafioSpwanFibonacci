using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMain : MonoBehaviour
{
    [SerializeField] private Text quantityShips;
    [SerializeField] private EndlessGenerator endless;


    private void LateUpdate()
    {
        quantityShips.text = $"Total number of ships: {endless.CountOfShips:D4}\nRemaining: {endless.RemaningShips:D4}";
    }
}
