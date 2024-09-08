using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI phaseTitle;
    [SerializeField] private TextMeshProUGUI description;
    private void OnEnable()
    {
        UpdateInfoBox();
    }

    public void UpdateInfoBox()
    {
        IPhase curPhase = GameManager.Instance.GetCurPhase();

        if(curPhase is StartGame)
        {
            phaseTitle.text = "Hello there :D";

            description.text =
                "Thanks for playing Jam Em Up!<br>" +
                "This game was created as a semester project by a team of four students<br>" +
                "We hope you'll have lots of fun strategizing and trying to get as far as possible<br>" +
                "This info box will inform you about controls and information about the current game phase<br>" +
                "Open it whenever you need :)<br><br>" +
                "Controls:<br>" +
                "Hold Right Click: Camera Movement<br>" +
                "Space: Move on to the next game phase";

        }
        if (curPhase is SelectBuilding)
        {
            phaseTitle.text = "Building Shop";

            description.text =
                "Pick the building to add to your mountain<br><br>" +
                "There are Attack Towers, that deal damage to enemies<br>" +
                "Attack Towers are always 1x1<br><br>" +
                "There are Support Buildings, that apply a buff to directly adjacent Attack Towers<br>" +
                "They can vary in shape to potentially buff more than one Attack Tower<br><br>" +
                "Hint:<br>" +
                "Support Tower buffs stack multiplicatively";
        }
        if (curPhase is Preparation || curPhase is PlaceBuilding)
        {
            phaseTitle.text = "Preparation Phase";

            description.text =
                "You can reconfigure your Building Layout however long you need to find the perfect placement for all your buildings.<br>" +
                "If a building isn't needed anymore or gets in the way, there is the option of destroying it by clicking on the trash can, while having it selected. Be aware that you won't get it back<br><br>" +
                "Controls:<br>" +
                "Left Click: Place Building<br>" +
                "R: Rotate Building<br>" +
                "Hold Left Click On Placed Building: Pick Up That Building<br>" +
                "Space: Move on to the defense phase";
        }
        if (curPhase is DefendBase)
        {
            phaseTitle.text = "Defense Phase";

            description.text = 
                "Watch as your towers process your enemies into sweet sweet jam";
        }
        if (curPhase is DefeatScreen)
        {
            phaseTitle.text = "Defeated";

            description.text =
                "Well that didn't go all too well...<br>" +
                "How about you run it back and try a different strategy?";
        }
    }
}
