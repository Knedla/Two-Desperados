namespace Game.System.Validation
{
    public enum ErrorCode
    {
        StopProcessExecution, // if for any reason there is a need to interrupt the process but not to trigger the error (example: the result of the execution of the process is equal to the beginning; real world example: swap of two identical items in the inventory)
        
        Settings_00001, // Gameplay Config
        CastNumber_00001, // ne moze da kastuje u int
        Quantity_00001 // nema dovoljan iznos
    }
}
