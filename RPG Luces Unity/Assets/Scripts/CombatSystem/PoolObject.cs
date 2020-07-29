using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T>
{
    // Este delegate lo usaremos para llamar a las funciones correspondientes cuando el objeto sea habilitado y deshabilitado.
    public delegate void PoolCallback(T obj);

    private T _obj;
    private bool _isActive;

    // Creamos variables del tipo de nuestro delegate. Se podrán "guardar" funciones que tengan los mismos parametros que nuestro delegate.
    private PoolCallback _initCallback;
    private PoolCallback _disposeCalback;

    // En el constructor pasaremos el objeto que va a guardar, el callback que se ejecutará a la hora de ser habilitado y el callback a la hora de ser deshabilitado.
    public PoolObject(T obj, PoolCallback initCallback, PoolCallback disposeCallback)
    {
        _obj = obj;
        _initCallback = initCallback;
        _disposeCalback = disposeCallback;
        IsActive = false;
    }

    // Getter para poder conseguir el objeto que almacena esta clase. No es necesario un Setter ya que nos encargarmos únicamente en el constructor de pasarle este objeto.
    public T Obj
    {
        get
        {
            return _obj;
        }
    }

    // Getter y Setter para poder preguntar y asignar el valor de _isActive.
    public bool IsActive
    {
        get
        {
            return _isActive; // Retornamos si está activo o no.
        }
        set
        {
            _isActive = value;

            // Además de cambiar el valor de _isActive, dependiendo su valor llamaremos al callback correspondiente.
            if (_isActive)
            {
                _initCallback(_obj);
            }
            else
            {
                _disposeCalback(_obj);

            }
        }
    }
}
