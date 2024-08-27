using UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalUI : MonoBehaviour
{
	[SerializeField] private HUD hud;

	[SerializeField] private Canvas _canvas;
	public HUD HUD => hud;

	private void Awake()
	{
		SceneManager.activeSceneChanged += ActiveSceneChangedHandler;
	}

	public void SetActiveHud(bool isActive)
	{
		hud.gameObject.SetActive(isActive);
	}

	public void ActiveSceneChangedHandler(Scene previous, Scene current)
	{
		_canvas.worldCamera = Camera.main;
	}
}