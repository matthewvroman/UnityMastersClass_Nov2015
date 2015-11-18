using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static string HashTableKey = "AudioManager";

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float m_defaultMusicVolume;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float m_defaultSfxVolume;

	private List<AudioTrack> m_tracks = new List<AudioTrack>(); 

	[SerializeField]
	private List<AudioClip> m_audioClips = new List<AudioClip>();

	private List<AudioTrack.ChannelType>m_mutedChannels = new List<AudioTrack.ChannelType>();

	private static AudioManager m_instance;
	public static AudioManager Instance
	{
		get
		{
			if(m_instance == null)
			{
				m_instance = GameObject.Find("AudioManager").GetComponent<AudioManager>();
			}
			return m_instance;
		}
	}

	private void Awake()
	{
#if UNITY_EDITOR && UNITY_IPHONE
		// Editor only for Frank's sanity
		m_defaultMusicVolume = 0.0f;
		m_defaultSfxVolume = 0.0f;
#endif

		if(m_instance != null && m_instance != this)
		{
			Debug.LogWarning("Only one AudioManager can exist at a time. Destroying this one");
			GameObject.Destroy(this.gameObject);
			return;
		}

		m_instance = this;
	}

	public void PlaySfx(string clipName, float volume = 1.0f)
	{
		AudioTrack audioTrack = GetFreeAudioTrack(AudioTrack.ChannelType.Sfx);
		AudioClip audioClip = GetAudioClipByName(clipName);
		audioTrack.PlayAudioClip(audioClip, m_defaultSfxVolume * volume);

		if(IsChannelMuted(AudioTrack.ChannelType.Sfx))
		{
			audioTrack.Mute();
		}
	}

	public void PlayMusic(string clipName, float volume = 1.0f, bool loop = true)
	{
		AudioTrack audioTrack = GetFirstAudioTrack(AudioTrack.ChannelType.Music);
		if(audioTrack.Source.clip != null && audioTrack.Source.clip.name == clipName) return;
		AudioClip audioClip = GetAudioClipByName(clipName);
		audioTrack.PlayAudioClip(audioClip, m_defaultMusicVolume * volume, loop); 

		if(IsChannelMuted(AudioTrack.ChannelType.Music))
		{
			audioTrack.Mute();
		}
	}

	private AudioTrack GetFreeAudioTrack(AudioTrack.ChannelType channelType)
	{
		AudioTrack track;
		int numTracks = m_tracks.Count;
		for(int i=0; i<numTracks; i++)
		{
			track = m_tracks[i];
			if(track.Type == channelType && !track.Source.isPlaying)
			{
				return track;
			}
		}

		track = new AudioTrack(channelType);
		m_tracks.Add(track);
		return track;
	}

	private AudioTrack GetFirstAudioTrack(AudioTrack.ChannelType channelType)
	{
		AudioTrack track;
		int numTracks = m_tracks.Count;
		for(int i=0; i<numTracks; i++)
		{
			track = m_tracks[i];
			if(track.Type == channelType)
			{
				return track;
			}
		}
		
		track = new AudioTrack(channelType);
		m_tracks.Add(track);
		return track;
	}

	private AudioClip GetAudioClipByName(string clipName)
	{
		if(clipName == "" || clipName == string.Empty) return null;

		int numClips = m_audioClips.Count;
		for(int i=0; i<numClips; i++)
		{
			AudioClip clip = m_audioClips[i];
			if(clip.name == clipName)
			{
				return clip;
			}
		}

		Debug.LogError("Couldn't find AudioClip with name '" + clipName + "'. Please make sure it's added to the AudioManager's Audio Clip list.");

		return null;
	}

	public void MuteAllChannels()
	{
		int numChannels = System.Enum.GetNames(typeof(AudioTrack.ChannelType)).Length;
		for(int i=0; i<numChannels; i++)
		{
			AudioTrack.ChannelType channelType = (AudioTrack.ChannelType)i;
			MuteChannel(channelType);
		}
	}

	public void UnmuteAllChannels()
	{
		int numChannels = System.Enum.GetNames(typeof(AudioTrack.ChannelType)).Length;
		for(int i=0; i<numChannels; i++)
		{
			AudioTrack.ChannelType channelType = (AudioTrack.ChannelType)i;
			UnmuteChannel(channelType);
		}
	}

	public void MuteChannel(AudioTrack.ChannelType channelType)
	{
		if(m_mutedChannels.IndexOf(channelType) != -1) return;

		int numTracks = m_tracks.Count;
		for(int i=0; i<numTracks; i++)
		{
			AudioTrack audioTrack = m_tracks[i];
			if(audioTrack.Type == channelType)
			{
				audioTrack.Mute();
			}
		}

		m_mutedChannels.Add(channelType);
	}

	public void UnmuteChannel(AudioTrack.ChannelType channelType)
	{
		if(m_mutedChannels.IndexOf(channelType) == -1) return;

		int numTracks = m_tracks.Count;
		for(int i=0; i<numTracks; i++)
		{
			AudioTrack audioTrack = m_tracks[i];
			if(audioTrack.Type == channelType)
			{
				audioTrack.Unmute();
			}
		}

		m_mutedChannels.Remove(channelType);
	}

	public void ToggleChannelMute(AudioTrack.ChannelType channelType)
	{
		if(IsChannelMuted(channelType))
		{
			UnmuteChannel(channelType);
		}
		else
		{
			MuteChannel(channelType);
		}
	}

	public bool IsChannelMuted(AudioTrack.ChannelType channelType)
	{
		return m_mutedChannels.IndexOf(channelType) != -1;
	}

	public bool AreAllChannelsMuted()
	{
		int numChannels = System.Enum.GetNames(typeof(AudioTrack.ChannelType)).Length;
		for(int i=0; i<numChannels; i++)
		{
			AudioTrack.ChannelType channelType = (AudioTrack.ChannelType)i;
			if(m_mutedChannels.IndexOf(channelType) == -1)
			{
				return false;
			}
		}

		return true;
	}

	//Saves muted channels to Hashtable
	public Hashtable WriteToHashTable()
	{
		Hashtable hashtable = new Hashtable();
		int numChannels = System.Enum.GetNames(typeof(AudioTrack.ChannelType)).Length;
		for(int i=0; i<numChannels; i++)
		{
			AudioTrack.ChannelType channel = (AudioTrack.ChannelType)i;
			string key = i.ToString();
			hashtable.Add(key, IsChannelMuted(channel));
		}

		return hashtable;
	}

	//Reads muted channels from Hashtable
	public void ReadFromHashTable(Hashtable hashtable)
	{
		int numChannels = System.Enum.GetNames(typeof(AudioTrack.ChannelType)).Length;
		for(int i=0; i<numChannels; i++)
		{
			string key = i.ToString();
			AudioTrack.ChannelType channel = (AudioTrack.ChannelType)i;
			if(hashtable.ContainsKey(key))
			{
				bool muted = (bool)hashtable[key];
				if(muted)
				{
					MuteChannel(channel);
				}
			}
		}
	}

}
