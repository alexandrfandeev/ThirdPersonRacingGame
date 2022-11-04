using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Core.SignalBus
{
    public class Signal
    {
        public static Signal Current { get; private set; }
        
        private readonly List<SignalData> _signals = new List<SignalData>();

        public static void Initialize()
        {
            Current = new Signal();
        }
        
        public Signal()
        {
            MonoBehaviour[] monoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();
            foreach (MonoBehaviour component in monoBehaviours)
            {
                SubObject(component);
            }
        }

        public void SubObject(MonoBehaviour component)
        {
            IEnumerable<MethodInfo> methods = component.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(method => Attribute.IsDefined(method, typeof(Sub)));
            foreach (MethodInfo method in methods)
            {
                Type type = method.GetParameters()[0].ParameterType;
                _signals.Add(new SignalData(method, component, type));
            }
        }

        public void Sub<T>(MonoBehaviour target, Action<T> action)
        {
            MethodInfo method = action.Method;
            Type type = method.GetParameters()[0].ParameterType;
            _signals.Add(new SignalData(method, target, type));
        }

        public void Fire<T>(object data)
        {
            foreach (SignalData signal in _signals)
            {
                if (signal.Type == typeof(T))
                {
                    signal.Method.Invoke(signal.Target, new[] { data });
                }
            }
        }
    }

    public class SignalData
    {
        public MethodInfo Method { get; }
        public MonoBehaviour Target { get; }
        public Type Type { get; }

        public SignalData(MethodInfo method, MonoBehaviour target, Type type)
        {
            Method = method;
            Target = target;
            Type = type;
        }
    }
}