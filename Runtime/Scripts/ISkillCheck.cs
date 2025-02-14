using HHG.Common.Runtime;

namespace HHG.SkillCheckSystem.Runtime
{
    public interface ISkillCheck
    {
        string Header { get; }
        string Description { get; }
        int Difficulty { get; }
        Dice Dice { get; }
    }

    public static class ISkillCheckExtensions
    {
        public static bool PerformSkillCheck(this ISkillCheck skillCheck, int skillBonus, out int roll, out int final)
        {
            roll = skillCheck.Dice.Roll();
            final = roll + skillBonus;
            return final >= skillCheck.Difficulty;
        }
    }
}