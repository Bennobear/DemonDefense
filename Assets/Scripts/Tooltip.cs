using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tooltipText;
    private static Tooltip Instance { get; set; }
    private void Awake()
    {
        Instance = this;
        Hide();
    }
    //Static method to open the overlay from another script
    public static void Show_Static()
    {
        Instance.Show();
    }
    //Static method to close the overlay from another script
    public static void Hide_Static()
    {
        Instance.Hide();
    }
    //Activates the UpgradeOverlay at the position of the tower
    private void Show()
    {
        gameObject.SetActive(true);
    }
    //Hides the UpgradeOverlay again
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public static void setText_Static(string text)
    {
        Instance.setText(text);
    }

    private void setText(string text)
    {
        tooltipText.text = text;
    }
}
