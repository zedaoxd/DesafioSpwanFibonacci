using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private EndlessGenerator endless;

    private void Update()
    {
        foreach (var ship in endless.ShipsList)
        {
            var position = ship.transform.position;
            position.z += ship.Speed * Time.deltaTime;
            ship.transform.position = position;
        }
    }
}
