using UnityEngine;
using UnityEngine.UI;

//コンティニューの使用有無によって、GOOD ENDまたはBAD ENDの概要を表示

public class EndingText : MonoBehaviour
{
    public Text endingTextBox;
    const string goodEnd = "The grand master, once defeated, agreed to reverse his world-class magic because he was able to finish the research he was trying to conduct. He was relieved from his position and given a sentence in prison for a few years, but is allowed to come back to the Mage Association if he has recognizably reconciled by that point. Since the liutenant of the twelfth squadron was the first to get into gear and start an investigation, he was promoted to new Grand Master, even given his unique way of using magic and low capacity for the art. Since you were the one who solved the incident, you are treated as a national hero, and given the newly-created position of Vice Master of the academy. As Vice Master, you personally gained the right to train the elite mages of the next generation, who lived on to become known as some of the strongest of all time.\n(Good Ending. Congratulations for beating the game without using any continues!)";
    const string badEnd = "The grand master refused to reverse the world-class magic, and the incantation for it is so long and difficult, no one could memorize it. The anachronism stopped spreading, but the areas that were affected do not return to normal. With the grand master gone, the mage association is in chaos. The country takes a relatively large stumble, but gets back on its feet within a year. People have learned to live with the revived creatures and the new(old) geography. Because there was such trouble that resulted from the grand master being removed from his position, you were relieved of your position, but still given pay, as a form of retirement for at least solving the original catastrophe.\n(Bad Ending. Try to complete the game without using any continues to get the good ending!)";
    
    void Start()
    {
        if (Parameters.singleton.continues == 5)
        {
            endingTextBox.text = goodEnd;
        }
        else
        {
            endingTextBox.text = badEnd;
        }
    }
}
