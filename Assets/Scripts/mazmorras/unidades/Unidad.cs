using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : GenericUnit
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (this.hpActual <= 0)
        {
            //he morido
            this.estoyVivo = false;
        }
    }
}
