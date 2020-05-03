using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnos : MonoBehaviour
{

    static Dictionary<string, List<MovimientoCasillas>> units = new Dictionary<string, List<MovimientoCasillas>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<MovimientoCasillas> turnTeam = new Queue<MovimientoCasillas>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (turnTeam.Count == 0)
        {
            //el primer round inicializa aquí...
            InitTeamTurnQueue();
        }   
    }

    static void InitTeamTurnQueue()
    {
        List<MovimientoCasillas> teamList = units[turnKey.Peek()];

        foreach (MovimientoCasillas unit in teamList)
        {
            turnTeam.Enqueue(unit);

        }

        StartTurn();
    }

    static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }

    }

    public static void EndTurn()
    {
        //final del turno del EQUIPO
        MovimientoCasillas unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    //cada unidad se añade a si misma a la lista
    public static void AddUnit(MovimientoCasillas unit)
    {
        List<MovimientoCasillas> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<MovimientoCasillas>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }


}
