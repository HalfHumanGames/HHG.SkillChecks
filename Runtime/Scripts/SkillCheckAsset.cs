using HHG.Common.Runtime;
using UnityEngine;

namespace HHG.SkillCheckSystem.Runtime
{
    [CreateAssetMenu(fileName = "Style Name", menuName = "HHG/Skill Check System/Skill Check")]
    public class SkillCheckAsset : ScriptableObject, ISkillCheck
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