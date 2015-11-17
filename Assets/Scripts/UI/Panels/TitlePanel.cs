using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitlePanel : MonoBehaviour {

	[SerializeField] private Button m_playButton;

	private void Awake()
	{
		//m_playButton.onClick.AddListener (OnPlayButtonClicked);
		m_playButton.onClick.AddListener (() => { OnPlayButtonClicked(); });
	}

	private void OnPlayButtonClicked()
	{


		LoadingPanel.LoadAdditive ("GameplayScene", ()=>{ PanelManager.Instance.SwitchPanel (this.gameObject, "GameplayPanel"); });
	}
}
