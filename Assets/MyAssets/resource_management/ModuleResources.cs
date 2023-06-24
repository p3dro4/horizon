using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.resource_management
{
    [CreateAssetMenu(fileName = "ModuleResources", menuName = "ScriptableObject/ModuleResources", order = 10)]
    public class ModuleResources : ScriptableObject
    {
        [SerializeField] private int power;
        [SerializeField] private int science;
        [SerializeField] private int morkite;
        [SerializeField] private int bismor;
        [SerializeField] private int phazionite;
        [Space(10)] [SerializeField] private int initalPower;
        [SerializeField] private int initialScience;
        [SerializeField] private int initialMorkite;
        [SerializeField] private int initialBismor;
        [SerializeField] private int initialPhazionite;

        private void OnEnable()
        {
            power = initalPower;
            science = initialScience;
            morkite = initialMorkite;
            bismor = initialBismor;
            phazionite = initialPhazionite;
        }

        public int Power => power;

        public int Science => science;

        public int Morkite => morkite;

        public int Bismor => bismor;

        public int Phazionite => phazionite;

        public int GetResource(string resource)
        {
            return resource.ToUpper() switch
            {
                "POWER" => power,
                "SCIENCE" => science,
                "MORKITE" => morkite,
                "BISMOR" => bismor,
                "PHAZIONITE" => phazionite,
                _ => 0
            };
        }

        public delegate void ChangedValue();

        [SerializeField] private List<ChangedValue> _listeners = new List<ChangedValue>();

        public void AddListener(ChangedValue listener)
        {
            _listeners.Add(listener);
        }

        private void CallDelegates()
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke();
            }
        }
    }
}