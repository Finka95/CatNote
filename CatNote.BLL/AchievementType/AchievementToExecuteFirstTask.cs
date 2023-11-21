namespace CatNote.BLL.AchievementType;

public class AchievementToExecuteFirstTask : Achievement
{
    public int TasksExecute { get; set; }

    public override Task<bool> Execute(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
