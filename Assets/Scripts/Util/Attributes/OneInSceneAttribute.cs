using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
internal class OneInSceneAttribute : Attribute
{
    #region Variables

    private GameObject singleton;

    #endregion Variables

    #region Constructors

    public OneInSceneAttribute()
    {
        if (!GameObject.Find("Singleton"))
        {
            singleton = new GameObject
            {
                name = "Singleton"
            };
        }
        else
        {
            singleton = GameObject.Find("Singleton");
        }
    }

    #endregion Constructors
}