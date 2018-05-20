using UnityEngine;
using UnityEngine.Networking;

public class LazerRendererBehaviour : NetworkBehaviour
{

  public GameObject Avatar0;
  public GameObject Avatar1;

	// Use this for initialization
	void Start () {

  }
	
	// Update is called once per frame
	void Update () {
    Draw();
  }

  private void Draw()
  {
    var renderer = gameObject.GetComponent<LineRenderer>();
    renderer.SetPosition(0, Avatar0.gameObject.transform.position);
    renderer.SetPosition(1, Avatar1.gameObject.transform.position);
  }
}
