using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LoadingPanel : MonoBehaviour {

	[SerializeField] private GameObject m_fillObject;

	private Action m_onComplete;

	public static void LoadAdditive(string sceneName, Action onComplete)
	{
		LoadingPanel panel = PanelManager.Instance.GetPanel ("LoadingPanel").GetComponent<LoadingPanel> ();
		panel.m_onComplete = onComplete;
		PanelManager.Instance.AddPanel (panel.gameObject);

		LoadManager.Instance.LoadAdditiveAsync (sceneName);
	}

	private void OnEnable()
	{
		LoadManager.OnLoadProgress += HandleOnLoadProgress;
		LoadManager.OnLoadComplete += HandleOnLoadComplete;

		SetProgress (0.0f);
	}

	private void HandleOnLoadComplete ()
	{
		if (m_onComplete != null) {
			m_onComplete();
		}
		PanelManager.Instance.RemovePanel (this.gameObject);
	}

	private void OnDisable()
	{
		LoadManager.OnLoadProgress -= HandleOnLoadProgress;
		LoadManager.OnLoadComplete -= HandleOnLoadComplete;
	}

	private void HandleOnLoadProgress (float progress)
	{
		SetProgress (progress);
	}

	private void SetProgress(float progress)
	{
		Vector3 scale = m_fillObject.transform.localScale;
		scale.x = progress;
		m_fillObject.transform.localScale = scale;
	}

}
