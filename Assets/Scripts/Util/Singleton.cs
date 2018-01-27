using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util
{
    internal class Singleton : MonoBehaviour
    {
        #region Variables

        //List of types of which there can be one and only one in each scene
        private List<Type> singletons = new List<Type>();

        #endregion Variables

        #region Unity Functions

        private void Start()
        {
            //Get Types from project
            List<Type> classes = new List<Type>(System.Reflection.Assembly.GetExecutingAssembly().GetTypes());

            //Loop through them
            foreach (Type type in classes)
            {
                //and get their attributes
                List<object> attributes = new List<object>(type.GetCustomAttributes(false));

                //If they have the OneInScene attribute
                if (attributes.Contains(typeof(OneInSceneAttribute)))
                {
                    //Add it to the singleton list
                    singletons.Add(type);
                }
            }

            //Now go through the singletons
            foreach (Type type in singletons)
            {
                List<GameObject> objects = ManualConvertToList(FindObjectsOfType(type));
                if (objects.Count > 1)
                {
                    throw new Exception("There must be only " + type.Name + " in the scene at any time");
                }
            }

            Destroy(gameObject);
        }

        #endregion Unity Functions

        #region Helper Functions

        private List<GameObject> ManualConvertToList(UnityEngine.Object[] @object)
        {
            List<GameObject> output = new List<GameObject>();
            foreach (UnityEngine.Object miniobject in @object)
            {
                output.Add((GameObject)miniobject);
            }
            return output;
        }

        #endregion Helper Functions
    }
}