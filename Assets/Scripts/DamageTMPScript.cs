using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTMPScript : MonoBehaviour
{
    public float disapearTime;

    private void Start()
    {
        Invoke("Destroy", disapearTime * Global.gameSpeed);
    }

    private void Destroy()
        => Destroy(gameObject);
}