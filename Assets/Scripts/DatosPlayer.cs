using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    //[SerializeField]
    //private DatosUnidad[] _coleccionUnidades;
    [SerializeField]
    List<DatosUnidad> _equipoUnidades = new List<DatosUnidad>();


    //tb debería tener un modelo
    private GameObject _avatarModelPrefab;


    private void Init()
    {
        //debería recuperar los datos desde servidor
        int id;
        //soy el jugador con ID ? (1)
        //llamar a getEquipoUnidadesPlayer(id)


        //FALSEANDO MI EQUIPO
        DatosUnidad du = new DatosUnidad(1, "rasek", 5, 100);
        DatosUnidad du2 = new DatosUnidad(2, "rusuk", 5, 100);

        du.modelPrefabName = du.unitName;

        _equipoUnidades.Add(du);
        _equipoUnidades.Add(du2);
    }

    private void Start()
    {
        //debería recuperar los datos desde servidor
        int id;
        //soy el jugador con ID ? (1)
        //llamar a getEquipoUnidadesPlayer(id)


        //FALSEANDO MI EQUIPO
        DatosUnidad du = new DatosUnidad(1, "rasek", 5, 100);
        DatosUnidad du2 = new DatosUnidad(2, "rusuk", 5, 100);

        du.modelPrefabName = du.unitName;

        _equipoUnidades.Add(du);
        _equipoUnidades.Add(du2);
    }
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

    //public Unidad[] coleccionUnidades
    //{
    //    get
    //    {
    //        return _coleccionUnidades;
    //    }
    //    set
    //    {
    //        _coleccionUnidades = value;
    //    }
    //}

    public List<DatosUnidad> equipoUnidades
    {
        get
        {
            return _equipoUnidades;
        }
        set{
            _equipoUnidades = value;
        }
    }

    private void addUnidadEquipo(DatosUnidad unidad)
    {
        _equipoUnidades.Add(unidad);
    }

    private void removeUnidadEquipo(DatosUnidad unidad)
    {
        _equipoUnidades.Remove(unidad);
    }
}
