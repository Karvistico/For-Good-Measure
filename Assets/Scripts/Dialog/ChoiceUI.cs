using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonData
{
    public string text;
    public System.Action action;

    public OptionButtonData(string text, System.Action action)
    {
        this.text = text;
        this.action = action;
    }
}

public class ChoiceUI : MonoBehaviour
{
    public GameObject leftContainer;
    public GameObject rightContainer;
    public GameObject topContainer;
    public GameObject choiceTitle;

    private void ClearPanel()
    {
        choiceTitle.GetComponent<TextMeshProUGUI>().text = "";
        leftContainer.SetActive(false);
        rightContainer.SetActive(false);
        topContainer.SetActive(false);
        //foreach (Transform child in choiceContainer.transform)
        //    Destroy(child.gameObject);
    }

    public void ShowOptions(string message, OptionButtonData[] options)
    {
        //Reset state of option panels
        ClearPanel();
        int optionCount = 0;
        choiceTitle.GetComponent<TextMeshProUGUI>().text = message;
        GameObject target;

        foreach (var option in options)
        {
            target = leftContainer;
            if (optionCount==1)
                target = rightContainer;
            if (optionCount==2)
                target = topContainer;
            ShowOption(option, target);
            optionCount++;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ShowOption(OptionButtonData option, GameObject target)
    {
        target.SetActive(true);
        target.transform.Find("Content Panel").GetComponentInChildren<TextMeshProUGUI>().text = option.text;
        Debug.Log(target.transform.Find("Content Panel"));
        target.transform.Find("Content Panel").GetComponent<Button>().onClick.AddListener(() =>
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            option.action.Invoke();
        });
    }
}
