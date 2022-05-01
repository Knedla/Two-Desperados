using Game.Components.LootTable;
using Game.Components.Notification;

public class NodeReward
{
    const string exp = "EXP: ";

    //1) "When any node is successfully hacked, the player gets an experience point reward."
    //2) "When you hack a “treasure node” you can get three types of rewards - “nuke” or “trap” power ups, and experience.The chance of getting any of the three should be equal."
    //jel u treasure node ide exp iz recenice pod 1) + jedna od 3 stavke iz recenice pod 2), cime moze da se dobije dupla dozu exp-a
    //ili se u treasure node recenica pod 1) overrajduje recenicom pod 2)
    //kontam da je vako kako sam napravio
    public void Collect(NodeController nodeController)
    {
        ILootable lootable = nodeController as ILootable;

        if (lootable != null)
        {
            ILootItem lootItem = lootable.GetLootItem();
            if (lootItem != null)
            {
                lootItem.CollectItem();
                return;
            }
        }

        //"After a treasure node is hacked, the reward is saved"
        //trenutno je stavljeno da posle svake dobijene nagrade cuva, bilo koji da je nod u pitanju, mada lako moze da se ogranici da cuva samo kad se hakuje treasure nod
        int experience = Framework.ExperienceManager.CalculateExperiene(nodeController);
        Framework.PlayerData.Experience.Add(experience);
        Framework.NotificationManager.Notify(new InstantNotification(null, exp + experience, null));
    }
}
