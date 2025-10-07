using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MinigameController : MonoBehaviour
{
    [SerializeField] Button buttonPrefab;
    [SerializeField] int numberOfButtons = 5;
    [SerializeField] float timeUntilNextButton = 1.0f;
    [SerializeField] UnityEvent onCompleted;

    List<Button> activeButtons = new();
    private void Start()
    {
        StartMinigame();
    }

    private void Update()
    {
        foreach (var button in activeButtons)
        {
            if (button == null)
            {
                activeButtons.Remove(button);
                break;
            }
        }
    }

    public void StartMinigame()
    {
        StartCoroutine(MinigameLoop());
    }

    IEnumerator MinigameLoop()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            float x = Random.Range(Screen.width * 0.1f, Screen.width * 0.9f);
            float y = Random.Range(Screen.height * 0.1f, Screen.height * 0.9f);

            Button button = Instantiate(buttonPrefab, new Vector2(x, y), Quaternion.identity, transform);
            activeButtons.Add(button);
        }

        while (true)
        {
            yield return new WaitForSeconds(timeUntilNextButton);

            if (activeButtons.Count <= 0)
            {
                break;
            }

            float x = Random.Range(Screen.width * 0.1f, Screen.width * 0.9f);
            float y = Random.Range(Screen.height * 0.1f, Screen.height * 0.9f);

            Button button = Instantiate(buttonPrefab, new Vector2(x, y), Quaternion.identity, transform);
            activeButtons.Add(button);
        }

        onCompleted?.Invoke();
    }
}
