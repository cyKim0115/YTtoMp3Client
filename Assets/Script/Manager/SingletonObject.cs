
    using UnityEngine;

    public class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject();
                    go.transform.position = Vector3.zero;
                    _instance = go.AddComponent<T>();
                    go.name = _instance.GetType().Name;
                }

                return _instance;
            }
        }
    }