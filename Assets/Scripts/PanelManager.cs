using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {

	private static PanelManager s_instance;
	public static PanelManager Instance
	{
		get
		{
			if(s_instance == null)
			{
				s_instance = GameObject.Find("PanelManager").GetComponent<PanelManager>();
			}
			return s_instance;
		}
	}

	[SerializeField] private List<GameObject>m_managedPanels;
	[SerializeField] private GameObject m_initialPanel;

	private void Awake()
	{
		if (s_instance == null) 
		{
			s_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else 
		{
			Debug.LogError("There can be only one PanelManager");
		}

		int numPanels = m_managedPanels.Count;
		for (int i=0; i<numPanels; i++) 
		{
			GameObject panel = m_managedPanels[i];
			if(panel == m_initialPanel)
			{
				AddPanel(panel);
			}
			else
			{
				RemovePanel(panel);
			}
		}

		GameManager.Instance.Initialize ();

	}

	public void AddPanel(GameObject gameObject)
	{
		gameObject.SetActive (true);
	}

	public void AddPanel(string panelName)
	{
		GameObject panel = GetPanel (panelName);
		AddPanel(panel);
	}

	public void RemovePanel(GameObject gameObject)
	{
		gameObject.SetActive (false);
	}

	public void RemovePanel(string panelName)
	{
		GameObject panel = GetPanel (panelName);
		RemovePanel(panel);
	}

	public void SwitchPanel(string fromPanel, string toPanel)
	{
		SwitchPanel (GetPanel (fromPanel), GetPanel (toPanel));
	}

	public void SwitchPanel(GameObject fromPanel, string toPanel)
	{
		SwitchPanel (fromPanel, GetPanel (toPanel));
	}

	public void SwitchPanel(GameObject fromPanel, GameObject toPanel)
	{
		RemovePanel (fromPanel);
		AddPanel (toPanel);
	}

	public GameObject GetPanel(string panelName)
	{
		int numPanels = m_managedPanels.Count;
		for (int i=0; i<numPanels; i++) 
		{
			if(m_managedPanels[i].name == panelName)
			{
				return m_managedPanels[i];
			}
		}
		return null;
	}


}
