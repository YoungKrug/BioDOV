namespace _Scripts.Planning.Interfaces
{
    public interface IPlannable
    {
        public int Order { get; }
        public bool CheckForPrerequisite(object obj);
        public PlannerConfig PlannedExecution(PlannerConfig obj);
        public string GetName();
    }
}