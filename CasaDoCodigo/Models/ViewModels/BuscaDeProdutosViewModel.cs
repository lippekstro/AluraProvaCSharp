using System.Collections.Generic;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public IList<Produto> Produtos { get; }
        public string Pesquisa { get; }

        public BuscaDeProdutosViewModel(IList<Produto> produtos, string pesquisa) : this(produtos)
        {
            Pesquisa = pesquisa;
        }

        public BuscaDeProdutosViewModel(IList<Produto> produtos)
        {
            Produtos = produtos;
        }
    }
}
