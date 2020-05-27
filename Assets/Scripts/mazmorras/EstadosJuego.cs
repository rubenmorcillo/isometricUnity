public static class EstadosJuego
{
    private static bool combate;
    private static bool explorar;
    private static bool tienda;
    private static bool menu;
    private static bool iniciado;


    public static void setIniciado(bool b)
    {
        iniciado = b;
       
    }
    public static bool Iniciado()
    {
        return iniciado;
    }

    public static void setCombate(bool b)
    {
        combate = b;
        explorar = !b;
        tienda = !b;
    }
    public static bool Combate()
    {
        return combate;
    }

    public static void setExplorar(bool b)
    {
        explorar = b;
        combate = !b;
        tienda = !b;
    }

    public static bool Explorar()
    {
        return explorar;
    }

    public static void setTienda(bool b)
    {
        tienda = b;
        combate = !b;
        explorar = !b;
    }

    public static bool Tienda()
    {
        return tienda;
    }
}
