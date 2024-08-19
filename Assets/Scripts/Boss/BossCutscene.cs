using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Camera;
public class BossCutscene : MonoBehaviour
{
    public BossBase boss;
    public CamerasCutscene cutsceneCam;
    public CamerasGame mainCams;
    public ShakeCamera shakeCam;
    public float waitToShake;
    public float timeToStop;
    public bool cutsceneOn;

    public void Cutscene()
    {
        cutsceneOn = true;
        StartCoroutine(CutsceneRoutine());
    }

    public IEnumerator CutsceneRoutine()
    {
        Player.Instance.soPlayerSetup.cutscene = true;
        mainCams.TurnOffAllCams();
        cutsceneCam.ChangeCamByType(CutsceneType.Boss_Cutscene);
        yield return new WaitForSeconds(waitToShake);
        boss.anim.GetAnimByType(BossAnimType.Angry);
        shakeCam.Shake();
        yield return new WaitForSeconds(timeToStop);
        cutsceneCam.TurnOffAllCams();
        mainCams.ChangeCamByType(CamType.Boss);
        cutsceneOn = false;
        Player.Instance.soPlayerSetup.cutscene = false;
    }
}
