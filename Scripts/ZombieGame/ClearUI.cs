using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClearUI : MonoBehaviour {
	[SerializeField]
	Text clearText;
	public string ClearText { set { clearText.text = value; } }

	public void Tweet()
	{
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(clearText.text +"\n http://u111u.info/kRdd"));
	}
}
