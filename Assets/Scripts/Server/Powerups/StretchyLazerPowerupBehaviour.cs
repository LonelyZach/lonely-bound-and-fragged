using UnityEngine;
using System.Linq;

public class StretchyLazerPowerupBehaviour : ActivePowerupBehaviour {

  protected override void StartPowerupClient()
  {
    var lazer = FindLazerInScene();

    lazer.LineDrawWidth -= 0.05f;
  }

  protected override void EndPowerupClient()
  {
    var lazer = FindLazerInScene();

    lazer.LineDrawWidth += 0.05f;
  }

  protected override void StartPowerupServer()
  {
    var lazer = FindLazerInScene();

    var springJoint = lazer.Avatar0.GetComponent<SpringJoint2D>() ?? lazer.Avatar1.GetComponent<SpringJoint2D>();
    springJoint.distance += 2;
  }

  protected override void EndPowerupServer()
  {
    var lazer = FindLazerInScene();

    var springJoint = lazer.Avatar0.GetComponent<SpringJoint2D>() ?? lazer.Avatar1.GetComponent<SpringJoint2D>();
    springJoint.distance -= 2;
  }

  private LazerBehaviour FindLazerInScene()
  {
    return FindObjectsOfType<LazerBehaviour>().Single(x => x.Avatar0_id == ActivatingAvatarId || x.Avatar1_id == ActivatingAvatarId);
  }
}
