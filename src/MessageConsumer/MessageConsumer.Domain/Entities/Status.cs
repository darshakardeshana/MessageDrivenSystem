using System.ComponentModel.DataAnnotations;

namespace MessageConsumer.Domain.Entities
{
    public enum Status
    {
        [Display(Name = "Success")]
        Success,
        [Display(Name = "Error")]
        Error
    }
}
