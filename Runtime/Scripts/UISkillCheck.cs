using DG.Tweening;
using HHG.Common.Runtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HHG.SkillCheckSystem.Runtime
{
    public class UISkillCheck : MonoBehaviour
    {
        private const float rollAnimationDuration = 3f;
        private const float rollLabelUpdateFrequency = .2f;
        private const float skillBonusAnimationDuration = 1f;

        [SerializeField] private TMP_Text headerLabel;
        [SerializeField] private TMP_Text descriptionLabel;
        [SerializeField] private TMP_Text difficultyLabel;
        [SerializeField] private Image diceSprite;
        [SerializeField] private TMP_Text diceLabel;
        [SerializeField] private TMP_Text skillBonusLabel;
        [SerializeField] private TMP_Text resultLabel;
        [SerializeField] private PlayableController dicePlayable;
        [SerializeField] private PlayableController skillBonusPlayable;
        [SerializeField] private PlayableController successPlayable;
        [SerializeField] private PlayableController failurePlayable;
        [SerializeField] private DiceSprites diceSprites;

        [Header("Test")]
        [SerializeField] private SkillCheck skillCheckTest;
        [SerializeField] private int skillCheckTestBonus;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // TODO: Remove once added to Playables.static.cs
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            Playable.Register<DOTweenAnimation>(t => t.RewindThenRecreateTweenAndPlay());
        }

        public void Refresh(ISkillCheck skillCheck, int skillBonus)
        {
            headerLabel.text = skillCheck.Header;
            descriptionLabel.text = skillCheck.Description;
            difficultyLabel.text = skillCheck.Difficulty.ToString();
            diceSprite.sprite = diceSprites.GetDiceSprite(skillCheck.Dice);
            diceLabel.text = "?";
            skillBonusLabel.text = $"+{skillBonus}";
            resultLabel.text = " "; // So takes up the same height
            rectTransform.RebuildLayout();
        }

        [ContextMenu("Perform Refresh Test")]
        public void PerformRefreshTest()
        {
            Refresh(skillCheckTest, skillCheckTestBonus);
        }

        [ContextMenu("Perform Skill Check Test")]
        public void PerformSkillCheckTest()
        {
            PerformSkillCheck(skillCheckTest, skillCheckTestBonus);
        }

        public void PerformSkillCheck(ISkillCheck skillCheck, int skillBonus)
        {
            bool success = skillCheck.PerformSkillCheck(skillBonus, out int roll, out int final);
            Refresh(skillCheck, skillBonus);
            OnSkillCheckPerformed(skillCheck, roll, final, success);
        }

        protected virtual void OnSkillCheckPerformed(ISkillCheck skillCheck, int roll, int final, bool success)
        {
            StartCoroutine(AnimateSkillCheckAsync(skillCheck, roll, final, success));
        }

        protected virtual IEnumerator AnimateSkillCheckAsync(ISkillCheck skillCheck, int roll, int final, bool success)
        {
            dicePlayable.Play();

            float elapsed = 0f;
            while (elapsed < rollAnimationDuration - rollLabelUpdateFrequency)
            {
                yield return new WaitForSeconds(rollLabelUpdateFrequency);
                diceLabel.text = skillCheck.Dice.Roll().ToString();
                elapsed += rollLabelUpdateFrequency;
            }

            diceLabel.text = roll.ToString();
            skillBonusPlayable.Play();

            yield return new WaitForSeconds(skillBonusAnimationDuration);

            diceLabel.text = final.ToString();
            skillBonusLabel.text = " "; // So takes up the same height
            resultLabel.text = success ? "Success" : "Failure";

            if (success)
            {
                successPlayable.Play();
            }
            else
            {
                failurePlayable.Play();
            }
        }
    }

}