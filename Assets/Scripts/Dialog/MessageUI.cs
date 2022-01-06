using System.Collections;
using System.Collections.Generic;
using Conversa.Runtime.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public RectTransform panelContainer;
    public GameObject panelPrefab;
    public Button nextMessageButton;
    public Transform targetA;
    public Transform targetB;

    GameObject instance;
    Transform target;

    private System.Action nextMessageCallback;

    public void ShowMessage(string speaker, string message, System.Action callback)
    {
        //Instantiate bubble
        instance = Instantiate(panelPrefab, panelContainer, true);
        
        //Figure out who's it for
        target = (speaker == "A") ? targetA : targetB;
        Debug.Log("<color=red>PRINTING</color> " + speaker + ": " + message);
        //Fill data
        instance.transform.Find("Content Panel").Find("Text").GetComponent<TextMeshProUGUI>().text = message;
        instance.GetComponent<SpeechBubbleTrack>().target = target;
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelContainer);

        nextMessageCallback = callback;
    }

    public void PrintAllMessages(List<MessageEvent> queue, System.Action callback){
        foreach (MessageEvent message in queue)
                ShowMessage(message.Actor, message.Message, callback);
        queue.Clear();
    }

    void ClearAllMessages(){
        foreach(Transform child in panelContainer)
            {
                child.GetComponent<Animator>().SetTrigger("Continue");
                Debug.Log("Clearing a chat bubble");
            }
    }
    
    void Update(){
        if(Input.GetMouseButtonDown(0))
        {
            ClearAllMessages();
            nextMessageCallback.Invoke();
        }
    }
}