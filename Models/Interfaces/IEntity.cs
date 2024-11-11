namespace WidgetCoUser.Models.Interfaces
{
    public interface IEntity //look at me doing things right with interfaces and all
    {
        string Id { get; set; }

        //imma let cosmosdb assign partitionkeys automatically (for now) so ill pretend it doesnt exist here
    }
}