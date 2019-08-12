namespace Server.Items
{
    public interface ITrappable : IEntity
    {
        int ItemID { get; set; }
        bool Trapped { get; }
    }
}