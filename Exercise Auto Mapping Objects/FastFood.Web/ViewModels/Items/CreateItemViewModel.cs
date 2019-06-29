using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Items
{
    public class CreateItemViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
