using System;
using System.Linq;
using Conversa.Runtime;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using Conversa.Runtime.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Conversa.Demo.Scripts
{
	[Serializable]
	public class Character
	{
		public string name;
		public Sprite avatar;
	}

	public class ConversaController : MonoBehaviour
	{
		[SerializeField] private GameObject messageWindow;
		[SerializeField] private Text messageText;
		[SerializeField] private GameObject choiceWindow;

		[SerializeField] private Button nextMessageButton;
		[SerializeField] private Button restartConversationButton;
		[SerializeField] private Image avatar;
		[SerializeField] private Text characterName;

		[SerializeField] private GameObject choiceOptionButtonPrefab;
		[SerializeField] private Conversation conversation;

		[SerializeField] private Character[] characters;

		private ConversationRunner runner;

		private void Start()
		{
			runner = new ConversationRunner(conversation);
			runner.OnConversationEvent.AddListener(HandleConversationEvent);
			restartConversationButton.onClick.AddListener(HandleRestartConversation);
		}

		private void HandleConversationEvent(IConversationEvent e)
		{
			switch (e)
			{
				case MessageEvent messageEvent:
					HandleMessage(messageEvent);
					break;
				case ChoiceEvent choiceEvent:
					HandleChoice(choiceEvent);
					break;
				case UserEvent userEvent:
					HandleUserEvent(userEvent);
					break;
				case EndEvent _:
					HandleEnd();
					break;
			}
		}

		private void HandleMessage(MessageEvent e)
		{
			choiceWindow.SetActive(false);
			messageWindow.SetActive(true);

			var characterAvatar = GetAvatar(e.Actor);
			if (characterAvatar != null) avatar.sprite = characterAvatar;
			characterName.text = e.Actor;

			messageText.text = $"{e.Message}";
			nextMessageButton.onClick.RemoveAllListeners();
			nextMessageButton.onClick.AddListener(() => e.Advance());

		}

		private void HandleChoice(ChoiceEvent e)
		{
			messageWindow.SetActive(true);

			characterName.text = e.Actor;
			messageText.text = e.Message;
			var characterAvatar = GetAvatar(e.Actor);
			if (characterAvatar != null) avatar.sprite = characterAvatar;

			choiceWindow.SetActive(true);

			foreach (Transform child in choiceWindow.transform)
				Destroy(child.gameObject);

			e.Options.ForEach(option =>
			{
				var instance = Instantiate(choiceOptionButtonPrefab, Vector3.zero, Quaternion.identity);
				instance.transform.SetParent(choiceWindow.transform);
				instance.GetComponentInChildren<Text>().text = option.Message;
				instance.GetComponent<Button>().onClick.AddListener(() => option.Advance());
			});
		}

		private void HandleUserEvent(UserEvent userEvent)
		{
			if (userEvent.Name == "Food bought")
				Debug.Log("We can use this event to update the inventory, for instance");
		}

		private void HandleRestartConversation() { runner.Begin(); }

		private void HandleEnd()
		{
			messageWindow.SetActive(false);
			choiceWindow.SetActive(false);
		}

		private Sprite GetAvatar(string character) => characters.FirstOrDefault(x => x.name == character)?.avatar;
	}
}
