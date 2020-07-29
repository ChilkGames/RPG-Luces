using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
{
    // Creamos un delegate. Los delegate son la versión de C# de hacer callbacks. 
    // Básicamente las trataremos como variables en las que se pueden "guardar" funciones y al momento de ejecutar el delegate se llamaran a todas las funciones que hayan sido ingresadas
    // Este delegate lo usaremos para llamar a las funciones correspondientes que se encarguen de crear los objetos para la pool.
    public delegate T CallbackFactory();

    // Haremos una lista de PoolObject para guardarnos todos los objetos de nuestra Pool.
    private List<PoolObject<T>> _poolList;

    // Los delegates de los PoolObjects serán pasados a través del Constructor de la Pool.
    private PoolObject<T>.PoolCallback _init;
    private PoolObject<T>.PoolCallback _dispose;

    // El delegate que se encargará de crear los objetos que van en la pool.
    private CallbackFactory _factoryMethod;

    //Si nuestra pool se expandirá automaticamente cuando no le queden objetos en la pool para usar.
    private bool _isDinamic;

    //Cuantos elementos tiene nuestra pool.
    private int _count;

    // En el constructor pasaremos los valores de nuestras variables privadas.
    public Pool(CallbackFactory factoryMethod, PoolObject<T>.PoolCallback init, PoolObject<T>.PoolCallback dispose, int initialStock, bool isDinamic = true)
    {
        _poolList = new List<PoolObject<T>>();

        _factoryMethod = factoryMethod;
        _init = init;
        _dispose = dispose;
        _count = initialStock;
        _isDinamic = isDinamic;

        for (int i = 0; i < _count; i++)
        {
            // Creamos los objetos de nuestro pool de objetos.
            _poolList.Add(new PoolObject<T>(_factoryMethod(), _init, _dispose));
        }
    }

    public T GetObjectFromPool()
    {
        //Recorremos nuestra lista de pool de objetos.
        for (int i = 0; i < _poolList.Count; i++)
        {
            var poolObject = _poolList[i];
            // Si no está activo es porque lo podemos utilizar.
            if (!poolObject.IsActive)
            {
                //Lo marcamos como activo y lo devolvemos para su uso.
                poolObject.IsActive = true;
                return poolObject.Obj; // El return corta la ejecución de la función.
            }
        }

        //Si llegamos hasta acá es porque todos los objetos se estaban utilizando.

        //Si nuestra pool es dinamico, creamos un nuevo Pool Object
        if (_isDinamic)
        {
            var newPoolObject = new PoolObject<T>(_factoryMethod(), _init, _dispose);
            newPoolObject.IsActive = true;
            _poolList.Add(newPoolObject);
            _count++;
            return newPoolObject.Obj;
        }

        //Si llegamos hasta acá es porque no tenemos ningún objeto que devolver. Devolvemos el nulo del tipo de dato que estemos pooleando.
        return default(T);
    }

    public void DisablePoolObject(T obj)
    {
        // Recorremos la lista
        for (int i = 0; i < _poolList.Count; i++)
        {
            var poolObject = _poolList[i];

            //Si es el objeto que estamos pasando por parametro, lo deshabilitamos.
            if (poolObject.Obj.Equals(obj))
            {
                poolObject.IsActive = false;
                return;
            }
        }
    }
}
