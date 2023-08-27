using UnityEngine;

namespace Test.Architecture
{
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject o = new GameObject(typeof(T).Name);
					_instance = o.AddComponent<T>();
					DontDestroyOnLoad(o);
				}

				return _instance;
			}
		}
	}
}