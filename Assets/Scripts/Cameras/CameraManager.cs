using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

namespace Camera
{
   
    
    public class CameraManager<T> : MonoBehaviour where T : System.Enum
    {

        public List<CameraSetup> camSetup;

        public void ChangeCamByType(T currType)
        {
            camSetup.ForEach(i => i.cam.SetActive(false));

            var currSetup = camSetup.Find(i => i.type.ToString() == currType.ToString());
            currSetup.cam.SetActive(true);
        }

        public void TurnOffAllCams()
        {
            camSetup.ForEach(i => i.cam.SetActive(false));
        }

        [System.Serializable]
        public class CameraSetup
        {
            public T type;
            public GameObject cam;
        }
    }
}
