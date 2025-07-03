using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Util
{
    public enum SituacaoPedido
    {
        [Description("Aberto")]
        [Display(Name = "Aberto")]
        ABERTO = 1,
        [Description("Fechado")]
        [Display(Name = "Fechado")]
        FECHADO = 2
    }
}
