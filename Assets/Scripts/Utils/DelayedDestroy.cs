using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {

	[SerializeField] private float m_delay = 1.0f;

	public static void Create(GameObject gameObject, float delay)
	{
		DelayedDestroy delayedDestroy = gameObject.AddComponent<DelayedDestroy> ();
		delayedDestroy.m_delay = delay;
	}

	private void Start()
	{
		StartCoroutine (DestroyAfterDelay ());
	}

	private IEnumerator DestroyAfterDelay()
	{
		yield return new WaitForSeconds(m_delay);

		GameObject.Destroy(this.gameObject);
	}

}
