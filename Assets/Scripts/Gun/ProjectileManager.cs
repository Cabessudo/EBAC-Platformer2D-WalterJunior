using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<GameObject> shoot;
    public GameObject shootToAdd;
    public float amountToAdd;

    void Start()
    {
        AddProjectiles();
    }

    void AddProjectiles()
    {
        shoot = new List<GameObject>();
        
        for(int i = 0; i < amountToAdd; i++)
        {
            GameObject obj = (GameObject)Instantiate(shootToAdd);
            shoot.Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
        }
    }

    public GameObject GetProjectile()
    {
        for(int i = 0; i < shoot.Count; i++)
        {
            if(!shoot[i].activeInHierarchy)
            {
                return shoot[i];
            }
        }
        return null;
    }
}
