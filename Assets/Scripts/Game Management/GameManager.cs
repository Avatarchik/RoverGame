using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sol
{
    public class GameManager : MonoBehaviour
    {
        protected Dictionary<System.Type, object> singletons = new Dictionary<System.Type, object>();

        protected static GameManager instance;

        public static T As<T>() where T : GameManager
        {
            return Exists ? Instance as T : null;
        }

        public static bool Exists
        {
            get { return Instance != null; }
        }

        ///<summary>
        ///Retrieve Game Manager singleton
        ///</summary>
        public static GameManager Instance
        {
            get
            {
                if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null) instance = GameObject.Find("_GameManager").GetComponent<GameManager>();

                return instance;
            }
        }

        ///<summary>
        ///Gt existing object of type <T>
        ///</summary>
        public static T Get<T>() where T : class
        {
            T singleton = default(T);

            if (Exists)
            {
                System.Type type = typeof(T);
                singleton = Instance.singletons.ContainsKey(type) ? (T)Instance.singletons[type] : null;

                if (singleton == null)
                {
                    singleton = Instance.singletons.Values.FirstOrDefault(i => i is T) as T;

                    if (singleton == null)
                    {
                        Component[] components = Instance.gameObject.GetComponentsInChildren(type, true);

                        if (components.Length > 0) singleton = components[0] as T;
                    }

                    if (singleton != null) Set<T>(singleton);
                }
            }

            return singleton;
        }

        ///<summary>
        ///Set existing singleton of type <T> to new singleton instance.
        ///</summary>
        public static void Set<T>(T singleton) where T : class
        {
            if (Exists) Instance.singletons[typeof(T)] = singleton;
        }

        ///<summary>
        ///Remove cached singleton of type <T>
        ///</summary>
        public static void Remove<T>(T singleton) where T : class
        {
            if (Exists && Instance.singletons.ContainsKey(typeof(T)) && Instance.singletons[typeof(T)] == singleton)
            {
                Instance.singletons.Remove(typeof(T));
            }
        }
    }
}