using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string nombre;
    public int cantidad;
    public Sprite icono;

    public Item(string nombre,Sprite icono)
    {
        this.nombre = nombre;
        this.icono = icono;
        this.cantidad = 0;
    }
}
