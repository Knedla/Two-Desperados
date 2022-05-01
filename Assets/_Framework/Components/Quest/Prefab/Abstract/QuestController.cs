namespace Game.Components.Quest
{
    public abstract class QuestController
    {
        public float CurrentValue { get; protected set; }
        public float RequiredValue { get; protected set; }

        public bool Done { get; protected set; }
        public abstract void SetData(QuestData questData);

        public virtual void OnDestroy() { }

        //public bool Started { get; protected set; } ovo treba ubaciti da se ne startuje isti quest 2x
    }
}
