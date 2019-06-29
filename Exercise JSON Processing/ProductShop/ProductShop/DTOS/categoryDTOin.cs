using System.Runtime.Serialization;

namespace ProductShop.DTOS
{
    [DataContract]
    public class categoryDTOin
    {
        [DataMember]
        public string Name { get; set; }
    }
}