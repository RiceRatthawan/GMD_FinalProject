using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI p1PointText, p2PointText, p1PotionText, p2PotionText, p1CrossText, p2CrossText;
    
    void Start()
    {
        p1PointText = GetComponent<TextMeshProUGUI>();
        p2PointText = GetComponent<TextMeshProUGUI>();
        p1PotionText = GetComponent<TextMeshProUGUI>();
        p2PotionText = GetComponent<TextMeshProUGUI>();
        p1CrossText = GetComponent<TextMeshProUGUI>();
        p2CrossText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateP1PointText(PlayerInventory playerInventory){
        p1PointText.text = playerInventory.P1Points.ToString();
    }

    public void UpdateP2PointText(PlayerInventory playerInventory){
        p2PointText.text = playerInventory.P2Points.ToString();
    }

    public void UpdateP1PotionText(PlayerInventory playerInventory){
        p1PotionText.text = playerInventory.P1NumberOfPotions.ToString();
    }

    public void UpdateP2PotionText(PlayerInventory playerInventory){
        p2PotionText.text = playerInventory.P2NumberOfPotions.ToString();
    }

    public void UpdateP1CrossText(PlayerInventory playerInventory){
        p1CrossText.text = playerInventory.P1NumberOfCrosses.ToString();
    }

    public void UpdateP2CrossText(PlayerInventory playerInventory){
        p2CrossText.text = playerInventory.P2NumberOfCrosses.ToString();
    }
}
