using Game.Components.Experience;
using Game.Components.Timer;
using Game.Components.UI;
using Game.System.Entity;
using Game.System.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NodeController : MonoBehaviour, IExperienceable, ITriggeringPercent
{
    const float cellDistance = 3;

    public static float maxX; // kasno sam shvatio da mi treba, a ovde mi je bilo najbezbolnije i najbrze da izvucem... - ne znaci da bi ga ostavio tako...
    public static float maxY;

    [SerializeField] private CustomButton CustomButton;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Text DifficultyText;
    [SerializeField] private Image ModifierImage;
    public TimerView TimerView;
    Material material;

    public NodeState State => (currentState != null) ? currentState.State : NodeState.NaN; // helper da ne bi morao salno da kastujem da bi proverio u kom je stanju nod
    public int Difficulty { get; private set; }
    public int Level => Config.DefaultExperience + Difficulty; //nije resenje problema, u svakom slucaju tu je, lako moze da se promeni...
    public virtual int TriggeringPercent => node.TriggerTracerChance + Difficulty; //treba neka logaritamska funkcija verovatno
    public NetworkController NetworkController { get; private set; } // SVESTAN SAM DA SU SVE METODE REFERENTNIH PROPERIA IZLOZENE IAKO JE PRIVATE SET, samo nemam jos i kad tim da se bavim
    public Dictionary<NodeController, NodePathController> Neighbors { get; private set; } // napravi custom, da ne bi bile izlozene metode od dictionary-a

    protected BaseNodeState currentState;
    protected HashSet<IUser> hackedBy;

    Node node; // ovo realno mogu i da sklonim... treba mi jedna vrednost odatle...

    protected virtual void Awake()
    {
        CustomButton.onClick.AddListener(OnClick);

        ModifierImage.gameObject.SetActive(false);
    }

    void OnClick()
    {
        ActionPanelController.Instance.Open(Player.Instance, this);
        ActionPanelController.Instance.transform.SetParent(transform);
    }

    public void SetData(NetworkController networkController, Node node)
    {
        NetworkController = networkController;
        this.node = node;
        Neighbors = new Dictionary<NodeController, NodePathController>();
        hackedBy = new HashSet<IUser>();

        transform.position = GetPosition();
        SetDifficulty(node.Difficulty);
    }
    
    Vector3 GetPosition() // zbudzeno na brzaka...
    {
        Vector3 position = new Vector3(node.Cell.X * cellDistance, node.Cell.Y * cellDistance);
        
        if (maxX < position.x)
            maxX = position.x;

        if (maxY < position.y)
            maxY = position.y;

        return position;
    }

    void SetDifficulty(int difficulty)
    {
        Difficulty = difficulty;
        DifficultyText.text = Difficulty.ToString();
    }

    public void RandomizeDifficulty()
    {
        SetDifficulty(Random.Range(Config.MinDifficulty, Config.MaxDifficulty));
    }

    public void SetGrayscale()
    {
        if (material == null)
            material = SpriteRenderer.material;

        material.SetFloat(Global.GrayscaleProp, 1);
    }

    public void ResetGrayscale()
    {
        if (material == null)
            material = SpriteRenderer.material;

        material.SetFloat(Global.GrayscaleProp, 0);
    }

    public void SetModifier(Item item)
    {
        ModifierImage.sprite = item.Definition.IconSprite;
        ModifierImage.gameObject.SetActive(true);
    }

    public void ResetModifier()
    {
        ModifierImage.gameObject.SetActive(false);
    }

    public void Lock()
    {
        CustomButton.interactable = false;

        foreach (KeyValuePair<NodeController, NodePathController> item in Neighbors)
        {
            if (item.Key.State == NodeState.Locked)
                item.Value.Lock();
        }
    }

    public void Unlock()
    {
        CustomButton.interactable = true;
    }

    public void AddNeighbor(NodeController neighbor, NodePathController nodePathController)
    {
        Neighbors.Add(neighbor, nodePathController);
    }

    private void OnDestroy()
    {
        if (currentState != null)
            currentState.OnDestroy();

        if (material != null)
            Destroy(SpriteRenderer.material);
    }

    // nisam ovo bez veze skroz
    // realbno bi trebalo da postoji stanje mreze kao sto je difficulty nodova, trap na nodu, spam zagusenje, a svaki user mreze da ima svoju instancu stanja koju je on ostavio iza sebe (hakovao, nije hakovao)
    // onda bi svaka akcija trebala da ima u potpisu IUser, da bi se znalo na koga akcija utice - ako je trap ili spam ne utice na onoga ko je izvrsio akciju
    // medjutim stanje igraca se preslikava na UI, pa sam krenuo sa idejom da je to glavno stanje mreze
    // nekako je cela logika binarna i ne znam da li bi imalo potrebe praviti ga kompleksnijim, tracer ne radi nikakve akcije (osim hakovanja) samo trpi posledice od akcija igraca
    // jos jedna stvar, kad tracer hakuje spam nod, ne treba nista da se desi... opet sve nekonzistentno tako da ne vidim kako da ga napravim da bude cisto
    // probao sam da implementiram jedan deo takvog sistema sa GetTimeToHack() => zvuci dobro, ima prilicno potencijala

    public int GetTimeToHack(IUser forUser) 
    {
        int timeToHack = Config.DefaultHackingDuration + Difficulty;
        timeToHack += timeToHack + (NetworkController.GetNetworkDifficulty(forUser) * Framework.PlayerPreferenceData.SpamNodeDecrease / 100);
        return timeToHack;
    }

    public void UnlockNeighbors(IUser user)
    {
        CustomButton.interactable = true;

        foreach (KeyValuePair<NodeController, NodePathController> item in Neighbors)
        {
            if (item.Key.State == NodeState.Locked)
                StartCoroutine(new UnlockedState(user, item.Key).SetState());
                
            item.Value.Unlock(this);
        }
    }

    public List<TimerView> GetTimerViews(IUser user)
    {
        List<TimerView> timerViews = new List<TimerView>();

        foreach (KeyValuePair<NodeController, NodePathController> item in Neighbors)
        {
            if ((item.Key.State == NodeState.InProgress || item.Key.State == NodeState.Hacked) && item.Key.hackedBy.Contains(user))
                timerViews.Add(item.Value.GetPathController(this, user));
            else if (user == Tracer.Instance && item.Key.hackedBy.Contains(user)) // BUDZEVINA
                timerViews.Add(item.Value.GetPathController(this, user));
        }

        return timerViews;
    }

    public void SetTimerViewsEndState(IUser user)
    {
        foreach (KeyValuePair<NodeController, NodePathController> item in Neighbors)
        {
            if ((item.Key.State == NodeState.InProgress || item.Key.State == NodeState.Hacked) && item.Key.hackedBy.Contains(user))
                item.Value.SetEndState(user);
            else if (user == Tracer.Instance && item.Key.hackedBy.Contains(user)) // BUDZEVINA
                item.Value.SetEndState(user);
        }
    }

    public IEnumerator ChangeState(BaseNodeState newState)
    {
        if (currentState != null)
            yield return Framework.StartCoroutine(currentState.Exit());

        currentState = newState;

        if (currentState != null)
            yield return Framework.StartCoroutine(currentState.Enter());

        if (State == NodeState.Hacked)
            OnHackedByPlayer();
    }

    public virtual void OnHackedByPlayer() // uprosceno je zato sto svakako samo player menja state nodova
    {
        hackedBy.Add(Player.Instance);
        new NodeReward().Collect(this);
        Framework.EventManager.TriggerEvent(Game.System.Event.CustomListener.NodeHacked, this);
    }

    public virtual IEnumerator TriggerState() // ovo moze samo tracer da okine...
    {
        hackedBy.Add(Tracer.Instance);
        yield return Framework.StartCoroutine(currentState.Trigger());
    }
}
