using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    bool isSplatCreated = false;
    public void SetPos(Vector3 pos)
    {
        if(isSplatCreated)return;

        transform.position = pos;
        isSplatCreated = true;
    }

}
