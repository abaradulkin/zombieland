using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsUIScript : MonoBehaviour
{
    private TextMeshProUGUI pointsText;

    void Start()
    {
        GameController gameController = FindObjectOfType<GameController>();
        pointsText = GetComponent<TextMeshProUGUI>();
        gameController.OnChangeActionPoint += ChangeActionPopintText;
    }

    private void ChangeActionPopintText(uint value)
    {
        pointsText.text = "Action points: " + value;
    }
}
