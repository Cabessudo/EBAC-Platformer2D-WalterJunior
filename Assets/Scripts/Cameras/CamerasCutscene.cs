using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Camera
{
    public enum CutsceneType
    {
        Boss_Cutscene,
        Fairy_Cutscene
    }

    public class CamerasCutscene : CameraManager<CutsceneType>
    {}
}
