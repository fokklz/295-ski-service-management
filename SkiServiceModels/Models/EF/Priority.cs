using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.EF
{
    public class Priority : PriorityBase, IGenericEFModel
    {
        [Key]
        public int Id { get; set; }
    }
}
