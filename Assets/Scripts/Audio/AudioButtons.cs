using AudioManager.Runtime.Core.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
	public class AudioButtons : MonoBehaviour
	{
		[Header("Music")] [SerializeField] private Toggle Music;
		[SerializeField] private Image targetMusic;
		[SerializeField] private Sprite targetMusicSpriteEnabled;
		[SerializeField] private Sprite targetMusicSpriteDisabled;

		[Header("Music")] [SerializeField] private Toggle Sound;
		[SerializeField] private Image targetSound;
		[SerializeField] private Sprite targetSoundSpriteEnabled;
		[SerializeField] private Sprite targetSoundSpriteDisabled;

		private void Awake()
		{
			var manager = ManagerAudio.SharedInstance;
			var musicIsOn = manager.GetConfig().IfMusicEnabled();
			Music.isOn = musicIsOn;
			Music.onValueChanged.AddListener(OnMusicClick);
			UpdateMusicInterface(musicIsOn);

			var soundsIsOn = manager.GetConfig().IfSoundEnabled();
			Sound.isOn = soundsIsOn;
			Sound.onValueChanged.AddListener(OnSoundClick);
			UpdateSoundInterface(soundsIsOn);
		}

		private void OnSoundClick(bool state)
		{
			var manager = ManagerAudio.SharedInstance;
			manager.PlayAudioClip(TAudio.Plop.ToString());
			manager.GetConfig().SetSoundEnabledState(Sound.isOn);
			UpdateSoundInterface(state);
		}

		private void OnMusicClick(bool state)
		{
			var manager = ManagerAudio.SharedInstance;
			manager.PlayAudioClip(TAudio.Plop.ToString());
			manager.GetConfig().SetMusicEnabledState(Music.isOn);
			UpdateMusicInterface(state);
		}

		private void UpdateMusicInterface(bool state)
		{
			if (targetMusic != null)
			{
				targetMusic.sprite = state ? targetMusicSpriteEnabled : targetMusicSpriteDisabled;
			}
		}

		private void UpdateSoundInterface(bool state)
		{
			if (targetSound != null)
			{
				targetSound.sprite = state ? targetSoundSpriteEnabled : targetSoundSpriteDisabled;
			}
		}
	}
}