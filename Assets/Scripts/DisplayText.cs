using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public GameObject buttonObject;
    public float delayTime = 5f;

    private void Start()
    {
        // Initially hide the button
        buttonObject.SetActive(false);

        // Start the coroutine to display the button after the specified delay
        StartCoroutine(DisplayButtonCoroutine());
    }

    private IEnumerator DisplayButtonCoroutine()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Display the button after the delay
        buttonObject.SetActive(true);
    }
}

