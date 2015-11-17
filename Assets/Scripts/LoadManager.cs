using UnityEngine;
using System.Collections;

public class LoadManager : MonoBehaviour {

	public delegate void LoadManagerProgressHandler(float progress);
	public static event LoadManagerProgressHandler OnLoadProgress;

	public delegate void LoadManagerEventHandler ();
	public static event LoadManagerEventHandler OnLoadStart;
	public static event LoadManagerEventHandler OnLoadComplete;

	private static LoadManager s_instance;
	public static LoadManager Instance
	{
		get
		{
			if (s_instance == null) {
				GameObject gameObject = new GameObject("LoadManager");
				s_instance = gameObject.AddComponent<LoadManager>();
			}
			return s_instance;
		}
	}

	private bool m_isLoading;
	private AsyncOperation m_asyncOperation;

	public LoadManager()
	{
		GameObject.DontDestroyOnLoad (this.gameObject);
	}

	public void LoadAdditiveAsync(string sceneName)
	{
		if (m_isLoading)
			return;

		m_asyncOperation = Application.LoadLevelAdditiveAsync(sceneName);

		m_isLoading = true;

		if (OnLoadStart != null) 
		{
			OnLoadStart();
		}
	}

	public void LoadAsync(string sceneName)
	{
		if (m_isLoading)
			return;

		m_asyncOperation = Application.LoadLevelAsync (sceneName);

		m_isLoading = true;

		if (OnLoadStart != null) 
		{
			OnLoadStart();
		}
	}

	private void Update()
	{
		if (m_isLoading && m_asyncOperation != null) {

			if(OnLoadProgress != null)
			{
				OnLoadProgress(m_asyncOperation.progress);
			}

			if(m_asyncOperation.isDone)
			{
				if(OnLoadComplete != null)
				{
					OnLoadComplete();
				}
				m_asyncOperation = null;
				m_isLoading = false;
			}
		}
	}

}
