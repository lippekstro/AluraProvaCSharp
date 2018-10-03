using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto,
            ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.Include(p => p.Categoria).ToList();
        }

        public IList<Produto> GetProdutos(int id)
        {
            return dbSet.Where(p => p.Categoria.Id == id).Include(p => p.Categoria).ToList();
        }

        public IList<Produto> GetProdutos(string pesquisa)
        {
            if (string.IsNullOrEmpty(pesquisa))
            {
                return GetProdutos();
            }

            pesquisa = pesquisa.ToUpper();

            return dbSet.Where(p => p.Categoria.Nome.ToUpper().Contains(pesquisa) || p.Nome.ToUpper().Contains(pesquisa)).Include(p => p.Categoria).ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                categoriaRepository.AddCat(livro.Categoria);
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    var categoria = categoriaRepository.GetCategoria(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
