using UnityEngine;

public class ShowAndHideObject : MonoBehaviour
{
    public GameObject objectToShow;
    public float showDuration = 2f;

    private void Start()
    {
        // Hide the object on start
        objectToShow.SetActive(false);

        // Show the object for the specified duration
        ShowObjectForDuration(showDuration);
    }

    private void ShowObjectForDuration(float duration)
    {
        objectToShow.SetActive(true);
        Invoke("HideObject", duration);
    }

    private void HideObject()
    {
        objectToShow.SetActive(false);
    }
}

