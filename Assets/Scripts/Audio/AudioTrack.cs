using UnityEngine;
using System.Collections;

public class AudioTrack {

	public enum ChannelType
	{
		Sfx,
		Music
	}

	private ChannelType m_type;
	public ChannelType Type
	{
		get
		{
			return m_type; 
		}
		set
		{
			m_type = value;
		}
	}

	private AudioSource m_source;
	public AudioSource Source
	{
		get
		{
			return m_source;
		}
		set
		{
			m_source = value;
		}
	}

	private float m_volume;

	public AudioTrack(ChannelType channelType)
	{
		m_type = channelType;
		m_source = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
	}

	public void PlayAudioClip(AudioClip audioClip, float volume, bool loop = false)
	{
		m_volume = volume;
		m_source.clip = audioClip;
		m_source.loop = loop;
		m_source.volume = volume;
		m_source.Play();
	}

	public void Mute()
	{
		m_source.volume = 0;
	}

	public void Unmute()
	{
		m_source.volume = m_volume;
	}
	
}
