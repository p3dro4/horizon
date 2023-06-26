using System;
using UnityEngine;

namespace MyAssets.base_building.research
{
    [CreateAssetMenu(fileName = "ResearchState", menuName = "ScriptableObject/ResearchState", order = 25)]
    public class ResearchState : ScriptableObject
    {
        [Header("Research Module")] [SerializeField]
        private int researchModuleLvl = 0;

        [SerializeField] private int researchModuleLvlMax = 3;

        [Space(10)] [Header("Material Processing Module")] [SerializeField]
        private int materialProcessingModuleLvl = 0;

        [SerializeField] private int materialProcessingModuleLvlMax = 3;

        [Space(10)] [Header("Generator Module")] [SerializeField]
        private int generatorModuleLvl = 0;

        [SerializeField] private int generatorModuleLvlMax = 3;

        [Space(10)] [Header("Relay Module")] [SerializeField]
        private int relayModuleLvl = 0;

        [SerializeField] private int relayModuleLvlMax = 3;

        [Space(20)] [SerializeField] private float lvlBonus = 0.15f;
        [SerializeField] private float baseCost = 25;

        [SerializeField] private bool reset = false;

        private void OnEnable()
        {
            if (!reset) return;
            ResetDefaults();
            reset = false;
        }

        public int ResearchModuleLvl
        {
            get => researchModuleLvl;
            set => researchModuleLvl = value;
        }

        public int ResearchModuleLvlMax
        {
            get => researchModuleLvlMax;
            set => researchModuleLvlMax = value;
        }

        public int MaterialProcessingModuleLvl
        {
            get => materialProcessingModuleLvl;
            set => materialProcessingModuleLvl = value;
        }

        public int MaterialProcessingModuleLvlMax
        {
            get => materialProcessingModuleLvlMax;
            set => materialProcessingModuleLvlMax = value;
        }

        public int GeneratorModuleLvl
        {
            get => generatorModuleLvl;
            set => generatorModuleLvl = value;
        }

        public int GeneratorModuleLvlMax
        {
            get => generatorModuleLvlMax;
            set => generatorModuleLvlMax = value;
        }

        public int RelayModuleLvl
        {
            get => relayModuleLvl;
            set => relayModuleLvl = value;
        }

        public int RelayModuleLvlMax
        {
            get => relayModuleLvlMax;
            set => relayModuleLvlMax = value;
        }

        public float LvlBonus => lvlBonus;

        public float BaseCost => baseCost;

        public void ResetDefaults()
        {
            researchModuleLvl = 0;
            materialProcessingModuleLvl = 0;
            generatorModuleLvl = 0;
            relayModuleLvl = 0;
        }

        public int GetModuleLvl(int index)
        {
            return index switch
            {
                0 => researchModuleLvl,
                1 => materialProcessingModuleLvl,
                2 => generatorModuleLvl,
                3 => relayModuleLvl,
                _ => -1
            };
        }
    }
}