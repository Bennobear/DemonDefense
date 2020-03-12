using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
//Class to create a nice looking screne fade between scenes

public class SceneFader : MonoBehaviour
{
	public Image img;
	public AnimationCurve curve;
	//When we enter a scene this creates a nice fade
	void Start()
	{
		StartCoroutine(FadeIn());
	}
	//Fade to another scene
	public void FadeTo(string scene)
	{
		StartCoroutine(FadeOut(scene));
	}
	//The Coroutine that creates the fade in effect 
	IEnumerator FadeIn()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
	}
	//The Coroutine that creates the fade out effect 
	IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
		SceneManager.LoadScene(scene);
	}
}