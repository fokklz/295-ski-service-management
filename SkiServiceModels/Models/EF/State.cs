using SkiServiceModels.Interfaces;
using SkiServiceModels.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace SkiServiceModels.Models.EF
{
    public class State : StateBase, IGenericEFModel
    {
        [Key]
        public int Id { get; set; }
    }
}
