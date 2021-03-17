
namespace Factory
{
    public class SdpFactory
    {
        public static ValueObjectFactory ValueObjectFactory() 
            => new ValueObjectFactory();
        public static EntityFactory EntityFactory()
            => new EntityFactory();
    }
}
