using HHG.Common.Runtime;
using UnityEngine;

namespace HHG.SkillCheckSystem.Runtime
{
    [System.Serializable]
    public partial class SkillCheck : ISkillCheck
    {
        public string Header => header;
        public string Description => description;
        public int Difficulty => difficulty;
        public Dice Dice => dice;

        [SerializeField] private string header;
        [SerializeField, TextArea] private string description;
        [SerializeField] private int difficulty;
        [SerializeField] private Dice dice;
    }
}