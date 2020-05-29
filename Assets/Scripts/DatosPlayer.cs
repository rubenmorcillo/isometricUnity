using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosPlayer : MonoBehaviour
{
    //cuenta
    [SerializeField]
    private string _nickname;

    //juego
    [SerializeField]
    private int _dinero;
    [SerializeField]
    private int _reputacion;
    [SerializeField]
    private Unidad[] _coleccionUnidades;

    //tb debería tener un modelo
    private GameObject _avatarModelPrefab;
    
   public string nickname
    {
        get
        {
            return _nickname;
        }
        set
        {
            _nickname = value;
        }
    }

    public int dinero
    {
        get
        {
            return _dinero;
        }
        set
        {
            _dinero = value;
        }
    }

    public int reputacion
    {
        get
        {
            return _reputacion;
        }
        set
        {
            _reputacion = value;
        }
    }

    public Unidad[] coleccionUnidades
    {
        get
        {
            return _coleccionUnidades;
        }
    }
}
