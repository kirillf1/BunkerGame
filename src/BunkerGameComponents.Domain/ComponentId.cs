namespace BunkerGameComponents.Domain
{
    public class ComponentId : Value<ComponentId>
    {
        public ComponentId(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
