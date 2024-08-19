using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public enum CamType
    {
        Player,
        Boss
    }

    public class CamerasGame : CameraManager<CamType>
    {}
}
