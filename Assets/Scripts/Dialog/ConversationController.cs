using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Conversa.Runtime;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour
{
    public Conversation conversation;
    public MessageUI messageUI;
    public ChoiceUI choiceUI;
    public Button startConversationButton;

    List<MessageEvent> messageQueue = new List<MessageEvent>();

    void Start(){
        Debug.Log("Convo start");
        StartDialogue();
    }

    public void StartDialogue()
    {
        startConversationButton.interactable = false; //This just helps me see when convo has started. To delete later.

        ConversationRunner runner = new ConversationRunner(conversation);
        runner.OnConversationEvent.AddListener(HandleConversationEvent);

        runner.Begin();
    }
    
    private void HandleConversationEvent(IConversationEvent e)
    {
        switch (e)
        {
            case MessageEvent messageEvent:
                Debug.Log("<color=yellow>Message Event</color>: (" + messageEvent.Actor + ": " + messageEvent.Message + ")");
                HandleMessageEvent(messageEvent);
                messageEvent.Advance();
                break;

            case UserEvent userEvent:
                break;

            case PauseEvent pauseEvent:
                Debug.Log("<color=yellow>Pause Event</color>");
                HandlePauseEvent(pauseEvent);
                break;

            case DelayEvent delayEvent:
                Debug.Log("<color=yellow>Delay Event</color>: " + delayEvent.Value);
                delayEvent.Advance();
                break;

            //Let's crack simple messages first...
            /*
            case ChoiceEvent choiceEvent:
                Debug.Log("<color=yellow>Choice Event</color>");
                HandleChoiceEvent(choiceEvent);
                break;
            */

            case EndEvent _:
                HandleEndEvent();
                break;
        }
    }

    private void HandleMessageEvent(MessageEvent e)
    {
        //Message events are sent to a queue before being fired by a "Pause" User Event or an End Event.
        messageQueue.Add(e);
    }

    private void HandlePauseEvent(PauseEvent e)
    {
        messageUI.PrintAllMessages(messageQueue, e.Advance);
    }

    // USER EVENTS

    private void PrintAllMessages()
    {
        foreach (MessageEvent message in messageQueue)
                messageUI.ShowMessage(message.Actor, message.Message, message.Advance);
                
        messageQueue.Clear();
    }

    // USER EVENTS END

    private void HandleEndEvent()
    {
        PrintAllMessages(); //End events print all remaining messages in queue, if any.
        
        startConversationButton.interactable = true; //This just helps me see when convo is finished. To delete later.
    }

    /*
    private void HandleChoiceEvent(ChoiceEvent e)
    {
        var options = e.Options.Select(x => new OptionButtonData(x.Message, x.Advance)).ToArray();
        choiceUI.ShowOptions(options);
    }
    */
}