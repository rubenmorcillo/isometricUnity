﻿using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosUnidad
{

    private int _hpMax;

    public DatosUnidad()
    {

    }
    public DatosUnidad(int id, string name, int hp)
    {
        this.id = id;
        unitName = name;
        estoyVivo = true;
        hpMax = hp;
        hpActual = _hpMax;
    }

    public int id { get; set; }
    
    public string unitName { set; get; }

    public int hpMax { get; set; }

    public int hpActual { get; set; }

    public bool estoyVivo { get; set; }

    public string modelPrefabName { get; set; }

    public int rangoMovimiento { get; set; }
    public bool isPlaced { get; set; }
   

    
    //creo que estas cosas las debo controlar en el combateManager

    //void Update()
    //{
    //    if (this.hpActual <= 0)
    //    {
    //        //he morido
    //        this.estoyVivo = false;
    //    }
    //}
}