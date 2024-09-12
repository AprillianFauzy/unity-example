using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    public DiceRoll diceRollScript;

    void Start()
    {
        diceRollScript = FindObjectOfType<DiceRoll>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Face"))
        {
            Rigidbody rb = diceRollScript.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude == 0)
            {
                int faceNumber;
                if (int.TryParse(other.name.Replace("Face", ""), out faceNumber))
                {
                    diceRollScript.diceFaceNumber = faceNumber;
                    Debug.Log("Dice Face Number: " + diceRollScript.diceFaceNumber);
                }
            }
        }
    }
}
