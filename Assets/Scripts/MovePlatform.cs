using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    
    [SerializeField] private Transform destino;
    [SerializeField] private float velocidad;

    private Vector3 posIni, posFin;

    void Start()
    {
        destino.parent = null;
        posIni = transform.position;
        posFin = destino.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);
        if(transform.position == destino.position)
        {
            destino.position = (destino.position == posFin) ? posIni : posFin;
        }
    }
}
