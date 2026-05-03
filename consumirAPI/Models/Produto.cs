using consumirAPI.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumirAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [DisplayName("Id Produto")]
        public int IdProduto { get; set; }
       
        [DisplayName("Id Empresa")]
        public int IdEmpresa { get; set; }

        [DisplayName("Valor Ultima Compra")]
        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal ValorUltimaCompra { get; set; }

        [DisplayName("Lucro Minimo")]
        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal LucroMinimo { get; set; }

        [DisplayName("Lucro Maximo")]
        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal LucroMaximo { get; set; }

        [DisplayName("Preco Venda Minimo")]
        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal PrecoVendaMinimo { get; set; }

        [DisplayName("Preco Sugerido")]
        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal PrecoSugerido { get; set; }

        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }

}
