using System.Collections;
using UnityEngine;
using TMPro;
//Simple class to make a nie looking survived round counter when you lose or win 

public class RoundsSurvived : MonoBehaviour
{
    public TMP_Text roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;
        yield return new WaitForSeconds(.7f);
        while (round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}
