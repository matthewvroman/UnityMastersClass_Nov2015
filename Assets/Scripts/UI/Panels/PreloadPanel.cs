using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreloadPanel : MonoBehaviour {

	[SerializeField] private string m_sceneNameToLoad;
	[SerializeField] private GameObject m_fillObject;
	[SerializeField] private GameObject[] m_objectsToDestroy;

	private void OnEnable()
	{
		LoadManager.OnLoadProgress += HandleOnLoadProgress;
		LoadManager.OnLoadComplete += HandleOnLoadComplete;

		SetProgress (0.0f);
	}

	private void OnDisable()
	{
		LoadManager.OnLoadProgress -= HandleOnLoadProgress;
		LoadManager.OnLoadComplete -= HandleOnLoadComplete;
	}

	private void Awake()
	{
		LoadManager.Instance.LoadAdditiveAsync (m_sceneNameToLoad);
	}


	private void HandleOnLoadProgress (float progress)
	{
		SetProgress (progress);
	}

	private void HandleOnLoadComplete ()
	{
		int numObjects = m_objectsToDestroy.Length;
		for (int i=0; i<numObjects; i++) 
		{
			GameObject.DestroyImmediate(m_objectsToDestroy[i]);
		}
	}

	private void SetProgress(float progress)
	{
		Vector3 scale = m_fillObject.transform.localScale;
		scale.x = progress;
		m_fillObject.transform.localScale = scale;
	}
}
