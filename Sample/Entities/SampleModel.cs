using AspNetCore.MongoDB;

namespace Sample.Entities
{
    public class SampleModel : IMongoEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
