using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayPanel : MonoBehaviour {

	[SerializeField] private Button m_exitButton;

	private void Awake()
	{
		m_exitButton.onClick.AddListener (() => { OnExitButtonClicked ();});
	}

	private void OnExitButtonClicked()
	{
		PanelManager.Instance.SwitchPanel (this.gameObject, "TitlePanel");
		LoadManager.Instance.LoadAsync ("EmptyScene");
		System.GC.Collect ();
	}
}
