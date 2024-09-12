using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private DiceRoll diceRollScript;

    void Start()
    {
        diceRollScript = FindObjectOfType<DiceRoll>();
    }

    void Update()
    {
        if (diceRollScript.diceFaceNumber != 0)
        {
            // Update the text to show only the dice face number
            scoreText.text = diceRollScript.diceFaceNumber.ToString();
        }
    }
}
