using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericUnit : MonoBehaviour
{
    private int _id;
    private string _unitName;
    private int _hpMax;
    private int _hpActual;
    private bool _estoyVivo;
    
    public GenericUnit()
    {
        
    }
    public GenericUnit(int id, string name, int hp)
    {
        _id = id;
        _unitName = name;
        _estoyVivo = true;
        _hpMax = hp;
        _hpActual = _hpMax;
    }

    public int id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }
    public string unitName
    {
        get
        {
            return _unitName;
        }
        set
        {
            _unitName = value;
        }
    }

    public int hpMax
    {
        get
        {
            return _hpMax;
        }
        set
        {
            _hpMax = value;
        }
    }

    public int hpActual
    {
        get
        {
            return _hpActual;
        }
        set
        {
            _hpActual = value;
        }
    }

    public bool estoyVivo
    {
        get
        {
            return _estoyVivo;
        }
        set
        {
            _estoyVivo = value;
        }
    }


}
